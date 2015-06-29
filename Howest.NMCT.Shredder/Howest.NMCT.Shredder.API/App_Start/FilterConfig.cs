using System.Web;
using System.Web.Mvc;

namespace Howest.NMCT.Shredder.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}