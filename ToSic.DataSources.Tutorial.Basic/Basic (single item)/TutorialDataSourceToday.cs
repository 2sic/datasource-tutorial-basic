using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ToSic.Eav.Data;
using ToSic.Eav.DataSources;
using ToSic.Eav.DataSources.Queries;

namespace ToSic.Tutorial.DataSource.Basic
{
    // Additional info so the visual query can provide the correct buttons and infos
    [VisualQuery(
        // The nice title to show in Visual Query
        NiceName = "Tutorial DateTime Basic",
        // The name of the icon - from the google fonts
        Icon = "today",
        // A very unique ID - make sure you get a fresh one for each data source
        // for example from https://guidgenerator.com/
        GlobalName = "7aee541c-7188-429f-a4bb-2663a576b19e"
    )]
    public class TutorialDataSourceToday: ExternalData
    {
        #region Constants

        public const string DateFieldName = "Date";

        #endregion

        #region Constructor for Dependency Injection and Services

        private readonly IDataBuilder _dateBuilder;

        /// <summary>
        /// Constructor to tell the system what out-streams we have.
        /// 
        /// Note that the base class needs certain Dependencies, which are all wrapped in the Dependencies type.
        /// This allows for a stable API even if future base classes require more dependencies.
        /// </summary>
        /// <param name="baseDependencies">The dependencies required by the base class</param>
        /// <param name="builder">The builder which we'll need to create our own data</param>
        public TutorialDataSourceToday(Dependencies baseDependencies, IDataBuilder builder): base(baseDependencies, "My.Basic")
        {
            // Make sure the services retrieved are connected for insights-logging
            ConnectServices(
                // Configure the builder to later create this type of data
                _dateBuilder = builder.Configure(typeName: "Basic", titleField: DateFieldName)
            );

            // "Default" out; when accessed, will deliver GetList
            Provide(GetListWithToday);
        }

        #endregion

        /// <summary>
        /// Get-List method, which will load/build the items once requested 
        /// Note that the setup is lazy-loading so this code will only execute when used
        /// </summary>
        private IImmutableList<IEntity> GetListWithToday()
        {
            var values = new Dictionary<string, object>
            {
                { DateFieldName, DateTime.Now.ToShortDateString() },
                { "Weekday", DateTime.Now.DayOfWeek },
                { "DayOfWeek", (int)DateTime.Now.DayOfWeek }
            };

            // Construct the IEntity and return as Immutable
            var entity = _dateBuilder.Create(values);
            return new[] { entity }.ToImmutableList();
        }
    }
}