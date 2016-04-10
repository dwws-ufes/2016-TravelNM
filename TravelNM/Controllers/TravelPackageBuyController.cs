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
        private IMaintenance<TravelPackageBuy> _maintenancebuy;

        public TravelPackageBuyController(IMaintenance<TravelPackage> maintenance, IMaintenance<TravelPackageBuy> maintenancebuy)
        {
            this._maintenance = maintenance;
            this._maintenancebuy = maintenancebuy;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(TravelPackageBuyView travelpackagebuyview)
        {
            this._maintenancebuy.Save(travelpackagebuyview.TravelPackageBuy);
            return RedirectToAction("Index");
        }
    }
}
