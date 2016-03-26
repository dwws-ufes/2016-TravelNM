using InterfacesTravelMN;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }
    }
}
