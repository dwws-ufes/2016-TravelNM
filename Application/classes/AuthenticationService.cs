using InterfacesTravelMN;
using Model;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationTravelMN.classes
{
    public class AuthenticationService : IAuthentication
    {
        TravelMNContext context = new TravelMNContext();

        public User Login(User user)
        {
            return context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
        }
    }
}
