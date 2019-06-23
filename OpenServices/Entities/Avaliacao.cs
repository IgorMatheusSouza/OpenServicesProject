using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Entities
{
    public class Avaliacao
    {
        public int Nota { get; set; }

        public string Descricao { get; set; }

        public DateTime Data { get; set; }

        public Servico Servico { get; set; }

        public Usuario Usuario { get; set; }
    }
}
