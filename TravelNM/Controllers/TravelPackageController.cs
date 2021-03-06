﻿using InterfacesTravelMN;
using Model;
using System;
using System.Web.Mvc;
using TravelNM.Models;
using VDS.RDF.Query;

namespace TravelNM.Controllers
{
    public class TravelPackageController : BaseController
    {
        private IMaintenance<TravelPackage> _maintenance;

        private IMaintenance<City> _maintenanceCity;

        private RDFController _rdfcontroller;

        private IRDFMaintenance<RDFTriple> _maintenancerdf;

        public TravelPackageController(IMaintenance<TravelPackage> maintenance, IMaintenance<City> maintenanceCity,
            RDFController rdfcontroller, IRDFMaintenance<RDFTriple> maintenancerdf)
        {
            this._maintenance = maintenance;
            this._maintenanceCity = maintenanceCity;
            this._rdfcontroller = rdfcontroller;
            this._maintenancerdf = maintenancerdf;
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

                String Origin = _maintenanceCity.Get(travelpackageview.TravelPackage.CityOrigin.Id).Name;     
                String Destination = _maintenanceCity.Get(travelpackageview.TravelPackage.CityDestination.Id).Name;

                _rdfcontroller.Package(Origin.ToString().Replace("-", "") + "_" +
                Destination.ToString().Replace("-", ""), travelpackageview.TravelPackage.Id);
               
                return RedirectToAction("Index");
            }
            else
            {
                Response.Write("<script>window.location = '" + "Edit/" + Convert.ToString(travelpackageview.TravelPackage.Id + "' </script>"));
                return null;
            }
        }

        [HttpPost]
        public JsonResult Delete(TravelPackage travelpackage, Methods methods)
        {
            this._maintenance.Delete(travelpackage);

            String Origin = methods.GetStringNoAccents(_maintenanceCity.Get(travelpackage.IdCityOrigin).Name);

            String Destination = methods.GetStringNoAccents(_maintenanceCity.Get(travelpackage.IdCityDestination).Name);

            _rdfcontroller.deleteRDF(@"C:\Dell\" + Origin.Replace(" ", "-") + "_to_" +
            Destination.Replace(" ", "-") + ".rdf", Origin, Destination);

            return Json("ok");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(TravelPackageView travelpackageview)
        {
            if (travelpackageview.TravelPackage.CityOrigin.Id != travelpackageview.TravelPackage.CityDestination.Id)
            {
                this._maintenance.Save(travelpackageview.TravelPackage);

                String Origin = _maintenanceCity.Get(travelpackageview.TravelPackage.CityOrigin.Id).Name;
                String Destination = _maintenanceCity.Get(travelpackageview.TravelPackage.CityDestination.Id).Name;

                _rdfcontroller.Package(Origin.ToString().Replace("-", "") + "_" +
                    Destination.ToString().Replace("-", ""), travelpackageview.TravelPackage.Id);

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

        public ActionResult SearchCitySparql(int Id, TravelPackageView travelpackageview)
        {
            string Command =

                 @"SELECT ?x ?desc WHERE { ?x a dbpedia-owl:Place; dbpprop:name ?name; dbpedia-owl:abstract " +
                 "?desc. FILTER(str(?name) = " + "'" + _maintenanceCity.Get(Id).Name + "'"
                 + " ) FILTER(langMatches(lang(?desc), 'pt')) }";

            Object results = _maintenancerdf.Get(Prefixes() + Command);

            if (results is SparqlResultSet)
            {
                SparqlResultSet rset = (SparqlResultSet)results;
                string varDesc;

                if (rset.Count >= 1)
                {
                    string str = rset.Results[0].ToString();

                    string[] strs = str.Remove(0, 5).Split('?');

                    varDesc = strs[1].Remove(0, 7);
                }
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
