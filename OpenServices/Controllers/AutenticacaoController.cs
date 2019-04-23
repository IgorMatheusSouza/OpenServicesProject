using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenServices.Entities;
using OpenServices.Models;
using System;
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
            var usuario = OpenServicesContext.Usuarios.Find(tipoPerfil.IdUsuario);

            var prestador = this.TransformarUsuario(usuario);
            prestador.Cnpj = tipoPerfil.Cnpj;
            prestador.Especializacao = tipoPerfil.Especializacao;
            prestador.Categorias.Add(new CategoriaPrestador { IdCategoria = tipoPerfil.CategoriaSelecionada });
            OpenServicesContext.PrestadorServicos.Add(prestador);
            OpenServicesContext.SaveChanges();

            return RedirectToAction("TipoPagamento", new { id = prestador.IdUsuario });
        }

        [HttpGet]
        [Route("Autenticacao/TipoPagamento/{IdUsuario}")]
        public ActionResult TipoPagamento(int IdUsuario)
        {
            var tipoPagamento = new TipoPagamentoViewModel(IdUsuario);
            tipoPagamento.NomeUsuario = OpenServicesContext.PrestadorServicos.FirstOrDefault(x => x.IdUsuario == IdUsuario).Nome;
            return View(tipoPagamento);
        }

        [HttpPost]
        [Route("Autenticacao/TipoPagamento/{IdUsuario}")]
        public ActionResult TipoPagamento(TipoPagamentoViewModel tipoPagamentoView)
        {

            var prestador = OpenServicesContext.PrestadorServicos.FirstOrDefault(x => x.IdUsuario == tipoPagamentoView.IdUsuario);

            if (tipoPagamentoView.PermiteCartaoCredito)
                prestador.FormaPagamentos.Add(new FormaPagamento { Tipo = EnumTipoPagamento.CartaoCredito });

            if (tipoPagamentoView.PermiteCartaoDebito)
                prestador.FormaPagamentos.Add(new FormaPagamento { Tipo = EnumTipoPagamento.CartaoDebito });

            if (tipoPagamentoView.PermiteDinheiro)
                prestador.FormaPagamentos.Add(new FormaPagamento { Tipo = EnumTipoPagamento.Dinheiro });

            OpenServicesContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private PrestadorServico TransformarUsuario(Usuario usuario)
        {
            var prestador = new PrestadorServico
            {
                Nome = usuario.Nome,
                Cpf = usuario.Cpf,
                Senha = usuario.Senha,
                Email = usuario.Email,
                Rg = usuario.Rg,
                DataNascimento = usuario.DataNascimento

            };
            OpenServicesContext.Usuarios.Remove(usuario);
            return prestador;
        }
    }
}