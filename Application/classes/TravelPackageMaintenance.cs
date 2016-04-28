using InterfacesTravelMN;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace ApplicationTravelMN.classes
{
    public class TravelPackageMaintenance : IMaintenance<TravelPackage>
    {
        TravelMNContext context = new TravelMNContext();
        public void Save(TravelPackage t)
        {
            context.TravelPackages.Attach(t);
            context.TravelPackages.Add(t);
            context.SaveChanges();
        }

        public TravelPackage Get(int id)
        {
            return context.TravelPackages.Include(o => o.CityOrigin).Include(d => d.CityDestination).Where(item => item.Id == id).FirstOrDefault();
        }

        public List<TravelPackage> GetAll()
        {
            return context.TravelPackages.Include(o => o.CityOrigin).Include(d => d.CityDestination).ToList();
        }

        public void Update(TravelPackage t)
        {
            context.TravelPackages.Attach(t);
            context.Entry(t).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(TravelPackage t)
        {
            context.TravelPackages.Remove(this.Get(t.Id));
            context.SaveChanges();
        }

        public List<TravelPackage> Search(string[] args)
        {
            string arg0 = args[0].ToUpper();
            var list = context.TravelPackages.Include(o => o.CityDestination).Include(o => o.CityOrigin).Where(item => item.CityDestination.Name.ToUpper().Contains(arg0)).ToList();
            return list;
        }

        public List<TravelPackage> GetAllId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
