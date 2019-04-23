using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Models
{
    public class TipoPagamentoViewModel
    {
        public TipoPagamentoViewModel()
        {
        }

        public TipoPagamentoViewModel(int idUsuario)
        {
            IdUsuario = idUsuario;
        }

        public int IdUsuario { get; set; }

        public string NomeUsuario { get; set; }

        public bool PermiteCartaoCredito { get; set; }

        public bool PermiteCartaoDebito { get; set; }

        public bool PermiteDinheiro { get; set; }
    }
}
