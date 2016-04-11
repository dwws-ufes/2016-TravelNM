using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelNM.Models;

namespace TravelNM.Controllers
{
    public class TravelPackageBuyController : BaseController
    {
        private IMaintenance<TravelPackage> _maintenance;
        private IMaintenance<TravelPackageBuy> _maintenancebuypackage;
        private IMaintenance<Customer> _maintenancepackagecustomer;

        public TravelPackageBuyController(IMaintenance<TravelPackage> maintenance, IMaintenance<TravelPackageBuy> maintenancebuypackage,
            IMaintenance<Customer> maintenancepackagecustomer)
        {
            this._maintenance = maintenance;
            this._maintenancebuypackage = maintenancebuypackage;
            this._maintenancepackagecustomer = maintenancepackagecustomer;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAll());
        }

        [HttpPost]
        public JsonResult Create(TravelPackageBuy travelpackagebuy, int id)
        {           
            travelpackagebuy.DateBuy = DateTime.Now;
            travelpackagebuy.TravelPackage = _maintenance.Get(id);
            travelpackagebuy.Customer = _maintenancepackagecustomer.Get(int.Parse(Session["IdCustomer"].ToString()));

            this._maintenancebuypackage.Save(travelpackagebuy);

            return Json("ok");
        }
    }
}
