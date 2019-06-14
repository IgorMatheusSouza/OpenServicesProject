using OpenServices.Entities;
using System.Collections.Generic;

namespace OpenServices.Models
{
    public class BuscarPrestadosViewModel
    {
        public string Categoria { get; set; }

        public string Email { get; set; }

        public List<PrestadorServico> Usuarios { get; set; }
    }
}
