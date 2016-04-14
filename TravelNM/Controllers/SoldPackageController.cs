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

        public SoldPackageController(IMaintenance<TravelPackageBuy> maintenance)
        {
            this._maintenance = maintenance;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAll());
        }

        [HttpPost]
        public JsonResult Delete(TravelPackageBuy travelpackagebuy)
        {
            this._maintenance.Delete(travelpackagebuy);
            return Json("ok");
        }
    }
}
