using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenServices.Entities;
using OpenServices.Models;

namespace OpenServices.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly OpenServicesContext OpenServicesContext;

        public AutenticacaoController(OpenServicesContext openServicesContext)
        {
            OpenServicesContext = openServicesContext;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro(Usuario usuario)
        {
            var msg = string.Empty;
            if (usuario.validarNovoUsuario(out msg, OpenServicesContext))
            {
                OpenServicesContext.Usuarios.Add(usuario);
                OpenServicesContext.SaveChanges();
                return RedirectToAction("TipoPefil", new { id = usuario.IdUsuario });
            }
            else
                return View(msg);

        }

        public ActionResult TipoPefil(int IdUsuario)
        {
            return View(new TipoPerfilViewModel(IdUsuario));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TipoPefil(TipoPerfilViewModel tipoPerfil)
        {
            var msg = string.Empty;
            if (tipoPerfil.IsPrestadorServico)
            {
                var usuario = (PrestadorServico)OpenServicesContext.Usuarios.Find(tipoPerfil.IdUsuario);
                usuario.Cnpj = tipoPerfil.Cnpj;
                usuario.Especializacao = tipoPerfil.Especializacao;
                return View();
            }
            else
                return View(msg);

        }
    }
}