using InterfacesTravelMN;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationTravelMN.classes
{
    public class CityMaintenance : IMaintenance<City>
    {
        TravelMNContext context = new TravelMNContext();
        public void Save(City t)
        {
            context.City.Add(t);
            context.SaveChanges();
        }

        public City Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<City> GetAll()
        {
            return context.City.ToList();
        }
    }
}
