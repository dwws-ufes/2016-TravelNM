using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model
{
    public class City : Entity
    {  
        public string Name { get; set; }
        
        public string State { get; set; }
    }
}
