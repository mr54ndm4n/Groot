using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Groot
 {
     public class Groot
     {

         public static List<OUT> CsvReaderIters<OUT>(string filePath, Func<IEnumerable<(string, string)>, OUT> fn)
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
         
         public static List<Dictionary<string, string>> GetDictFromCsv(string filePath)
         {
             return CsvReaderIters(filePath, xs => xs.ToDictionary(x => x.Item1.Trim(), x => x.Item2.Trim()));
         }

         public static List<T> GetObjectFromCsv<T>(string filePath)
         {
             Type type = typeof(T);
             return GetDictFromCsv(filePath).Select(elem =>
             {
                 T tElem = (T) Activator.CreateInstance(typeof(T), new object[] { });
                 foreach (KeyValuePair<string, string> x in elem)
                 {
                     PropertyInfo prop = type.GetProperty(x.Key);
                     prop.SetValue(tElem, Convert.ChangeType(x.Value, prop.PropertyType), null);
                 }
                 return tElem;
             }).ToList();
             
         }
         
         
     }
 }