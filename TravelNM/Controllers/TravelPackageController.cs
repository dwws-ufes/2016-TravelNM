using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelNM.Controllers
{
    public class TravelPackageController : BaseController
    {
        private IMaintenance<TravelPackage> _maintenance;

        public TravelPackageController(IMaintenance<TravelPackage> maintenance)
        {
            this._maintenance = maintenance;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAll());
        }

    }
}
