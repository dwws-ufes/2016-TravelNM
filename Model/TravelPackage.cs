using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class TravelPackage : Entity
    {
        public City CityOrigin { get; set; }
        public City CityDestination { get; set; }
        public decimal Priece { get; set; }
        public string Description { get; set; }
    }
}
