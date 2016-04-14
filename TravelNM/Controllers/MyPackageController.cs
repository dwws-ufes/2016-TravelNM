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
    public class MyPackageController : BaseController
    {
        private IMaintenance<TravelPackageBuy> _maintenance;

        public MyPackageController(IMaintenance<TravelPackageBuy> maintenance)
        {
            this._maintenance = maintenance;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAllId(int.Parse(Session["IdCustomer"].ToString())));
        }   
    }
}
