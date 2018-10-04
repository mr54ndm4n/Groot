using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Groot
 {
     public static class Groot
     {
         private static List<TOut> CsvReaderIters<TOut>(string filePath, Func<IEnumerable<(string, string)>, TOut> fn)
         {
             var csvlines = File.ReadAllLines(filePath);
             var header = csvlines[0].Split(',').Select(s => s.Trim()).ToList();

             return (from line in csvlines.Skip(1)
                 let data = new Dictionary<string, string>()
                 let enumRange = Enumerable.Range(start: 0, count: header.Count)
                 select fn(line.Split(',')
                     .ToList()
                     .Zip(enumRange, (value, idx) => (header[idx], value)))).ToList();
         }
         
         public static IEnumerable<Dictionary<string, string>> GetDictFromCsv(string filePath)
         {
             return CsvReaderIters(filePath, xs => xs.ToDictionary(x => x.Item1.Trim(), x => x.Item2.Trim()));
         }

         public static IEnumerable<T> GetObjectFromCsv<T>(string filePath, bool autoMapForNoCustomAttr = true)
         {
             var type = typeof(T);
             var propCollection = type.GetProperties();
             return GetDictFromCsv(filePath).Select(elem =>
             {
                 var tElem = (T) Activator.CreateInstance(typeof(T), new object[] { });
                 foreach (var x in elem)
                 {

                     IEnumerable<PropertyInfo> propertyEnumerable;
                     
                     var matchedCustomAttrProp = propCollection
                         .Where(property => property.GetCustomAttributes<GrootFieldAttribute>()
                             .Any(grootAttr => grootAttr.GetGrootFields() == x.Key)
                         );
                     
                     if (autoMapForNoCustomAttr)
                     {
                         var matchedFieldNamePropWithNoCustomAttr = propCollection
                             .Where(property => !property.GetCustomAttributes<GrootFieldAttribute>().Any())
                             .Where(property => property.Name == x.Key);
                         propertyEnumerable = matchedCustomAttrProp.Concat(matchedFieldNamePropWithNoCustomAttr);
                     }
                     else
                     {
                         propertyEnumerable = matchedCustomAttrProp;
                     }
                     
                     propertyEnumerable
                         .ToList()
                         .ForEach(prop =>
                         {
                             prop.SetValue(tElem, Convert.ChangeType(x.Value, prop.PropertyType), null);
                         });

                 }
                 return tElem;
             }).ToList();
             
         }
         
         
     }
 }