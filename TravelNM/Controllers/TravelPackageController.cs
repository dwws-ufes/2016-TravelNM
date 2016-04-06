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
    public class TravelPackageController : BaseController
    {
        private IMaintenance<TravelPackage> _maintenance;

        private IMaintenance<City> _maintenanceCity;

        public TravelPackageController(IMaintenance<TravelPackage> maintenance, IMaintenance<City> maintenanceCity)
        {
            this._maintenance = maintenance;

            // Added Class _maintenanceCity for fill DDLFor Cities Origin and Destination.
            this._maintenanceCity = maintenanceCity;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAll());
        }

        public ActionResult New(TravelPackageView travelpackageview)
        {
            travelpackageview.Cities = _maintenanceCity.GetAll();
            return View(travelpackageview);
        }

        public ActionResult Edit(int id, TravelPackageView travelpackageview)
        {
            travelpackageview.TravelPackage = this._maintenance.Get(id);
            travelpackageview.Cities = _maintenanceCity.GetAll();
            return View(travelpackageview);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Update(TravelPackageView travelpackageview)
        {
            if (travelpackageview.TravelPackage.CityOrigin.Id != travelpackageview.TravelPackage.CityDestination.Id)
            {
                this._maintenance.Update(travelpackageview.TravelPackage);
                return RedirectToAction("Index");
            }
            else
            {
                // Alter Lang inner Class.
                MsgSameCities("Edit/" + Convert.ToString(travelpackageview.TravelPackage.Id));

                return null;
            }
        }

        [HttpPost]
        public JsonResult Delete(TravelPackage travelpackage)
        {
            this._maintenance.Delete(travelpackage);
            return Json("ok");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(TravelPackageView travelpackageview)
        {            
            if (travelpackageview.TravelPackage.CityOrigin.Id != travelpackageview.TravelPackage.CityDestination.Id)
            {
                this._maintenance.Save(travelpackageview.TravelPackage);
                return RedirectToAction("Index");
            }
            else
            {
                // Alter Lang inner Class.
                MsgSameCities("New");

                return null;
            }
        }

        public void MsgSameCities(string Url)
        {
            var culture = System.Globalization.CultureInfo.CurrentCulture;

            if (culture.Name == "en-US")
                Response.Write("<script> alert('The cities of origin and destination may not be the same.'); window.location = '" + Url + "' </script>");
            else
                Response.Write("<script> alert('As cidades de origem e destino não podem ser iguais.'); window.location =  '" + Url + "' </script>");
        }
    }
}
