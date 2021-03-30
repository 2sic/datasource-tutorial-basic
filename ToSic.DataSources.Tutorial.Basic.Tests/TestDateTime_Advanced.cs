using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToSic.Eav.Apps;
using ToSic.Eav.LookUp;
using ToSic.Tutorial.DataSource.Basic;

namespace ToSic.Tutorial.Datasource.Tests
{
    // Tests disabled for now, because they would require Dependency Injection configured
    // which is quite complex for such a simple demo.
    [TestClass]
    public class TestDateTime_Configurable
    {
        [TestMethod]
        public void TestStandardUseCase()
        {
            // Tests disabled for now, because they would require Dependency Injection configured
            // which is quite complex for such a simple demo.
            return; 
            var yourDataSource = TestSource1();
            Assert.AreEqual(1, yourDataSource.Out.Count, "Should only find one out-stream");
            Assert.AreEqual(1, yourDataSource.List.Count());

            var first = yourDataSource.List.First(); 
            Assert.AreEqual("Date Today", first.GetBestValue("Title"));
            Assert.AreEqual("Date Today", first.GetBestValue("EntityTitle"));
            Assert.AreEqual("Saturday", first.GetBestValue("DayOfWeek"), "Expecting it to be Saturday - you'll have to update this test for your weekday.");
        }

        // Tests disabled for now, because they would require Dependency Injection configured
        // which is quite complex for such a simple demo.
        //[TestMethod]
        //public void UseInvalidSetting_ExpectError()
        //{
        //    var ds = TestSource1();
        //    var settings = new Dictionary<string, string>
        //    {
        //        {"DesiredDate", "Not today - should cause error"}
        //    };
        //    var settingsValueProvider = new LookUpInDictionary("Settings", settings);
        //    ds.Configuration.LookUpEngine.Sources.Add(settingsValueProvider.Name, settingsValueProvider);
        //    Assert.AreEqual("Date Today", ds.List);
        //}

        public ConfigurableDateTime TestSource1()
        {
            return new ToSic.Eav.DataSource()
                .GetDataSource<ConfigurableDateTime>(new AppIdentity(1,1), null, TestConfigProvider());
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
