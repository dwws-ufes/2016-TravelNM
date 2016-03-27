using InterfacesTravelMN;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace ApplicationTravelMN.classes
{
    public class TravelPackageMaintenance : IMaintenance<TravelPackage>
    {
        TravelMNContext context = new TravelMNContext();
        public void Save(TravelPackage t)
        {
            throw new NotImplementedException();
        }

        public TravelPackage Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<TravelPackage> GetAll()
        {
            return context.TravelPackages.Include(o => o.CityOrigin).Include(d => d.CityDestination).ToList();
        }

        public void Update(TravelPackage t)
        {
            throw new NotImplementedException();
        }

        public void Delete(TravelPackage t)
        {
            throw new NotImplementedException();
        }

        public List<TravelPackage> Search(string[] args)
        {
            string arg0 = args[0].ToUpper();
            var list = context.TravelPackages.Include(o => o.CityDestination).Include(o => o.CityOrigin).Where(item => item.CityDestination.Name.ToUpper().Contains(arg0)).ToList();
            return list;
        }
    }
}
