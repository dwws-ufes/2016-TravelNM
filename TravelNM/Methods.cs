using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace TravelNM
{
    public class Methods
    {
        public string GenHashSalt(string Password, string Salt)
        {
            var HashedPassword = (Password + Salt);

            return  Crypto.Hash(HashedPassword, "MD5");
        }
    }
}