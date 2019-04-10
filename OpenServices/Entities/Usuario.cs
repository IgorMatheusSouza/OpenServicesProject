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

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Cpf { get; set; }

		public string Rg { get; set; }

		public DateTime? DataNascimento { get; set; }


        public bool validarNovoUsuario(out String msg, OpenServicesContext openServicesContext) {
            msg = null;
            if (openServicesContext.Usuarios.Any(x => x.Cpf == this.Cpf))
                msg = "Usuário já cadastrado";

            return msg == null;
        }
	}
}
