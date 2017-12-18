using System;
using System.Collections.Generic;
using ToSic.Eav.DataSources;
using ToSic.Eav.DataSources.VisualQuery;
using ToSic.Eav.Interfaces;

namespace ToSic.Tutorial.DataSource
{
    // additional info so the visual query can provide the correct buttons and infos
    [VisualQuery(
        GlobalName = "10ebb0af-4b4e-44cb-81e3-68c3b0bb388d",   // namespace or guid
        NiceName = "DateTime-BasicList",
        HelpLink = "https://github.com/2sic/2sxc/wiki/DotNet-DataSources-Custom"
    )]
    public class DateTimeDataSourceBasicList: ExternalDataDataSource
    {
        public const string DateFieldName = "Date";
        public const string IdField = "Id";
        public const int ItemsToGenerate = 7;

        /// <summary>
        /// Constructor to tell the system what out-streams we have
        /// </summary>
        public DateTimeDataSourceBasicList()
        {
            Provide(GetList); // default out, if accessed, will deliver GetList
        }

        /// <summary>
        /// Get-List method, which will load/build the items once requested 
        /// Note that the setup is lazy-loading,
        /// ...so this code will not execute unless it's really used
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IEntity> GetList()
        {
            var randomNumbers = new List<IEntity>();

            for (var i = 0; i < ItemsToGenerate; i++)
            {
                var values = new Dictionary<string, object>
                {
                    {IdField, i},
                    {DateFieldName, RandomDay()}
                };
                randomNumbers.Add(AsEntity(values, DateFieldName, id: i));
            }

            return randomNumbers;
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