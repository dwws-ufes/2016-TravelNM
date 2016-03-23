using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;

namespace Application.Controllers
{
    public class CidadeController : Controller
    {
        public List<Cidade> ObterCidadesId(int codigoEstado)
        {
            var cidades = new ManutencaoCidade().ObterCidadesIdEstado(codigoEstado).ToList();
            return cidades;
        }
    }   
}
