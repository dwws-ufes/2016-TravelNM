using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Model;

namespace Application
{
    public class ManutencaoPessoa
    {
        TravelMNContext db = new TravelMNContext();

        public List<Pessoa> ObterPessoas()
        {
            var pessoas = db.Pessoa.ToList();
            return pessoas;
        }

        public Pessoa ObterPessoaId(int id, Pessoa pessoa)
        {
            pessoa = db.Pessoa.Find(id);
            return pessoa;
        }

        public void AtualizarPessoa(Pessoa pessoa)
        {
            db.Entry(pessoa).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void ExcluirPessoa(Pessoa pessoa, int id)
        {
            var pessoas = db.Pessoa.Find(id);
            db.Entry(pessoas).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public void AdicionarPessoa(Pessoa pessoa)
        {
            db.Entry(pessoa).State = EntityState.Added;
            db.SaveChanges();
        }
    }
}
