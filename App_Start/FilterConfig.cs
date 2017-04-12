using System.Web;
using System.Web.Mvc;

namespace EF6_DBfirstOracle_poc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
