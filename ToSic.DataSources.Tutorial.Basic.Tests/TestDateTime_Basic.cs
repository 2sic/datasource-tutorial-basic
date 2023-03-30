using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToSic.Testing.Shared;

namespace ToSic.Tutorial.DataSources.Tests
{
    [TestClass]
    public class TestDateTime_Basic: TestBaseEavDataSource
    {
        [TestMethod]
        public void DateTimeDataSource_HasOneItem()
        {
            var dtmDs = CreateDataSource<TodayInfos>();
            Assert.AreEqual(1, dtmDs.List.Count(), "make sure it has 1 and only 1 item in the list");

            var item = dtmDs.List.First();
            Assert.IsNotNull(item, "the item must be a real object");
        }

        [TestMethod]
        public void DateTimeDataSource_HasOneAttribute()
        {
            var dtmDs = CreateDataSource<TodayInfos>();
            var item = dtmDs.List.First();
            Assert.AreEqual(3, item.Attributes.Count, "has only 1 property");
        }


        [TestMethod]
        public void DateTimeDataSource_HasDateFieldWhichIsDate()
        {
            var dtmDs = CreateDataSource<TodayInfos>();
            var item = dtmDs.List.First();
            var dateAsString = item.Get<string>("Date");
            Assert.IsNotNull(dateAsString);

            var dateAsDate = item.Get<DateTime>("Date");
            Assert.IsNotNull(dateAsDate);
        }
    }
}
