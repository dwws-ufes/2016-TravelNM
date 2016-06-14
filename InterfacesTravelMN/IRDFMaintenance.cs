using System;
using VDS.RDF;

namespace InterfacesTravelMN
{
    public interface IRDFMaintenance<T>
    {
        Object Get(string Query);

        Object GetTNM(string Query);

        void SaveTNM(IGraph graph, Uri name);

        void DeleteTNM(IGraph graph);
    }
}
