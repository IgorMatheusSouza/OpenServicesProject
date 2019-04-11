using OpenServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Models
{
    public class TipoPerfilViewModel
    {
        public TipoPerfilViewModel()
        {

        }
        public TipoPerfilViewModel(int idUsuario)
        {
            IdUsuario = idUsuario;
        }

        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public bool IsPrestadorServico { get; set; }

        public string Cnpj { get; set; }

        public string Especializacao { get; set; }

        public List<Categoria> Categorias { get; set; }

        public int CategoriaSelecionada { get; set; }
    }
}
