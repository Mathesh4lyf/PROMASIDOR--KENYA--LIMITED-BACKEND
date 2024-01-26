using System.Web;
using System.Web.Mvc;

namespace PROMASIDOR__KENYA__LIMITED
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
