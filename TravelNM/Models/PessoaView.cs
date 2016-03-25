using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelNM.Models
{
    public class PessoaView : Pessoa
    {
        public List<Estado> Estados { get; set; }

        public List<Cidade> Cidades { get; set; }
    }
}