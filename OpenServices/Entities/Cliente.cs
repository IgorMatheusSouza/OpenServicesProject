﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Entities
{
	public class Cliente : Usuario
	{
		public string Endereco { get; set; }
	}
}
