using System;

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
