using Model;
using System.Collections.Generic;

namespace TravelNM.Models
{
    public class TravelPackageBuyView
    {
        public TravelPackageBuy TravelPackageBuy { get; set; }
        public List<Customer> Customers { get; set; }
        public List<TravelPackage> TravelPackages { get; set; }
    }
}