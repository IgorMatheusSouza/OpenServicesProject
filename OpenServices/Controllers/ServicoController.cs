using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenServices.Entities;
using OpenServices.Models;

namespace OpenServices.Controllers
{
    public class ServicoController : Controller
    {
        private static Usuario usuarioLogado;

        private readonly OpenServicesContextData OpenServicesContext;

        public ServicoController(OpenServicesContextData openServicesContext)
        {
            OpenServicesContext = openServicesContext;
        }
        public IActionResult Solicitar(Usuario usuario)
        {
            usuarioLogado = usuario;
            var response = new SolicitarServicoModel { Email = usuario.Email };
            return View(response);
        }

        [HttpPost]
        public IActionResult Solicitar(SolicitarServicoModel servico)
        {
            var cliente = (Cliente)OpenServicesContext.Usuarios.FirstOrDefault(x => x.Email == servico.Email);
            var categoria = OpenServicesContext.Categorias.FirstOrDefault(x => x.Nome == servico.NomeCategoria);
            var servicoCadastro = cliente.RequesitarServico(servico.Descricao, categoria);
            OpenServicesContext.Servicos.Add(servicoCadastro);
            return RedirectToAction("BuscarPrestador", categoria);
        }

        public IActionResult BuscarPrestador(Categoria categoria)
        {
            var prestadores = OpenServicesContext.Usuarios.Where(x=> x.GetType() == typeof(PrestadorServico)).Select(x=> (PrestadorServico)x).ToList();
            var response = new BuscarPrestadosViewModel { Email = usuarioLogado.Email, Categoria = categoria.Nome, Usuarios = prestadores.Where(x => x.Categorias.Any(k => k.Nome == categoria.Nome)).ToList() };
            return View(response);
        }

        public IActionResult ConfirmarPrestador()
        {
            return View(usuarioLogado);
        }

        public IActionResult LogadoCliente(Usuario usuario)
        {
            usuarioLogado = usuario;
            return View(usuarioLogado);
        }

        public IActionResult Chat()
        {
            var objeto = new { clienteId = 6, prestadorId = 7, usuarioEnvio = 2 };
            return RedirectToAction("Chat", "Mensagem", objeto);
        }

        [HttpGet]
        public JsonResult Cancelar()
        {
            new Servico().Cancelar();
            OpenServicesContext.Servicos.RemoveAll(x=> true);
            return Json(null);
        }

        public IActionResult Avaliar()
        {
            return View(new AvaliacaoViewModel { usuario = usuarioLogado });
        }

        [HttpPost]
        public IActionResult Avaliar(AvaliacaoViewModel avaliacaoViewModel)
        {
            var servico = OpenServicesContext.Servicos.FirstOrDefault();
            servico.Status = "Finalizado";

            var avaliacao = new Avaliacao { Descricao = avaliacaoViewModel.Descricao, Nota = avaliacaoViewModel.Nota, Servico = servico, Data = DateTime.Now, Usuario = usuarioLogado };
            OpenServicesContext.Avaliacoes.Add(avaliacao);
            servico.Avaliacoes.Add(avaliacao);
            return RedirectToAction("LogadoCliente", usuarioLogado);
        }

        public IActionResult HistoricoServicos()
        {
            var servicos = OpenServicesContext.Servicos.Where(x => x.Cliente.Email == usuarioLogado.Email).ToList();
            return View(new HistoricoServicoModel { Usuario = usuarioLogado , Servicos = servicos});
        }
    }
}