using Model;

namespace InterfacesTravelMN
{
    public interface IAuthentication
    {
        User Login(User user);

        Customer LoginCustomer(Customer custumer);
    }
}
