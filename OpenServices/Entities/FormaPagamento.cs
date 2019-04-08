using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Entities
{
	public class FormaPagamento
	{
		[Key]
		public int IdFormaPagamento { get; set; }
		public string Tipo { get; set; }
	}
}
