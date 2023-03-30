using System;
using ToSic.Eav.Data.Build;
using ToSic.Eav.DataSource;
using ToSic.Eav.DataSource.VisualQuery;

namespace ToSic.Tutorial.DataSources
{
    // This `[VisualQuery]` Attribute provides info so the visual query can provide the correct buttons and infos
    // There are ca. 20 possible settings
    [VisualQuery(
        // The nice title to show in Visual Query
        NiceName = "TodayInfos (Tutorial)",
        // The name of the icon - from the google fonts
        Icon = "today",
        // A very unique ID - make sure you get a fresh one for each data source
        // for example from https://guidgenerator.com/
        NameId = "7aee541c-7188-429f-a4bb-2663a576b19e"
    )]
    public class TodayInfos: CustomDataSource
    {
        /// <summary>
        /// Constructor to tell the system what out-streams we have.
        /// 
        /// Note that the base class needs certain Dependencies, which are all wrapped in the Dependencies type.
        /// This allows for a stable API even if future base classes require more dependencies.
        /// </summary>
        /// <param name="services">The dependencies required by the base class</param>
        public TodayInfos(MyServices services): base(services, "My.Basic")
        {
            // "Default" out; when accessed, will deliver GetListWithToday
            ProvideOut(GetListWithToday, options: () => new DataFactoryOptions(titleField: "Date"));
        }

        /// <summary>
        /// Get-List method, which will load/build the items once requested 
        /// Note that the setup is lazy-loading so this code will only execute when used
        /// </summary>
        private object GetListWithToday()
        {
            // These are the values which the Entity will have
            // It uses a very simple anonymous object
            return new 
            {
                Date = DateTime.Now.ToShortDateString(),
                Weekday = DateTime.Now.DayOfWeek,
                DayOfWeek = (int)DateTime.Now.DayOfWeek
            };
        }
    }
}