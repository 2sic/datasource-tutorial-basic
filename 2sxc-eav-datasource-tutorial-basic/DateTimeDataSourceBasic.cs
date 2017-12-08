using System;
using System.Collections.Generic;
using ToSic.Eav.DataSources;
using ToSic.Eav.DataSources.VisualQuery;
using ToSic.Eav.Interfaces;

namespace ToSic.Tutorial.DataSource
{
    // additional info so the visual query can provide the correct buttons and infos
    [VisualQuery(Type = DataSourceType.Source,
        DynamicOut = false,
        EnableConfig = false,
        NiceName = "DateTime-Basic",
        HelpLink = "https://github.com/2sic/2sxc/wiki/DotNet-DataSources")]
    public class DateTimeDataSourceBasic: ExternalDataDataSource
    {
        public const string DateFieldName = "Date";

        public DateTimeDataSourceBasic()
        {
            // wire up the default out-stream, so if it's accessed, will deliver the data of GetList
            Provide(GetList);
        }

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