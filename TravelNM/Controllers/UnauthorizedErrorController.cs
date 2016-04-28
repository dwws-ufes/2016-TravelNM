using System.Web.Mvc;

namespace TravelNM.Controllers
{
    [AllowAnonymous]
    public class UnauthorizedErrorController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }
    }
}
