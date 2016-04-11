using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class TravelPackageBuy : Entity
    {
        public Customer Customer { get; set; }
        public TravelPackage TravelPackage { get; set; }
        public DateTime DateBuy { get; set; }
        public int Status { get; set; }
    }
}
