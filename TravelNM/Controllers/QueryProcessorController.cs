using TravelNM.Models;
using System.Web.Mvc;
using System.Collections.Generic;
using VDS.RDF.Query;
using Persistence;
using System;

namespace TravelNM.Controllers
{
    [ValidateInput(false)]
    public class QueryProcessorController : BaseController
    {
        List<QueryProcessorView> listquery = new List<QueryProcessorView>();

        public ActionResult Index()
        {
            List<QueryProcessorView> queryprocessorview = new List<QueryProcessorView>();

            return View (queryprocessorview);
        }

        public ActionResult ExecuteSparql(String Query, TravelMNContext travelmncontext, Methods methods)
        {
            try
            {
                string Command = Query;

                Object results = travelmncontext.StardogRDF.Query(Command);

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

                        QueryProcessorView query = new QueryProcessorView();
                        query.Subject = Subject;
                        query.Predicate = Predicate;
                        query.Object = Object;
                        listquery.Add(query);
                    }
                }
        
                return Json(listquery, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }   
}
