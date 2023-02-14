using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ToSic.Eav.Data;
using ToSic.Eav.DataSources;
using ToSic.Eav.DataSources.Queries;

namespace ToSic.Tutorial.DataSource.Basic
{
    // additional info so the visual query can provide the correct buttons and infos
    [VisualQuery(
        NiceName = "Tutorial Random Dates List",
        Icon = "date_range",
        GlobalName = "10ebb0af-4b4e-44cb-81e3-68c3b0bb388d"   // random & unique Guid
    )]
    public class TutorialDataSourceRandomDates: ExternalData
    {
        #region Constants

        public const string DateFieldName = "Date";
        public const string IdField = "Id";
        public const int ItemsToGenerate = 27;

        #endregion

        #region Constructor for Dependency Injection and Services

        private readonly IDataBuilder _builder;

        /// <summary>
        /// Constructor to tell the system what out-streams we have
        /// </summary>
        public TutorialDataSourceRandomDates(Dependencies dependencies, IDataBuilder builder): base(dependencies, "My.BsList")
        {
            // Make sure the services retrieved are connected for insights-logging
            ConnectServices(
                // Configure the builder to later create this type of data
                _builder = builder.Configure(typeName: "BasicList", titleField: DateFieldName)
            );

            // default out, if accessed, will deliver GetList
            Provide(Get27RandomDates);
        }

        #endregion

        /// <summary>
        /// Get-List method, which will load/build the items once requested 
        /// Note that the setup is lazy-loading,
        /// ...so this code will not execute unless it's really used
        /// </summary>
        /// <returns></returns>
        private IImmutableList<IEntity> Get27RandomDates()
        {
            var result = Enumerable
                .Range(1, ItemsToGenerate)
                .Select(i => _builder.Create(
                    new Dictionary<string, object>
                    {
                        { IdField, i },
                        { DateFieldName, RandomDay() }
                    },
                    id: i))
                .ToImmutableList();

            return result;
        }

        // helper to randomly generate dates
        private readonly Random _randomizer = new Random();
        private readonly DateTime _start = new DateTime(1995, 1, 1);

        private DateTime RandomDay()
        {
            var range = (DateTime.Today - _start).Days;
            return _start.AddDays(_randomizer.Next(range));
        }
        
    }
}