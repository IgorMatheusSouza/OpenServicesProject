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
			Categorias = new HashSet<CategoriaPrestador>();
			FormaPagamentos = new HashSet<FormaPagamento>();
		}

		public string Cnpj { get; set; }
		public string Especializacao { get; set; }
		public ICollection<CategoriaPrestador> Categorias { get; set; }
		public ICollection<FormaPagamento> FormaPagamentos { get; set; }
	}
}
