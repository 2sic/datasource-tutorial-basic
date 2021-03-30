using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToSic.Tutorial.DataSource.Basic;

namespace ToSic.Tutorial.Datasource.Tests
{
    [TestClass]
    public class TestDateTime_Basic
    {
        [TestMethod]
        public void DateTimeDataSource_HasOneItem()
        {
            var dtmDs = new DateTimeDataSourceBasic();
            Assert.AreEqual(1, dtmDs.List.Count(), "make sure it has 1 and only 1 item in the list");

            var item = dtmDs.List.First();
            Assert.IsNotNull(item, "the item must be a real object");
        }

        [TestMethod]
        public void DateTimeDataSource_HasOneAttribute()
        {
            var dtmDs = new DateTimeDataSourceBasic();
            var item = dtmDs.List.First();
            Assert.AreEqual(3, item.Attributes.Count, "has only 1 property");
        }


        [TestMethod]
        public void DateTimeDataSource_HasDateFieldWhichIsDate()
        {
            var dtmDs = new DateTimeDataSourceBasic();
            var item = dtmDs.List.First();
            var dateAsObject = item.GetBestValue(DateTimeDataSourceBasic.DateFieldName);
            Assert.IsNotNull(dateAsObject);

            var dateAsDate = dateAsObject as DateTime?;
            Assert.IsNotNull(dateAsDate);

        }
    }
}
