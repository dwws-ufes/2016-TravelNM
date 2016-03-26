using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Estado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Codigo { get; set; }

        public String Nome { get; set; }

        public String Abreviacao { get; set; }

        public int codigoPais { get; set; }
    }
}