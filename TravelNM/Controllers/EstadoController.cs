using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using TravelNM.Models;

namespace Application.Controllers
{
    public class EstadoController : Controller
    {
        public List<Estado> ObterEstados(PessoaView pessoaview)
        {
           // pessoaview.Estados = new ManutencaoEstado().ObterEstados().ToList();
          //  return pessoaview.Estados;
            return null;
        }
    }   
}
