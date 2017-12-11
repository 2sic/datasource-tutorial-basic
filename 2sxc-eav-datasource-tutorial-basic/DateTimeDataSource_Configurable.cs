using System;
using System.Collections.Generic;
using ToSic.Eav.DataSources;
using ToSic.Eav.DataSources.VisualQuery;
using ToSic.Eav.Interfaces;

// Demo / Training Code to help you create our own DataSource
// You can find the newest version here: https://github.com/2sic/eav-Custom-DataSource
// there is also an App showing you how it would be used
// and how such a data-source is configured. 
// todo!!!! 
// The app is important because it contains a content-type called "|Config MyCompany.DataSources.DateTimeDataSource" which is needed for configuration
// The app can be found here: http://2sxc.org/en/Apps/Details?AppGuid=3bbc0160-a366-49db-99a0-0d50932e8fba
// Also read the blog I wrote about this in http://www.dnnsoftware.com/community-blog

namespace ToSic.Tutorial.Datasource
{
    // Note that this attribute is necessary for the DataSource to show up in the 
    [VisualQuery(Type = DataSourceType.Source,
        DynamicOut = false,
        NiceName = "DateTime-Configurable",
        HelpLink = "https://github.com/2sic/2sxc/wiki/DotNet-DataSources")]
    public class DateTimeDataSource_Configurable: ExternalDataDataSource
    {
        #region Configuration-properties
		private const string SomeDemoConfigurationKey = "DemoConfigNameInConfigList";
        private const string AnotherDemoConfigKey = "AnotherDemoConfig";

		/// <summary>
		/// A piece of demo-configuration. It must always be stored/accessed from the Configuration dictionary
		/// because everythin in the config-dictionary will be token-resolved
		/// </summary>
		public string DemoConfiguration
		{
			get => Configuration[SomeDemoConfigurationKey];
		    set => Configuration[SomeDemoConfigurationKey] = value;
		}

        /// <summary>
        /// A number-demo config. Note that we don't do error-checking, because if it's actuall not a number, a error should really be raised. 
        /// You'll see later that we're actually starting with a token, but we're already adding a fallback number so after token resolving
        /// it should always be a number
        /// </summary>
        public int AnotherDemoConfig
        {
            get => Convert.ToInt32(Configuration[AnotherDemoConfigKey]);
            set => Configuration[AnotherDemoConfigKey] = value.ToString();
        }

		#endregion

		/// <summary>
		/// Constructs a new EntityIdFilter
		/// </summary>
		public DateTimeDataSource_Configurable()
		{
            // The out-list contains all out-streams.
            // For performance reasons we want to make sure that they are NOT created unless accessed
            // Because of this, we create a data-stream with a deferred call to GetEntities - like this:
			Provide(GetEntities);

            // Example of pre-configuring a text
            // This will place the token to be resolved into the variable
            // The tokens will be resolved before use
            // The following token means: 
            // - Try to use the configured value from the setting on this data-source in the visual query
            // - if there is none, just use the value "Today"
            ConfigMask(SomeDemoConfigurationKey, "[Settings:DesiredDate||Today]"); 
            
            // Example of pre-configuring a number value
            // We can't just say AnotherDemoConfig = "text" because that would not compile since it expects a number
            // So we just add the token to be resolved directly to the configuration list
            ConfigMask(AnotherDemoConfigKey, "[Settings:Hours||17]"); 
		}


        private List<IEntity> _cachedEntities; 

        /// <summary>
        /// This is the deferred call to retrieve entities
        /// If you created the source correctly it won't be called unless accessed
        /// This is recommended for performance reasons
        /// We also recommend placing the result in the cache...
        /// </summary>
        /// <returns></returns>
		private IEnumerable<IEntity> GetEntities()
        {
            // try to use the cached result in case this had been accessed before
            if (_cachedEntities != null)
                return _cachedEntities;

            // This will resolve the tokens before starting
			EnsureConfigurationIsLoaded();

            // Here's your real code. 
            // Typically you will either perform some work with the In-streams
            // or retrieve data from another source like XML, RSS, SQL, File-storage etc.
            // Usually you would also need configuration from the UI - but sometimes not, especially if it's just for a very specific purpose
			#region Your Custom Business Logic

            _cachedEntities = new List<IEntity>();
			try
			{
                // Check if we're trying to inform about today
			    if (DemoConfiguration != "Today")
                    throw new Exception();

                // In this demo we'll just create 1 entity containing some values related to today
                // I'll use the simple method of placing all values in a dictionary and then converting it into an Entity-object
			    var today = new Dictionary<string, object>
			    {
			        {"Title", "Date Today"},
			        {"Date", DateTime.Today},
			        {"DayOfWeek", DateTime.Today.DayOfWeek.ToString()},
			        {"DayOfWeekNumber", DateTime.Today.DayOfWeek}
			    };

			    // ...now convert to an entity, and add to the list of results
			    var ent = AsEntity(today, "Title", "DateTimeInfo");
                _cachedEntities.Add(ent);
			}
			catch (Exception ex)
			{
				throw new Exception("Unable to generate entities in this custom data source", ex);
			}
			#endregion

			return _cachedEntities;
		}
    }
}
