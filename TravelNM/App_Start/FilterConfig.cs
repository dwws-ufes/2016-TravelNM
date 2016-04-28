using System.Web.Mvc;

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