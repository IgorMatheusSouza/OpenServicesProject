using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Entities
{
	public class Usuario
	{
		[Key]
		public int IdUsuario { get; set; }

		public string Nome { get; set; }

		public string Cpf { get; set; }

		public string Rg { get; set; }

		public DateTime DataNascimento { get; set; }
	}
}
