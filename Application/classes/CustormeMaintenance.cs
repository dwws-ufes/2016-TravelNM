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
    public class CustormeMaintenance : IMaintenance<Customer>
    {
        TravelMNContext context = new TravelMNContext();
        public void Save(Customer t)
        {
            using (var context = new TravelMNContext())
            {
                context.Customers.Attach(t);
                context.Customers.Add(t);
                context.SaveChanges();
            }
        }

        public Customer Get(int id)
        {
            return context.Customers.Include(c => c.City).Where(item => item.Id.Equals(id)).FirstOrDefault();
        }

        public List<Customer> GetAll()
        {
            return context.Customers.Include(c => c.City).ToList();
        }

        public void Update(Customer t)
        {
            using (var context = new TravelMNContext())
            {
                context.Entry(t).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(Customer t)
        {
            context.Customers.Remove(this.Get(t.Id));
            context.SaveChanges();
        }

        public List<Customer> Search(string[] args)
        {
            string arg0 = args[0].ToUpper();
            var list = context.Customers.Include(c => c.City).Where(item => item.Name.ToUpper().Contains(arg0) | item.Email.ToUpper().Equals(arg0)).ToList();
            return list;
        }

        public List<Customer> GetAllId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
