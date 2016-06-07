namespace Model
{
    public class TravelPackage : Entity
    {
        public City CityOrigin { get; set; }
        public City CityDestination { get; set; }
        public decimal Priece { get; set; }
        public string Description { get; set; }
        public string SameAs { get; set; }
    }
}
