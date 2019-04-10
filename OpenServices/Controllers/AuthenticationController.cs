using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenServices.Entities;

namespace OpenServices.Controllers
{
	[Produces("application/json")]
	[Route("api/Authentication")]
	public class AuthenticationController : Controller
	{
		public OpenServicesContext OpenServicesContext { get; set; }

		[HttpPost]
		public JsonResult CadastrarUsuario(Usuario usuario) {

            try
            {
                OpenServicesContext.Usuarios.Add(usuario);
                OpenServicesContext.SaveChanges();
                return Json("Usuário cadastrado");
            }
            catch (Exception) {
                return Json("Erro ao cadastrar usuário");
            }
		}
    }
}