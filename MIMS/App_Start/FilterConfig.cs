using System.Web.Mvc;
using AlfasiWeb.Util;

namespace MIMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahErrorLoggerFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}