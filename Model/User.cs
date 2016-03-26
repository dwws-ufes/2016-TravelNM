using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model
{
    public class User : Entity
    {  
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
