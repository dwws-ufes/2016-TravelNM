using System.Web.Mvc;
using System.Collections.Generic;
using System;
using Model;
using InterfacesTravelMN;
using VDS.RDF.Query;

namespace TravelNM.Controllers
{
    [ValidateInput(false)]
    public class QueryProcessorController : BaseController
    {
        List<RDFTriple> rdftriplelist = new List<RDFTriple>();

        private IRDFMaintenance<RDFTriple> _maintenancerdf;

        public QueryProcessorController(IRDFMaintenance<RDFTriple> maintenancerdf)
        {
            this._maintenancerdf = maintenancerdf;
        }

        public ActionResult Index()
        {
            List<RDFTriple> rdftriple = new List<RDFTriple>();

            return View (rdftriple);
        }

        public ActionResult ExecuteSparql(String Query, Methods methods)
        {
            try
            {
                string Command = Query;

                Object results = _maintenancerdf.GetTNM(Query);

                string Subject, Predicate, Object;

                if (results is SparqlResultSet)
                {
                    SparqlResultSet rset = (SparqlResultSet)results;

                    foreach (SparqlResult result in rset)
                    {
                        IList<string> SPO = result.ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        Subject = "";
                        Predicate = "";
                        Object = "";

                        if (SPO[0].ToString().Contains("?s"))
                            Subject = methods.GetStringNoAccents(SPO[0]).Replace("?s = ", "");
                        else
                            if (SPO[0].ToString().Contains("?p"))
                            Predicate = methods.GetStringNoAccents(SPO[0]).Replace("?p = ", "");
                        else
                            Object = methods.GetStringNoAccents(SPO[0]).Replace("?o = ", "");

                        if (SPO.Count > 1)
                        {
                            if (SPO[1].ToString().Contains("?s"))
                                Subject = methods.GetStringNoAccents(SPO[1]).Replace("?s = ", "");
                            else
                                if (SPO[1].ToString().Contains("?p"))
                                Predicate = methods.GetStringNoAccents(SPO[1]).Replace("?p = ", "");
                            else
                                Object = methods.GetStringNoAccents(SPO[1]).Replace("?o = ", "");
                        }

                        if (SPO.Count > 2)
                        {
                            if (SPO[2].ToString().Contains("?s"))
                                Subject = methods.GetStringNoAccents(SPO[2]).Replace("?s = ", "");
                            else
                                if (SPO[2].ToString().Contains("?p"))
                                Predicate = methods.GetStringNoAccents(SPO[2]).Replace("?p = ", "");
                            else
                                Object = methods.GetStringNoAccents(SPO[2]).Replace("?o = ", "");
                        }

                        RDFTriple rdftriple = new RDFTriple();
                        rdftriple.Subject = Subject;
                        rdftriple.Predicate = Predicate;
                        rdftriple.Object = Object;
                        rdftriplelist.Add(rdftriple);
                    }
                }
        
                return Json(rdftriplelist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }   
}
