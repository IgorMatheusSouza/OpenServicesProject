using Microsoft.AspNetCore.Mvc;
using OpenServices.Entities;
using OpenServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Controllers
{
    public class MensagemController : Controller
    {
        private readonly OpenServicesContextData OpenServicesContext;

        public MensagemController(OpenServicesContextData openServicesContext)
        {
            OpenServicesContext = openServicesContext;
        }

        public IActionResult Chat([FromQuery]int clienteId, int prestadorId, int usuarioEnvio) {
            var cliente = (Cliente)OpenServicesContext.Usuarios.FirstOrDefault(x => x.IdUsuario == clienteId);
            var prestador = (PrestadorServico)OpenServicesContext.Usuarios.FirstOrDefault(x => x.IdUsuario == prestadorId);
            ChatViewModel = new ChatViewModel { Cliente = cliente, Prestador = prestador, UsuarioLogado = usuarioEnvio == 1 ? (Usuario)prestador : cliente, UsuarioConversando = usuarioEnvio == 2 ? (Usuario)cliente : prestador };
            return View(ChatViewModel);
        }

        [HttpGet]
        public JsonResult GetMensagem()
        {
            var mensagens = OpenServicesContext.Mensagems.Where(x => x.IdReceiver == ChatViewModel.UsuarioLogado.IdUsuario || x.IdSender == ChatViewModel.UsuarioLogado.IdUsuario);
            var retorno = mensagens.Select(x => new { Data = x.Data.ToString("dd-MM-yyyy hh:mm"), x.Texto, Nome = x.IdSender == ChatViewModel.Cliente.IdUsuario ? ChatViewModel.Cliente.Nome : ChatViewModel.Prestador.Nome }).ToList();
            return Json(retorno);
        }

        [HttpPost]
        public JsonResult EnviarMensagem([FromBody]ChatViewModel chat)
        {
            var mensagem = new Mensagem().EnviarMensagem(chat.Msg, chat.Id , chat.Id != ChatViewModel.UsuarioConversando.IdUsuario ? ChatViewModel.UsuarioConversando.IdUsuario : ChatViewModel.UsuarioLogado.IdUsuario);
            OpenServicesContext.Mensagems.Add(mensagem);
            return Json(null);
        }

        private static ChatViewModel ChatViewModel;
    }
}
