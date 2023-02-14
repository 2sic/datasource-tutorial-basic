﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToSic.Tutorial.DataSource.Basic;

namespace ToSic.Tutorial.Datasource.Tests
{
    [TestClass]
    public class TestDateTime_BasicList
    {
        [TestMethod]
        public void DateTimeDataSource_HasManyItems()
        {
            var dtmDs = new TutorialDataSourceRandomDates();
            Assert.AreEqual(TutorialDataSourceRandomDates.ItemsToGenerate, dtmDs.List.Count(), $"make sure it has exactly {TutorialDataSourceRandomDates.ItemsToGenerate} item in the list");

            var item = dtmDs.List.First();
            Assert.IsNotNull(item, "the item must be a real object");
        }

        [TestMethod]
        public void DateTimeDataSource_HasTwoAttributes()
        {
            var dtmDs = new TutorialDataSourceRandomDates();
            var item = dtmDs.List.First();
            Assert.AreEqual(2, item.Attributes.Count, "has only 2 property");
        }


        [TestMethod]
        public void DateTimeDataSource_HasDateFieldWhichIsDate()
        {
            var dtmDs = new TutorialDataSourceRandomDates();
            var item = dtmDs.List.First();
            var dateAsObject = item.GetBestValue(TutorialDataSourceRandomDates.DateFieldName);
            Assert.IsNotNull(dateAsObject);

            var dateAsDate = dateAsObject as DateTime?;
            Assert.IsNotNull(dateAsDate);

        }
    }
}
