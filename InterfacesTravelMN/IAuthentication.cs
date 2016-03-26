using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfacesTravelMN
{
    public interface IAuthentication
    {
        User Login(User user);
    }
}
