using InterfacesTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelNM.Controllers
{
    public class CityController : BaseController
    {
         private IMaintenance<City> _maintenanceCity;

         public CityController(IMaintenance<City> maintenanceCity)
        {
            this._maintenanceCity = maintenanceCity;
        }

        public ActionResult Index()
        {
            return View(this._maintenanceCity.GetAll());
        }

        public ActionResult New()
        {
            return View(new City());
        }

        public ActionResult Edit(int id)
        {
            return View(this._maintenanceCity.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Update(City city)
        {
            this._maintenanceCity.Update(city);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult Delete(City city)
        {
            this._maintenanceCity.Delete(city);
            return Json("ok");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()] 
        public ActionResult Create(City city)
        {
            this._maintenanceCity.Save(city);
            return RedirectToAction("Index");
        }
    }   
}
