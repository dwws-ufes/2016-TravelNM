using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using TravelNM.Models;

namespace Application.Controllers
{
    public class CidadeController : Controller
    {
        public List<Cidade> ObterCidadesId(PessoaView pessoaview, int codigoEstado)
        {
            pessoaview.Cidades = new ManutencaoCidade().ObterCidadesIdEstado(codigoEstado).ToList();
            return pessoaview.Cidades;
        }
    }   
}
