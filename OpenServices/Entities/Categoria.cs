using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenServices.Entities
{
	public class Categoria
	{
		[Key]
		public int IdCategoria { get; set; }
		public string Nome { get; set; }
		public ICollection<CategoriaPrestador> PrestadoresServico { get; set; }
	}
}
