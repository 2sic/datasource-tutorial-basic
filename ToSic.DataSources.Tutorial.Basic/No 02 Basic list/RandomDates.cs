﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ToSic.Eav.Data;
using ToSic.Eav.Data.Build;
using ToSic.Eav.DataSource;
using ToSic.Eav.DataSource.VisualQuery;

namespace ToSic.Tutorial.DataSources
{
    // additional info so the visual query can provide the correct buttons and infos
    [VisualQuery(
        NiceName = "Random Dates (Tutorial)",
        Icon = "date_range",
        NameId = "10ebb0af-4b4e-44cb-81e3-68c3b0bb388d"   // random & unique Guid
    )]
    public class RandomDates: CustomDataSource
    {
        #region Constants

        public const string DateFieldName = "Date";
        public const string IdField = "Id";
        public const int ItemsToGenerate = 27;

        #endregion

        #region Constructor for Dependency Injection and Services

        /// <summary>
        /// Constructor to tell the system what out-streams we have
        /// </summary>
        public RandomDates(MyServices services): base(services, "My.BsList")
        {
            // default out, if accessed, will deliver GetList
            ProvideOut(Get27RandomDates);
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
            var dateBuilder = DataFactory.New(options: new DataFactoryOptions(typeName: "BasicList", titleField: "Date"));
            var result = Enumerable
                .Range(1, ItemsToGenerate)
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