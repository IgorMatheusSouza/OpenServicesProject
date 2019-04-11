using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenServices.Entities;
using OpenServices.Models;
using System.Linq;

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
                return RedirectToAction("TipoPerfil", new { id = usuario.IdUsuario });
            }
            else
                return View(msg);
        }

        [HttpGet]
        [Route("Autenticacao/TipoPerfil/{IdUsuario}")]
        public ActionResult TipoPerfil(int IdUsuario)
        {
            var tipoPerfil = new TipoPerfilViewModel(IdUsuario);
            tipoPerfil.Categorias = OpenServicesContext.Categorias.ToList();
            tipoPerfil.Usuario = OpenServicesContext.Usuarios.FirstOrDefault(x => x.IdUsuario == IdUsuario);
            return View(tipoPerfil);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TipoPerfil(TipoPerfilViewModel tipoPerfil)
        {
            var usuario = (PrestadorServico)OpenServicesContext.Usuarios.Find(tipoPerfil.IdUsuario);
            usuario.Cnpj = tipoPerfil.Cnpj;
            usuario.Especializacao = tipoPerfil.Especializacao;
            usuario.Categorias.Add(new CategoriaPrestador { IdCategoria = tipoPerfil.CategoriaSelecionada });
            OpenServicesContext.SaveChanges();
            return View();
        }
    }
}