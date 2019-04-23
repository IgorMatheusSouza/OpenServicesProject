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
	
	public class AuthenticationController : Controller
	{
		public OpenServicesContext OpenServicesContext { get; set; }

        [Route("api/CadastrarUsuario")]
        [HttpPost]
		public JsonResult CadastrarUsuario([FromBody]Usuario usuario) {

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

        [Route("api/GetUsuario")]
        [HttpGet]
        public JsonResult GetUsuario(Usuario usuario)
        {

            try
            {
                OpenServicesContext.Usuarios.Add(usuario);
                OpenServicesContext.SaveChanges();
                return Json("Usuário cadastrado");
            }
            catch (Exception)
            {
                return Json("Erro ao cadastrar usuário");
            }
        }
    }
}