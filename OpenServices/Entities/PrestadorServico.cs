using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Entities
{
	public class PrestadorServico : Usuario
	{
		public PrestadorServico()
		{
            CategoriasPrestador = new HashSet<CategoriaPrestador>();
			FormaPagamentos = new HashSet<FormaPagamento>();
            Categorias = new List<Categoria>();
        }

		public string Cnpj { get; set; }
		public string Especializacao { get; set; }
		public ICollection<CategoriaPrestador> CategoriasPrestador { get; set; }
		public ICollection<FormaPagamento> FormaPagamentos { get; set; }

        public ICollection<Categoria> Categorias { get; set; }

        public List<Categoria> SelecionarCategorias()
        {
            return Categorias.ToList();
        }

        public Categoria SelecionarCategorias(string nome)
        {
            return Categorias.FirstOrDefault(x=> x.Nome == nome);
        }
    }
}
