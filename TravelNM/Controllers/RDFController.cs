using InterfacesTravelMN;
using Model;
using System;
using System.Linq;
using System.Web.Mvc;
using VDS.RDF;
using VDS.RDF.Writing;

namespace TravelNM.Controllers
{
    [AllowAnonymous]
    public class RDFController : Controller
    {
        private IMaintenance<TravelPackage> _maintenance;

        private Methods _methods;

        public RDFController(IMaintenance<TravelPackage> maintenance, Methods methods)
        {
            this._maintenance = maintenance;

            this._methods = methods;
        }

        public void Package(string id, int Id)
        {
            var travelpackage = _maintenance.Get(Id);

            string[] Cities = id.Split('_');
            var Origin = _methods.GetStringNoAccents(Cities[0].Replace('-', ' '));
            var Destination = _methods.GetStringNoAccents(Cities[1].Replace('-', ' '));

            IGraph graph = new Graph(true);

            IUriNode dotNetRDF = graph.CreateUriNode(UriFactory.Create("http://localhost:58402/rdf/SeePackage/" + _methods.GetStringNoAccents(id.Replace(" ", "-"))));

            graph.NamespaceMap.AddNamespace("tnm", UriFactory.Create("http://localhost:58402/voctravel/v1#"));
            IUriNode uri_local = graph.CreateUriNode("tnm:hasOrigin");
            ILiteralNode local = graph.CreateLiteralNode(travelpackage.CityOrigin.Name);
            graph.Assert(new Triple(dotNetRDF, uri_local, local));

            graph.NamespaceMap.AddNamespace("tnm", UriFactory.Create("http://localhost:58402/voctravel/v1#"));
            IUriNode uri_local_destination = graph.CreateUriNode("tnm:hasDestination");
            ILiteralNode destination = graph.CreateLiteralNode(travelpackage.CityDestination.Name);
            graph.Assert(new Triple(dotNetRDF, uri_local_destination, destination));

            graph.NamespaceMap.AddNamespace("grNS", UriFactory.Create("http://purl.org/goodrelations/v1#"));
            IUriNode uri_price = graph.CreateUriNode("grNS:hasCurrencyValue");
            ILiteralNode price = graph.CreateLiteralNode(travelpackage.Priece.ToString("C"));
            graph.Assert(new Triple(dotNetRDF, uri_price, price));

            graph.NamespaceMap.AddNamespace("grNS", UriFactory.Create("http://purl.org/goodrelations/v1#"));
            IUriNode uri_description = graph.CreateUriNode("grNS:description");
            ILiteralNode description = graph.CreateLiteralNode(travelpackage.Description);
            graph.Assert(new Triple(dotNetRDF, uri_description, description));

            graph.NamespaceMap.AddNamespace("owl", UriFactory.Create("http://www.w3.org/2002/07/owl#"));
            IUriNode uri_sameas = graph.CreateUriNode("owl:sameAS");
            ILiteralNode sameas = graph.CreateLiteralNode(_methods.GetStringNoAccents(travelpackage.SameAs));
            graph.Assert(new Triple(dotNetRDF, uri_sameas,  sameas));


            graph.NamespaceMap.AddNamespace("dcterms", UriFactory.Create("http://purl.org/dc/terms/"));

            IUriNode uri_dcdatecreatedon = graph.CreateUriNode("dcterms:created");       
            ILiteralNode dcdatecreatedon = graph.CreateLiteralNode(Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy")));
            graph.Assert(new Triple(dotNetRDF, uri_dcdatecreatedon, dcdatecreatedon));

            IUriNode uri_dcdatemodifieddon = graph.CreateUriNode("dcterms:modified");
            ILiteralNode dcdatemodifieddon = graph.CreateLiteralNode(Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy")));
            graph.Assert(new Triple(dotNetRDF, uri_dcdatemodifieddon, dcdatemodifieddon));

            IUriNode uri_dccreate = graph.CreateUriNode("dcterms:create");
            ILiteralNode dccreate = graph.CreateLiteralNode("Created by Travel NM");
            graph.Assert(new Triple(dotNetRDF, uri_dccreate, dccreate));

            RdfXmlWriter rdfxmlwriter = new RdfXmlWriter();

            if (System.IO.File.Exists(@"C:\Dell\" + Origin.Replace(" ", "-") + "_to_" +
                Destination.Replace(" ", "-") + ".rdf"))

                System.IO.File.Delete(@"C:\Dell\" + Origin.Replace(" ", "-") + "_to_" +
                Destination.Replace(" ", "-") + ".rdf");

            rdfxmlwriter.Save(graph, @"C:\Dell\" + Origin.Replace(" ","-") + "_to_" +
                Destination.Replace(" ", "-") + ".rdf");
        }

        public string SeePackage(string id)
        {
            TravelPackage travelpackage = _maintenance.GetAll().FirstOrDefault();

            Response.ContentType = "application/xml";

            string[] Cities = id.Split('_');
            var Origin = _methods.GetStringNoAccents(Cities[0].Replace('-', ' '));
            var Destination = _methods.GetStringNoAccents(Cities[1].Replace('-', ' '));

            IGraph graph = new Graph(true);

            IUriNode dotNetRDF = graph.CreateUriNode(UriFactory.Create("http://localhost:58402/rdf/SeePackage/" + _methods.GetStringNoAccents(id.Replace(" ", "-"))));

            graph.NamespaceMap.AddNamespace("tnm", UriFactory.Create("http://travelnm.org/voctravel/v1#"));
            IUriNode uri_local = graph.CreateUriNode("tnm:hasOrigin");
            ILiteralNode local = graph.CreateLiteralNode(travelpackage.CityOrigin.Name);
            graph.Assert(new Triple(dotNetRDF, uri_local, local));

            graph.NamespaceMap.AddNamespace("tnm", UriFactory.Create("http://travelnm.org/voctravel/v1#"));
            IUriNode uri_local_destination = graph.CreateUriNode("tnm:hasDestination");
            ILiteralNode destination = graph.CreateLiteralNode(travelpackage.CityDestination.Name);
            graph.Assert(new Triple(dotNetRDF, uri_local_destination, destination));

            graph.NamespaceMap.AddNamespace("grNS", UriFactory.Create("http://purl.org/goodrelations/v1#"));
            IUriNode uri_price = graph.CreateUriNode("grNS:hasCurrencyValue");
            ILiteralNode price = graph.CreateLiteralNode(travelpackage.Priece.ToString("C"));
            graph.Assert(new Triple(dotNetRDF, uri_price, price));

            graph.NamespaceMap.AddNamespace("grNS", UriFactory.Create("http://purl.org/goodrelations/v1#"));
            IUriNode uri_description = graph.CreateUriNode("grNS:description");
            ILiteralNode description = graph.CreateLiteralNode(travelpackage.Description);
            graph.Assert(new Triple(dotNetRDF, uri_description, description));

            graph.NamespaceMap.AddNamespace("owl", UriFactory.Create("http://www.w3.org/2002/07/owl#"));
            IUriNode uri_sameas = graph.CreateUriNode("owl:sameAS");
            ILiteralNode sameas = graph.CreateLiteralNode(travelpackage.SameAs);
            graph.Assert(new Triple(dotNetRDF, uri_sameas, sameas));


            graph.NamespaceMap.AddNamespace("dcterms", UriFactory.Create("http://purl.org/dc/terms/"));

            IUriNode uri_dcdatecreatedon = graph.CreateUriNode("dcterms:created");
            ILiteralNode dcdatecreatedon = graph.CreateLiteralNode(Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy")));
            graph.Assert(new Triple(dotNetRDF, uri_dcdatecreatedon, dcdatecreatedon));

            IUriNode uri_dcdatemodifieddon = graph.CreateUriNode("dcterms:modified");
            ILiteralNode dcdatemodifieddon = graph.CreateLiteralNode(Convert.ToString(DateTime.Now.ToString("dd/MM/yyyy")));
            graph.Assert(new Triple(dotNetRDF, uri_dcdatemodifieddon, dcdatemodifieddon));

            IUriNode uri_dccreate = graph.CreateUriNode("dcterms:create");
            ILiteralNode dccreate = graph.CreateLiteralNode("Created by Travel NM");
            graph.Assert(new Triple(dotNetRDF, uri_dccreate, dccreate));          

            string graphStr = System.IO.File.ReadAllText(@"C:\Dell\" + Origin.Replace(" ", "-") + "_to_" +
                Destination.Replace(" ", "-") + ".rdf");

            return graphStr;
        }
    }
}













