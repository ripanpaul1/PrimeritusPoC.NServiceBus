using System.Web;
using System.Web.Mvc;

namespace Lateetud.PrimeritusService.Manager
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
