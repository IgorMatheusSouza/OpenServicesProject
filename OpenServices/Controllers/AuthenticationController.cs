using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using OpenServices.Entities;

namespace OpenServices.Controllers
{
    [Produces("application/json")]

    public class AuthenticationController : Controller
    {
        public OpenServicesContext OpenServicesContext { get; set; }

        public AuthenticationController(OpenServicesContext openServicesContext)
        {
            OpenServicesContext = openServicesContext;
        }

        [Route("api/CadastrarUsuario")]
        [HttpPost]
        public JsonResult CadastrarUsuario([FromBody]Usuario usuario)
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

        [Route("api/recuperarSenha")]
        [HttpPost]
        public JsonResult RecuperarSenha([FromBody] Usuario userEmail)
        {
            var email = userEmail.Email;
            var user = OpenServicesContext.Usuarios.FirstOrDefault(x => x.Email.Contains(email));
            if (user == null)
                return Json(false);

            try
            {
                var novaSenha = new Random().Next(10000, 99999);
                var message = new MimeMessage();
                message.To.Add(new MailboxAddress("support@OpenService.com", user.Email));
                message.From.Add(new MailboxAddress("support@OpenService.com", "support@OpenService.com"));
                message.Subject = "Recuperação de senha";
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = $"Olá {user.Nome}, sua nova senha é {novaSenha}"
                };
                using (var emailClient = new SmtpClient())
                {
                    emailClient.Connect("smtp.gmail.com", 587, false);
                    emailClient.Authenticate("igormatheus.14.11@gmail.com", "nkgyzgcdnxwyocer");
                    emailClient.Send(message);
                    emailClient.Disconnect(true);
                }
                user.Senha = novaSenha.ToString();
                OpenServicesContext.SaveChanges();
                return Json(true);
            }
            catch (Exception ex)
            {
                ModelState.Clear();
                ViewBag.Message = $" Oops! We have a problem here {ex.Message}";
                return Json(false);
            }
        }

        [Route("api/realizarLogin")]
        [HttpPost]
        public JsonResult RealizarLogin([FromBody]Usuario credenciasLogin)
        {
            var user = OpenServicesContext.Usuarios.FirstOrDefault(x => x.Email.Contains(credenciasLogin.Email) && x.Senha.Contains(credenciasLogin.Senha));
            return user != null ? Json(user.IdUsuario) : Json(null);
        }

        [Route("api/getUsuario/{idUser}")]
        [HttpGet]
        public JsonResult GetUsuario(int idUser)
        {
            return Json(OpenServicesContext.Usuarios.Find(idUser));
        }
    }
}