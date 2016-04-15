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
    public class SoldPackageController : BaseController
    {
        private IMaintenance<TravelPackageBuy> _maintenance;

        private IMaintenance<Customer> _maintenanceCustomer;

        private IMaintenance<TravelPackage> _maintenanceTravelPackage;

        public SoldPackageController(IMaintenance<TravelPackageBuy> maintenance, IMaintenance<Customer> maintenanceCustomer,
            IMaintenance<TravelPackage> maintenanceTravelPackage)
        {
            this._maintenance = maintenance;
            this._maintenanceCustomer = maintenanceCustomer;
            this._maintenanceTravelPackage = maintenanceTravelPackage;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAll());
        }

        public ActionResult New(TravelPackageBuyView travelpackagebuyview)
        {
            travelpackagebuyview.TravelPackage = _maintenanceTravelPackage.GetAll();
            travelpackagebuyview.Customers = _maintenanceCustomer.GetAll();

            return View(travelpackagebuyview);
        }


        [HttpPost]
        public JsonResult Delete(TravelPackageBuy travelpackagebuy)
        {
            this._maintenance.Delete(travelpackagebuy);
            return Json("ok");
        }

        [HttpPost]
        public ActionResult Create(TravelPackageBuyView travelpackagebuyview)
        {
            travelpackagebuyview.TravelPackageBuy.DateBuy = DateTime.Now;
            travelpackagebuyview.TravelPackageBuy.Status = int.Parse(Request.Form["Status"].ToString());
            this._maintenance.Save(travelpackagebuyview.TravelPackageBuy);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id, TravelPackageBuyView travelpackagebuyview)
        {
            travelpackagebuyview.TravelPackageBuy = this._maintenance.Get(id);
            travelpackagebuyview.TravelPackage = _maintenanceTravelPackage.GetAll();
            travelpackagebuyview.Customers = _maintenanceCustomer.GetAll();
           
            return View(travelpackagebuyview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Update(TravelPackageBuyView travelpackagebuyview)
        {
            travelpackagebuyview.TravelPackageBuy.DateBuy = DateTime.Now;
            travelpackagebuyview.TravelPackageBuy.Status = int.Parse(Request.Form["Status"].ToString());

            this._maintenance.Update(travelpackagebuyview.TravelPackageBuy);
            return RedirectToAction("Index");
        }
    } 
}
