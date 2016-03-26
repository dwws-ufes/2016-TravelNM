//using ApplicationTravelMN;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TravelNM.Models;

namespace Application.Controllers
{
    public class PessoaController : Controller
    {
        //public ActionResult Index()
        //{
        //    List<Pessoa> pessoas = (new ManutencaoPessoa()).ObterPessoas();
        //    return View(pessoas);
        //}

        //public ActionResult Edit(int id, Pessoa pessoa)
        //{
        //    return View(new ManutencaoPessoa().ObterPessoaId(id, pessoa));
        //}

        //[HttpPost]
        //public ActionResult Edit(Pessoa pessoa)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        new ManutencaoPessoa().AtualizarPessoa(pessoa);
        //        return RedirectToAction("Index");
        //    }
        //    else
        //        return View(pessoa);
        //}

        //public ActionResult Delete(int id, Pessoa pessoa)
        //{
        //    return View(new ManutencaoPessoa().ObterPessoaId(id, pessoa));
        //}

        //[HttpPost]
        //public ActionResult Delete(Pessoa pessoa, int id)
        //{
        //    new ManutencaoPessoa().ExcluirPessoa(pessoa, id);
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Create(PessoaView pessoaview, int codigoEstado = 1)
        //{
        //    EstadoController estadocontroller = new EstadoController();
        //    CidadeController cidadecontroller = new CidadeController();

        //    estadocontroller.ObterEstados(pessoaview);
        //    cidadecontroller.ObterCidadesId(pessoaview, codigoEstado);

        //    return View(pessoaview);
        //}

        //public JsonResult getCidadesEstado(PessoaView pessoaview, int Id)
        //{
        //    CidadeController cidadecontroller = new CidadeController(); 

        //    return Json(new SelectList(cidadecontroller.ObterCidadesId(pessoaview, Id),"Codigo","Nome"), JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public ActionResult Create(Pessoa pessoa)
        //{
        //    new ManutencaoPessoa().AdicionarPessoa(pessoa);
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Details(int id, Pessoa pessoa)
        //{
        //    return View(new ManutencaoPessoa().ObterPessoaId(id, pessoa));
        //}
    }
}
