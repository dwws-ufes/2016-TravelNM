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

        public TravelPackageBuyController(IMaintenance<TravelPackage> maintenance, IMaintenance<TravelPackageBuy> maintenancebuypackage)
        {
            this._maintenance = maintenance;
            this._maintenancebuypackage = maintenancebuypackage;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAll());
        }

        public ActionResult Details(int id, TravelPackageView travelpackageview)
        {
            travelpackageview.TravelPackage = this._maintenance.Get(id);
            return View(travelpackageview);
        } 

        [HttpPost]
        public JsonResult Create(TravelPackageBuy travelpackagebuy, int id)
        {
            travelpackagebuy.DateBuy = DateTime.Now;
            travelpackagebuy.Status = 1;

            travelpackagebuy.Customer = new Customer() { Id = int.Parse(Session["IdCustomer"].ToString()) };
            travelpackagebuy.TravelPackage = new TravelPackage() { Id = id };

            this._maintenancebuypackage.Save(travelpackagebuy);
            return Json("ok");
        }
    }
}
