using OpenServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Models
{
    public class HistoricoServicoModel
    {
        public Usuario Usuario { get; set; }

        public List<Servico> Servicos { get; set; }
    }
}
