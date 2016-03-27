using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelNM.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private IMaintenance<TravelPackage> _maintenance;
        public HomeController(IMaintenance<TravelPackage> maintenance)
        {
            this._maintenance = maintenance;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Admin()
        {
            return RedirectToActionPermanent("Index", "Admin");
        }

        public ActionResult SearchPackage(string term)
        {
            var items = this._maintenance.Search(new [] { term }).Select(x => (x.CityOrigin.Name + " x " + x.CityDestination.Name));

            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
