using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Model;
using Persistence;

namespace ApplicationTravelMN
{
    public class ManutencaoEstado
    {
        TravelMNContext db = new TravelMNContext();

        public List<Estado> ObterEstados()
        {
            var estados = db.Estado.ToList();
            return estados;
        }
    }
}
