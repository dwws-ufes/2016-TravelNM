using Model;
using System.Collections.Generic;

namespace TravelNM.Models
{
    public class TravelPackageView
    {
        public TravelPackage TravelPackage { get; set; }
        public List<City> Cities { get; set; }
    }
}