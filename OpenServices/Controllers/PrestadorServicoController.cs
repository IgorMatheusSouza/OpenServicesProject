using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenServices.Entities;

namespace OpenServices.Controllers
{
    public class PrestadorServicoController : Controller
    {
        private static Usuario usuarioLogado;
        public IActionResult SolicitacaoServico(Usuario usuario)
        {
            usuarioLogado = usuario;
            return View(usuario);
        }

        [HttpPost]
        public IActionResult SolicitacaoServico()
        {
            return View();
        }


        public IActionResult AguardandoCliente()
        {
            return View(usuarioLogado);
        }
        
    }
}