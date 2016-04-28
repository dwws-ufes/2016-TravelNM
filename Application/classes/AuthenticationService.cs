using InterfacesTravelMN;
using Model;
using Persistence;
using System.Linq;

namespace ApplicationTravelMN.classes
{
    public class AuthenticationService : IAuthentication
    {
        TravelMNContext context = new TravelMNContext();

        public User Login(User user)
        {
            return context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
        }

        public Customer LoginCustomer(Customer customer)
        {
            return context.Customers.Where(x => x.Email == customer.Email && x.Password == customer.Password).FirstOrDefault();
        }
    }
}
