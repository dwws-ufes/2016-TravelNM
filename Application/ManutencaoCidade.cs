using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Model;

namespace Application
{
    public class ManutencaoCidade
    {
        TravelMNContext db = new TravelMNContext();

        public List<Cidade> ObterCidadesIdEstado(int id)
        {
            var cidades = db.Cidade.Where( c => c.codigoEstado == id).ToList();
            return cidades;
        }
    }
}
