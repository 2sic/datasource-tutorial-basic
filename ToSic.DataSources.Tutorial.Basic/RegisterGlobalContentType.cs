using System.Collections.Generic;
using System.Web;
using ToSic.Eav.Repositories;

namespace ToSic.Tutorial.DataSource.Basic
{
    /// <summary>
    /// This class will be picked up by 2sxc/EAV at boot.
    /// It will tell it where there are additional Content-Types to load.
    /// See also https://docs.2sxc.org/basics/data/content-types/range-global.html
    /// </summary>
    public class RegisterGlobalContentTypes : FolderBasedRepository
    {
        public override List<string> RootPaths => new List<string>
        {
            HttpContext.Current.Server.MapPath("~/DesktopModules/ToSic.DataSource.Tutorial.Basic/.data")
        };
    }
}