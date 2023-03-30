using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ToSic.Eav.Data;
using ToSic.Eav.Data.Build;
using ToSic.Eav.DataSource;
using ToSic.Eav.DataSource.VisualQuery;


// Demo / Training Code to help you create our own DataSource
// You can find the newest version here: https://github.com/2sic/datasource-tutorial-basic
// there is also an App showing you how it would be used
// and how such a data-source is configured. 

namespace ToSic.Tutorial.DataSources
{
    // Note that this attribute is necessary for the DataSource to show up in the 
    [VisualQuery(
        NiceName = "Tutorial DateTime Configurable",
        Icon = "event",
        NameId = "81dd49a7-fa70-4e98-b73d-8299bb3231f0",
        Type = DataSourceType.Source,
        // Guid of the Content-Type which must be exported with this DataSource
        // It's located in .data/contenttypes
        // The class RegisterGlobalContentTypes ensures that 2sxc/EAV will find it
        ConfigurationType = "677210a2-cf08-46e5-a6b2-86e56e27be99",
        HelpLink = "https://r.2sxc.org/DsCustom")]
    public class ConfigurableDateTime : CustomDataSource
    {

        #region Configuration-properties

        private const string DateTodayPlaceholder = "Today";

        /// <summary>
        /// A piece of demo-configuration. It must always be stored/accessed from the Configuration dictionary
        /// because everything in the config-dictionary will be token-resolved
        /// </summary>
        [Configuration(Fallback = DateTodayPlaceholder)]
        public string DesiredDate => Configuration.GetThis();   // Get the value from configuration - will automatically use the name of this property

        /// <summary>
        /// A number-demo config. Note that we do error-checking and store it with SetError
        /// </summary>
        [Configuration(Fallback = 17)]  // This says it will default to '17' unless overriden by config data
        public int Hours
        {
            get
            {
                // Get the stored value, and if anything fails, return -1
                var hour = Configuration.GetThis(-1);

                // If it's a valid hour-range, return it
                if (hour >= 0 && hour <= 23) 
                    return hour;

                // Error: return a negative value, so when it's processed it's clear that it was an error
                return -hour;
            }
        }


        #endregion

        /// <summary>
        /// Constructs a new EntityIdFilter
        /// </summary>
        public ConfigurableDateTime(MyServices services): base(services, "My.Config")
        {
            // The out-list contains all out-streams.
            // For performance reasons we want to make sure that they are NOT created unless accessed
            // Because of this, we create a data-stream with a deferred call to GetEntities - like this:
            ProvideOut(GetEntities);
        }


        /// <summary>
        /// This is the deferred call to retrieve entities
        /// If you created the source correctly it won't be called unless accessed
        /// This is recommended for performance reasons
        /// We also recommend placing the result in the cache...
        /// </summary>
        /// <returns></returns>
		private IImmutableList<IEntity> GetEntities()
        {
            // This will resolve the tokens of the Configuration before starting
            Configuration.Parse();

            // Here's your real code. 
            // Typically you will either perform some work with the In-streams
            // or retrieve data from another source like XML, RSS, SQL, File-storage etc.
            // Usually you would also need configuration from the UI - but sometimes not, especially if it's just for a very specific purpose
            #region Your Custom Business Logic

            try
            {
                // Check if we're trying to inform about today
                // if it fails, generate a result stream with error message inside
                if (DesiredDate != DateTodayPlaceholder)
                    return Error.Create(title: "Demo Config not Today", message: "The Demo Configuration should be 'Today' or empty.");

                // Get the hours - and if something is wrong, the ErrorStream will be pre-filled
                var hours = Hours;
                if (hours < 0)
                    return Error.Create(title: "Hour value out of range", message: $"The hour was '{-hours}' which is not valid");

                // For this demo we'll treat the current time as UTC
                var todayDate = DateTime.SpecifyKind(DateTime.Today, DateTimeKind.Utc);

                // In this demo we'll just create 1 entity containing some values related to today
                var today = new Dictionary<string, object>
                {
                    { "Title", "Date Today" },
                    { "Date", todayDate.AddHours(hours) },
                    { "DayOfWeek", DateTime.Today.DayOfWeek.ToString() },
                    { "DayOfWeekNumber", DateTime.Today.DayOfWeek }
                };

                // ...now convert to an entity with the data prepared before
                var builder = DataFactory.New(options: new DataFactoryOptions(typeName: "CustomDay", titleField: "Title"));
                var ent = builder.Create(today);
                return new List<IEntity> { ent }.ToImmutableArray();
            }
            catch (Exception ex)
            {
                // if something happens, let's return this information as a result
                return Error.Create(title: "Unexpected Error", message: "The Configurable DateTime DataSource ran into an exception.", exception: ex);
            }
            #endregion

        }
    }
}
