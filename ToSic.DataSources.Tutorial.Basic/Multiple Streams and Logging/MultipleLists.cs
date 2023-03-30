using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ToSic.Eav.Data;
using ToSic.Eav.Data.Build;
using ToSic.Eav.DataSource;
using ToSic.Eav.DataSource.VisualQuery;
using ToSic.Lib.Logging;

namespace ToSic.Tutorial.DataSources
{
    // additional info so the visual query can provide the correct buttons and infos
    [VisualQuery(
        NiceName = "Multiple Lists and Logging",
        Icon = "date_range",
        NameId = "c90c6ce6-20a8-41ba-ad18-d37090306b31"   // random & unique Guid
    )]
    public class MultipleLists : CustomDataSource
    {
        #region Constants

        public const string DateFieldName = "Date";
        public const string IdField = "Id";

        #endregion

        #region Constructor for Dependency Injection and Services

        /// <summary>
        /// Constructor to tell the system what out-streams we have
        /// </summary>
        public MultipleLists(MyServices services) : base(services, "My.BsList")
        {
            // default out, if accessed, will deliver GetList
            ProvideOut(Get27RandomDates);
            // Another out
            ProvideOut(Get42RandomDates, name: "MeaningOfLife");
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
            var dateBuilder = DataFactory.New(options: new DataFactoryOptions(typeName: "BasicList", titleField: DateFieldName));
            var result = Enumerable
                .Range(1, 27)
                .Select(i => dateBuilder.Create(
                    new Dictionary<string, object>
                    {
                        { IdField, i },
                        { DateFieldName, RandomDay() }
                    },
                    id: i))
                .ToImmutableList();

            return result;
        }

        private IImmutableList<IEntity> Get42RandomDates() => Log.Func(l =>
        {
            // Do some logging
            l.A("Just an info which is logged into insights");

            var dateBuilder = DataFactory.New(options: new DataFactoryOptions(typeName: "BasicList", titleField: DateFieldName));
            var result = Enumerable
                .Range(1, 42)
                .Select(i => dateBuilder.Create(
                    new Dictionary<string, object>
                    {
                        { IdField, i },
                        { DateFieldName, RandomDay() }
                    },
                    id: i))
                .ToImmutableList();

            // Return result together with a message for the log
            return (result, $"Found {result.Count} items");
        });

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