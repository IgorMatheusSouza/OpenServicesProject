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
        private readonly OpenServicesContextData OpenServicesContext;

        public AutenticacaoController(OpenServicesContextData openServicesContext)
        {
            OpenServicesContext = openServicesContext;
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            var usuarioLog = OpenServicesContext.Usuarios.FirstOrDefault(x => x.Email == usuario.Email && x.Senha == usuario.Senha);

            if (usuarioLog != null && usuarioLog.GetType() == typeof(PrestadorServico))
                return RedirectToAction("SolicitacaoServico", "PrestadorServico", usuario);

            if (usuarioLog != null)
                return RedirectToAction("LogadoCliente", "Servico", usuario);

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

            OpenServicesContext.Usuarios.Add(usuario);
            return RedirectToAction("TipoPerfil", new { id = usuario.IdUsuario });

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
            var usuario = OpenServicesContext.Usuarios.FirstOrDefault(x => x.IdUsuario == tipoPerfil.IdUsuario);

            var prestador = this.TransformarUsuario(usuario);
            prestador.Cnpj = tipoPerfil.Cnpj;
            prestador.Especializacao = tipoPerfil.Especializacao;
            prestador.CategoriasPrestador.Add(new CategoriaPrestador { IdCategoria = tipoPerfil.CategoriaSelecionada });
            OpenServicesContext.PrestadorServicos.Add(prestador);

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