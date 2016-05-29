using InterfacesTravelMN;
using Model;
using System;
using System.Web.Mvc;
using TravelNM.Models;
using Persistence;
using VDS.RDF.Storage;
using VDS.RDF.Query;

namespace TravelNM.Controllers
{
    public class TravelPackageController : BaseController
    {
        private IMaintenance<TravelPackage> _maintenance;

        private IMaintenance<City> _maintenanceCity;

        public TravelPackageController(IMaintenance<TravelPackage> maintenance, IMaintenance<City> maintenanceCity)
        {
            this._maintenance = maintenance;
            this._maintenanceCity = maintenanceCity;
        }

        public ActionResult Index()
        {
            return View(this._maintenance.GetAll());
        }

        public ActionResult BuyPackageIndex()
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
                Response.Write("<script>window.location = '" + "Edit/" + Convert.ToString(travelpackageview.TravelPackage.Id + "' </script>"));
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
                Response.Write("<script>window.location = '" + "New/" + "' </script>");
                return null;
            }
        }

        public ActionResult Details(TravelPackage travelpackage)
        {
            travelpackage = _maintenance.Get(travelpackage.Id);
            return View(travelpackage);
        }

        public static string Prefixes()
        {
            return "PREFIX dbpedia-owl: <http://dbpedia.org/ontology/>" + System.Environment.NewLine
            + "PREFIX dbpprop: <http://dbpedia.org/property/>" + System.Environment.NewLine;
        }


        public ActionResult SearchCitySparql(int Id, TravelMNContext travelmncontext, TravelPackageView travelpackageview)
        {
            string Command =

                 @"SELECT ?desc ?name WHERE { ?x a dbpedia-owl:Place; dbpprop:name ?name; dbpedia-owl:abstract " +
                 "?desc. FILTER(str(?name) = " + "'" + _maintenanceCity.Get(Id).Name + "'"
                 + " ) FILTER(langMatches(lang(?desc), 'pt')) }";

            Object results = travelmncontext.Stardog.Query(Prefixes() + Command);

            if (results is SparqlResultSet)
            {
                SparqlResultSet rset = (SparqlResultSet)results;
                string varDesc;

                if (rset.Count >= 1)
                    varDesc = rset.Results[0].ToString().Remove(0,8);
                else
                    varDesc = "";

                return Json(varDesc, JsonRequestBehavior.AllowGet);
            }
            else
            {
                throw new Exception("query failed " + Command);
            }
        }


    }
}
