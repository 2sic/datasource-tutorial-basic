﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToSic.Eav.Apps;
using ToSic.Eav.LookUp;
using ToSic.Tutorial.DataSource.Basic;

namespace ToSic.Tutorial.Datasource.Tests
{
    [TestClass]
    public class TestDateTime_Configurable
    {
        [TestMethod]
        public void TestStandardUseCase()
        {
            var yourDataSource = TestSource1();
            Assert.AreEqual(1, yourDataSource.Out.Count, "Should only find one out-stream");
            Assert.AreEqual(1, yourDataSource.List.Count());

            var first = yourDataSource.List.First(); 
            Assert.AreEqual("Date Today", first.GetBestValue("Title"));
            Assert.AreEqual("Date Today", first.GetBestValue("EntityTitle"));
            Assert.AreEqual("Saturday", first.GetBestValue("DayOfWeek"), "Expecting it to be Saturday - you'll have to update this test for your weekday.");
        }

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void UseInvalidSetting_ExpectError()
        {
            var ds = TestSource1();
            var settings = new Dictionary<string, string>
            {
                {"DesiredDate", "Not today - should cause error"}
            };
            var settingsValueProvider = new LookUpInDictionary("Settings", settings);
            ds.Configuration.LookUpEngine.Sources.Add(settingsValueProvider.Name, settingsValueProvider);
            Assert.AreEqual("Date Today", ds.List);
        }

        public DateTimeDataSource_Configurable TestSource1()
        {
#pragma warning disable 612
            return new ToSic.Eav.DataSource()
                .GetDataSource<DateTimeDataSource_Configurable>(new AppIdentity(1,1), null, TestConfigProvider());
#pragma warning restore 612
        }


        /// <summary>
        /// Create a test config provider - here you could supply tokens if you want to run tests which would resolve a token for you
        /// </summary>
        /// <returns></returns>
        public ILookUpEngine TestConfigProvider()
        {
            var vc = new LookUpEngine(null);
            // var entVc = new EntityValueProvider(AppSettings(), "AppSettings");
            // vc.Sources.Add("AppSettings".ToLower(), entVc);
            // vc.Sources.Add("AppResources".ToLower(), new EntityValueProvider(AppResources(), "AppResources"));
            return vc;
        }


    }
}
