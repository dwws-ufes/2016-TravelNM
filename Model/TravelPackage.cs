using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class TravelPackage : Entity
    {
        public City CityOrigin { get; set; }
        public City CityDestination { get; set; }
        public decimal Priece { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public int IdCityOrigin { get; set; }

        [NotMapped]
        public int IdCityDestination { get; set; }

    }
}
