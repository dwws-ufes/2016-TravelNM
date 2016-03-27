using InterfacesTravelMN;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ApplicationTravelMN.classes
{
    public class CityMaintenance : IMaintenance<City>
    {
        TravelMNContext context = new TravelMNContext();
        public void Save(City t)
        {
            context.Cities.Add(t);
            context.SaveChanges();
        }

        public City Get(int id)
        {
            return context.Cities.Find(id);
        }

        public List<City> GetAll()
        {
            return context.Cities.ToList();
        }

        public void Update(City t)
        {
            context.Entry(t).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(City t)
        {
            context.Cities.Remove(this.Get(t.Id));
            context.SaveChanges();
        }

        public List<City> Search(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
