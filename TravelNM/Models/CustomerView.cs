using Model;
using System.Collections.Generic;

namespace TravelNM.Models
{
    public class CustomerView
    {
        public Customer Customer { get; set; }
        public List<City> Cities { get; set; }
    }
}