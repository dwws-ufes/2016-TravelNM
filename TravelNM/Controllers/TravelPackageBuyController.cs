using InterfacesTravelMN;
using Model;
using System;
using System.Web.Mvc;
using TravelNM.Models;

namespace TravelNM.Controllers
{
    public class TravelPackageBuyController : BaseController
    {
        private IMaintenance<TravelPackage> _maintenance;
        private IMaintenance<TravelPackageBuy> _maintenancebuypackage;
        private IMaintenance<Customer> _maintenancecustomer;

        public TravelPackageBuyController(IMaintenance<TravelPackage> maintenance, IMaintenance<TravelPackageBuy> maintenancebuypackage,
            IMaintenance<Customer> maintenancecustomer)
        {
            this._maintenance = maintenance;
            this._maintenancebuypackage = maintenancebuypackage;
            this._maintenancecustomer = maintenancecustomer;
        }

        public ActionResult Index()
        {
            return View(this._maintenancebuypackage.GetAll());
        }

        public ActionResult New(TravelPackageBuyView travelpackagebuyview)
        {
            travelpackagebuyview.TravelPackage = _maintenance.GetAll();
            travelpackagebuyview.Customers = _maintenancecustomer.GetAll();
            return View(travelpackagebuyview);
        }

        public ActionResult Edit(int id, TravelPackageBuyView travelpackagebuyview)
        {
            travelpackagebuyview.TravelPackageBuy = this._maintenancebuypackage.Get(id);
            travelpackagebuyview.TravelPackage = _maintenance.GetAll();
            travelpackagebuyview.Customers = _maintenancecustomer.GetAll();

            return View(travelpackagebuyview);
        }

        [HttpPost]
        public JsonResult Delete(TravelPackageBuy travelpackagebuy)
        {
            this._maintenancebuypackage.Delete(travelpackagebuy);
            return Json("ok");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Update(TravelPackageBuyView travelpackagebuyview)
        {
            travelpackagebuyview.TravelPackageBuy.DateBuy = DateTime.Now;
            travelpackagebuyview.TravelPackageBuy.Status = int.Parse(Request.Form["Status"].ToString());

            this._maintenancebuypackage.Update(travelpackagebuyview.TravelPackageBuy);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(TravelPackageBuy travelpackagebuy)
        {
            travelpackagebuy.DateBuy = DateTime.Now;
            travelpackagebuy.Status = 1;
            this._maintenancebuypackage.Save(travelpackagebuy);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult CreateCustomer(TravelPackageBuy travelpackagebuy, int id)
        {
            travelpackagebuy.DateBuy = DateTime.Now;
            travelpackagebuy.Status = 1;

            travelpackagebuy.Customer = new Customer() { Id = int.Parse(Session["IdCustomer"].ToString()) };
            travelpackagebuy.TravelPackage = new TravelPackage() { Id = id };

            this._maintenancebuypackage.Save(travelpackagebuy);
            return Json("ok");
        }

        public ActionResult MyPackage()
        {
            return View(this._maintenancebuypackage.GetAllId(int.Parse(Session["IdCustomer"].ToString())));
        }

        public ActionResult DetailsMyPackage(int id)
        {
            if (_maintenancebuypackage.Get(id).Customer.Id == int.Parse(Session["idCustomer"].ToString()))
                return View(this._maintenancebuypackage.Get(id));
            else
                return RedirectToAction("MyPackage");
        }
    }
}
