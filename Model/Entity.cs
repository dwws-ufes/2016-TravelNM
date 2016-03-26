using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Model
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
