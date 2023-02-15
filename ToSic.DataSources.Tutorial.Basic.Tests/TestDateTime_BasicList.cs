using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToSic.Testing.Shared;

namespace ToSic.Tutorial.DataSources.Tests
{
    [TestClass]
    public class TestDateTime_BasicList: TestBaseEavDataSource
    {
        [TestMethod]
        public void DateTimeDataSource_HasManyItems()
        {
            var dtmDs = CreateDataSource<RandomDates>();
            Assert.AreEqual(RandomDates.ItemsToGenerate, dtmDs.List.Count(), $"make sure it has exactly {RandomDates.ItemsToGenerate} item in the list");

            var item = dtmDs.List.First();
            Assert.IsNotNull(item, "the item must be a real object");
        }

        [TestMethod]
        public void DateTimeDataSource_HasTwoAttributes()
        {
            var dtmDs = CreateDataSource<RandomDates>();
            var item = dtmDs.List.First();
            Assert.AreEqual(2, item.Attributes.Count, "has only 2 property");
        }


        [TestMethod]
        public void DateTimeDataSource_HasDateFieldWhichIsDate()
        {
            var dtmDs = CreateDataSource<RandomDates>();
            var item = dtmDs.List.First();
            var dateAsObject = item.GetBestValue(RandomDates.DateFieldName);
            Assert.IsNotNull(dateAsObject);

            var dateAsDate = dateAsObject as DateTime?;
            Assert.IsNotNull(dateAsDate);

        }
    }
}
