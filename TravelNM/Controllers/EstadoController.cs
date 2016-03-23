using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace Application.Controllers
{
    public class EstadoController : Controller
    {
        public List<Estado> ObterEstados()
        {
            var estados = new ManutencaoEstado().ObterEstados().ToList();
            return estados;
        }

        public List<Estado> ObterEstadosId(int codigoEstado)
        {
            var estados = new ManutencaoEstado().ObterEstadoId(codigoEstado).ToList();
            return estados;
        }
    }   
}
