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
    public class UserMaintenance : IMaintenance<User>
    {
        TravelMNContext context = new TravelMNContext();
        public void Save(User t)
        {
            context.Users.Add(t);
            context.SaveChanges();
        }

        public User Get(int id)
        {
            return context.Users.Find(id);
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public void Update(User t)
        {
            context.Entry(t).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(User t)
        {
            context.Users.Remove(this.Get(t.Id));
            context.SaveChanges();
        }


        public List<User> Search(string[] args)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
