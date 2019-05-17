using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenServices.Entities;

namespace OpenServices.Controllers
{
    public class ServicoController : Controller
    {

        private static Usuario usuarioLogado;

        public IActionResult Solicitar(Usuario usuario)
        {
            usuarioLogado = usuario;
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Solicitar(string servico)
        {
            return RedirectToAction("BuscarPrestador", usuarioLogado);
        }

        public IActionResult BuscarPrestador(string servico)
        {
            return View(usuarioLogado);
        }

        public IActionResult ConfirmarPrestador()
        {
            return View(usuarioLogado);
        }

    }
}