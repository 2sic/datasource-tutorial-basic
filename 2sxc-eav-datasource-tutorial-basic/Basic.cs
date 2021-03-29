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
        NiceName = "Demo DateTime Basic",
        GlobalName = "7aee541c-7188-429f-a4bb-2663a576b19e"   // random & unique Guid
    )]
    public class DateTimeDataSourceBasic: ExternalData
    {
        public const string DateFieldName = "Date";

        /// <summary>
        /// Constructor to tell the system what out-streams we have
        /// </summary>
        public DateTimeDataSourceBasic()
        {
            Provide(GetList); // "Default" out; when accessed, will deliver GetList
        }

        /// <summary>
        /// Get-List method, which will load/build the items once requested 
        /// Note that the setup is lazy-loading so this code will only execute when used
        /// </summary>
        private ImmutableArray<IEntity> GetList()
        {
            var date = DateTime.Now;
            var values = new Dictionary<string, object>
            {
                {DateFieldName, date},
                {"Weekday", date.DayOfWeek},
                {"DayOfWeek", (int) date.DayOfWeek}
            };
            
            // Construct the IEntity and return as ImmutableArray
            var entity = Build.Entity(values, titleField: DateFieldName);
            return new [] {entity}.ToImmutableArray();
        }
    }
}