using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Application.Controllers
{
    public class PessoaController : Controller
    {
        public ActionResult Index()
        {
            List<Pessoa> pessoas = (new ManutencaoPessoa()).ObterPessoas();
            return View(pessoas);
        }

        public ActionResult Edit(int id, Pessoa pessoa)
        {
            return View(new ManutencaoPessoa().ObterPessoaId(id, pessoa));
        }

        [HttpPost]
        public ActionResult Edit(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                new ManutencaoPessoa().AtualizarPessoa(pessoa);
                return RedirectToAction("Index");
            }
            else
                return View(pessoa);
        }

        public ActionResult Delete(int id, Pessoa pessoa)
        {
            return View(new ManutencaoPessoa().ObterPessoaId(id, pessoa));
        }

        [HttpPost]
        public ActionResult Delete(Pessoa pessoa, int id)
        {
            new ManutencaoPessoa().ExcluirPessoa(pessoa, id);
            return RedirectToAction("Index");
        }

        public ActionResult Create(int codigoEstado = 1)
        {
            EstadoController estadocontroller = new EstadoController();
            CidadeController cidadecontroller = new CidadeController();

            ViewBag.bagEstados = new SelectList(estadocontroller.ObterEstados(), "Codigo", "Nome");
            ViewBag.bagCidades = new SelectList(cidadecontroller.ObterCidadesId(codigoEstado), "Codigo", "Nome");

            return View();
        }

        public ActionResult ddlEstadoChange(int codigoEstado)
        {
            EstadoController estadocontroller = new EstadoController();
            ViewBag.bagEstados = new SelectList(estadocontroller.ObterEstadosId(codigoEstado), "Codigo", "Nome");

            CidadeController cidadecontroller = new CidadeController();
            ViewBag.bagCidades = new SelectList(cidadecontroller.ObterCidadesId(codigoEstado), "Codigo", "Nome");

            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Pessoa pessoa)
        {
            new ManutencaoPessoa().AdicionarPessoa(pessoa);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id, Pessoa pessoa)
        {
            return View(new ManutencaoPessoa().ObterPessoaId(id, pessoa));
        }
    }
}
