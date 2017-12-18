using System;
using System.Collections.Generic;
using ToSic.Eav.DataSources;
using ToSic.Eav.DataSources.VisualQuery;
using ToSic.Eav.Interfaces;

namespace ToSic.Tutorial.DataSource
{
    // additional info so the visual query can provide the correct buttons and infos
    [VisualQuery(
        NiceName = "DateTime-Basic",
        GlobalName = "7aee541c-7188-429f-a4bb-2663a576b19e",   // namespace or guid
        HelpLink = "https://github.com/2sic/2sxc/wiki/DotNet-DataSources-Custom"
    )]
    public class DateTimeDataSourceBasic: ExternalDataDataSource
    {
        public const string DateFieldName = "Date";

        /// <summary>
        /// Constructor to tell the system what out-streams we have
        /// </summary>
        public DateTimeDataSourceBasic()
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
            var values = new Dictionary<string, object>
            {
                {DateFieldName, DateTime.Now}
            };
            var entity = AsEntity(values);
            return new List<IEntity> {entity};
        }
    }
}