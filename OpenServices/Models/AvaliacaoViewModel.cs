using OpenServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Models
{
    public class AvaliacaoViewModel
    {
        public Usuario usuario { get; set; }

        public int Nota { get; set; }

        public string Descricao { get; set; }

    }
}
