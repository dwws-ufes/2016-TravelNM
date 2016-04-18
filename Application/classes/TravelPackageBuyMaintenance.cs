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
    public class TravelPackageBuyMaintenance : IMaintenance<TravelPackageBuy>
    {
        TravelMNContext context = new TravelMNContext();
        public void Save(TravelPackageBuy t)
        {
            context.TravelPackageBuys.Attach(t);
            context.TravelPackageBuys.Add(t);
            context.SaveChanges();
        }

        public TravelPackageBuy Get(int id)
        {
            return context.TravelPackageBuys.Include(t => t.TravelPackage).Include(c => c.Customer).Include(o => o.TravelPackage.CityOrigin).
                Include(d => d.TravelPackage.CityDestination).Where(item => item.Id == id).FirstOrDefault();
        }

        public List<TravelPackageBuy> GetAllId(int Id)
        {
            return context.TravelPackageBuys.Include(t => t.TravelPackage).Include(c => c.Customer).Include(o => o.TravelPackage.CityOrigin).
                Include(d => d.TravelPackage.CityDestination).Where(item => item.Customer.Id == Id).ToList();
        }

        public List<TravelPackageBuy> GetAll()
        {
            return context.TravelPackageBuys.Include(t => t.TravelPackage).Include(c => c.Customer).Include(o => o.TravelPackage.CityOrigin).
                Include(d => d.TravelPackage.CityDestination).ToList();
        }

        public void Update(TravelPackageBuy t)
        {
            context.Entry(t).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(TravelPackageBuy t)
        {
            context.TravelPackageBuys.Remove(this.Get(t.Id));
            context.SaveChanges();
        }

        public List<TravelPackageBuy> Search(string[] args)
        {
            string arg0 = args[0].ToUpper();
            var list = context.TravelPackageBuys.Include(t => t.TravelPackage).Include(c => c.Customer).
                Where(item => item.TravelPackage.CityDestination.Name.ToUpper().Contains(arg0) |
                    item.Customer.Name.ToUpper().Contains(arg0)).ToList();


            return list;
        }
    }
}
