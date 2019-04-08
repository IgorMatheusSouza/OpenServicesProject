using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Entities
{
	public class CategoriaPrestador
	{
		[Key]
		public int IdCategoriaPrestador { get; set; }

		public int IdUsuario { get; set; }

		public int IdCategoria { get; set; }

		public PrestadorServico PrestadorServico { get; set; }

		public Categoria Categoria { get; set; }
	}
}
