using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GrootUnitTest.models;
using System.Collections.Generic;

namespace GrootUnitTest.test
{
    [TestClass]
    public class GetObjectFromCsvTest
    {
        private const string itemName = "itema";
        private const string mappedFieldWithNoCustomAttr = "mappedFieldWithCustomAttr";

        [TestMethod]
        public void AutoMapForNoCustomAttr()
        {
            var path = Path.GetFullPath("resources/grootTrees.csv");
            var trees = Groot.Groot.GetObjectFromCsv<models.TreeWithSomeGrootField>(path);

            Assert.AreEqual(4, trees.Count(), "rows amount");

            var paul = trees.First(t => t.Name1 == "paul");

            Assert.IsNotNull(paul);
            Assert.AreEqual("paul", paul.Name, "mappedFieldWithNoCustomAttr");
            Assert.AreEqual("paul", paul.Name1, "mappedFieldWithCustomAttr");
            Assert.AreEqual("paul", paul.Name2, "mappedFieldWithCustomAttr");
            Assert.AreEqual(false, paul.IsDicotyledon, "mappedFieldWithCustomAttr");
            Assert.AreEqual(23.83M, paul.Width, "mappedFieldWithCustomAttr");
            Assert.AreEqual(232.29M, paul.Height, "mappedFieldWithCustomAttr");

        }

        [TestMethod]
        public void NoAutoMapForNoCustomAttr()
        {
            var path = Path.GetFullPath("resources/grootTrees.csv");
            var trees = Groot.Groot.GetObjectFromCsv<models.TreeWithSomeGrootField>(path, false);

            Assert.AreEqual(4, trees.Count(), "rows amount");

            var paul = trees.First(t => t.Name1 == "paul");

            Assert.IsNotNull(paul);
            Assert.IsNull(paul.Name, "mappedFieldWithNoCustomAttr");
            Assert.AreEqual("paul", paul.Name1, "mappedFieldWithCustomAttr");
            Assert.AreEqual("paul", paul.Name2, "mappedFieldWithCustomAttr");
            Assert.AreEqual(false, paul.IsDicotyledon, "mappedFieldWithCustomAttr");
            Assert.AreEqual(23.83M, paul.Width, "mappedFieldWithCustomAttr");
            Assert.AreEqual(232.29M, paul.Height, "mappedFieldWithCustomAttr");

        }

        [TestMethod]
        public void AutoMapForNoCustomAttrUsingEnum()
        {
            var path = Path.GetFullPath("resources/item.csv");
            var types = Groot.Groot.GetObjectFromCsv<models.Item>(path);

            Assert.AreEqual(3, types.Count(), "rows amount");

            var itema = types.First(t => t.Name == itemName);

            Assert.IsNotNull(itema);
            Assert.AreEqual(itemName, itema.Name, mappedFieldWithNoCustomAttr);
            Assert.AreEqual(68, itema.Amount, mappedFieldWithNoCustomAttr);
            Assert.AreEqual(models.ItemType.Plastic, itema.Type, mappedFieldWithNoCustomAttr);

        }

        [TestMethod]
        public void NoAutoMapForNoCustomAttrUsingEnum()
        {
            var path = Path.GetFullPath("resources/item.csv");
            var types = Groot.Groot.GetObjectFromCsv<models.Item>(path, false);

            Assert.AreEqual(3, types.Count(), "rows amount");

            var itema = types.First(t => t.Name == itemName);

            Assert.IsNotNull(itema);
            Assert.AreEqual(itemName, itema.Name, mappedFieldWithNoCustomAttr);
            Assert.AreEqual(68, itema.Amount, mappedFieldWithNoCustomAttr);
            Assert.AreEqual(models.ItemType.Plastic, itema.Type, mappedFieldWithNoCustomAttr);

        }

        [TestMethod]
        public void GenerateCsvFile()
        {
            var path = Path.GetFullPath("resources/item.csv");
            var types = Groot.Groot.GetObjectFromCsv<Item>(path).OrderBy(x=>x.Name).ToList();
            var result = Groot.Groot.CreateCsvFromList(types, Path.GetFullPath("resources/new.csv"));
            var newTypes = Groot.Groot.GetObjectFromCsv<Item>(Path.GetFullPath("resources/new.csv")).OrderBy(x => x.Name).ToList();
            
            for(int i = 0; i < types.Count(); i++)
            {
                Assert.AreEqual(types[i].Name, newTypes[i].Name);
                Assert.AreEqual(types[i].Amount, newTypes[i].Amount);
                Assert.AreEqual(types[i].Type, newTypes[i].Type);
            }
        }
        
    }
}