using InterfacesTravelMN;
using Model;
using Persistence;
using System;
using VDS.RDF;

namespace ApplicationTravelMN.classes
{
    public class RDFMaintenance : IRDFMaintenance<RDFTriple>
    {
        TravelMNContext context = new TravelMNContext();

        public Object GetTNM(string Query)
        {
            return context.StardogRDF.Query(Query);
        }

        public void SaveTNM(IGraph graph, Uri name)
        {
            context.StardogRDF.SaveGraph(graph);
        }

        public void DeleteTNM(IGraph graph)
        {
            context.StardogRDF.UpdateGraph(graph.BaseUri, null, graph.Triples);
        }

        public Object Get(string Query)
        {
            return context.Stardog.Query(Query);
        }
    }
}
