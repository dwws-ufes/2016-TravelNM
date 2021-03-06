﻿using InterfacesTravelMN;
using Model;
using System.Linq;
using System.Web.Mvc;

namespace TravelNM.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private IMaintenance<TravelPackage> _maintenance;
        private IMaintenance<Customer> _maintenanceCustomer;

        public HomeController(IMaintenance<TravelPackage> maintenance, IMaintenance<Customer> maintenanceCustomer)
        {
            this._maintenance = maintenance;
            this._maintenanceCustomer = maintenanceCustomer;
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
            var items = this._maintenance.Search(new[] { term }).Select(x => new { label = (x.CityOrigin.Name + " x " + x.CityDestination.Name), value = (x.CityOrigin.Name + " x " + x.CityDestination.Name), id = x.Id });
           
            return Json(items, JsonRequestBehavior.AllowGet);
        }
    }
}
