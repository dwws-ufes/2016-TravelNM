using System.Web;
using System.Web.Mvc;
using TravelNM.App_Start;

namespace TravelNM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
           // filters.Add(new LocalizationAttribute("en"), 0);
        }
    }
}