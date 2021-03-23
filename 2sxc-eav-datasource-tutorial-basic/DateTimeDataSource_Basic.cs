using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using ToSic.Eav;
using ToSic.Eav.Data;
using ToSic.Eav.Data.Builder;
using ToSic.Eav.DataSources;
using ToSic.Eav.DataSources.Queries;

namespace ToSic.Tutorial.DataSource.Basic
{
    // additional info so the visual query can provide the correct buttons and infos
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
            Provide(GetList); // default out, if accessed, will deliver GetList
        }

        /// <summary>
        /// Get-List method, which will load/build the items once requested 
        /// Note that the setup is lazy-loading so this code will only execute when used
        /// </summary>
        private ImmutableArray<IEntity> GetList()
        {
            var values = new Dictionary<string, object>
            {
                {DateFieldName, DateTime.Now}
            };
            var entity = new Entity(Constants.TransientAppId, 0, ContentTypeBuilder.Fake("unknown"), values, DateFieldName);
            
            return new [] {(IEntity) entity}.ToImmutableArray();
        }
    }
}