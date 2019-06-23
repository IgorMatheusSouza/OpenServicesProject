using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenServices.Entities;
using OpenServices.Models;

namespace OpenServices.Controllers
{
    public class PrestadorServicoController : Controller
    {
        private static Usuario usuarioLogado;

        private static SolicitacaoServicoViewModel solicitacaoServicoViewModel;

        private readonly OpenServicesContextData OpenServicesContext;

        public PrestadorServicoController(OpenServicesContextData openServicesContext)
        {
            OpenServicesContext = openServicesContext;
        }

        public IActionResult SolicitacaoServico(Usuario usuario)
        {
            var prestador = (PrestadorServico)OpenServicesContext.Usuarios.FirstOrDefault(x => x.Email == usuario.Email);
            usuarioLogado = prestador;
            var servico = OpenServicesContext.Servicos.FirstOrDefault();
            if (servico == null)
                return RedirectToAction("LogadoPrestador");
            var categoriaPrestador = prestador.SelecionarCategorias(servico.Categoria.Nome);
            if (categoriaPrestador != null)
            {
                solicitacaoServicoViewModel = new SolicitacaoServicoViewModel { Email = usuarioLogado.Email, Servico = servico };
                return View(solicitacaoServicoViewModel);
            }
            return View(usuario);
        }

        public IActionResult AguardandoCliente()
        {
            var servico = OpenServicesContext.Servicos.FirstOrDefault();
            if (servico == null)
                return RedirectToAction("LogadoPrestador");
            return View(solicitacaoServicoViewModel);
        }

      
        public IActionResult Chat()
        {
            var objeto = new { clienteId = solicitacaoServicoViewModel.Servico.Cliente.IdUsuario, prestadorId = usuarioLogado.IdUsuario, usuarioEnvio = 1 };
            return RedirectToAction("Chat","Mensagem", objeto);
        }

        public IActionResult LogadoPrestador()
        {
            return View(usuarioLogado);
        }

        public IActionResult DadosPrestador(int id)
        {
            var prestador = (PrestadorServico)OpenServicesContext.Usuarios.FirstOrDefault(x => x.IdUsuario == id);
            return View(prestador);
        }

        public IActionResult Avaliar()
        {
            return View(new AvaliacaoViewModel { usuario = usuarioLogado });
        }

        [HttpPost]
        public IActionResult Avaliar(AvaliacaoViewModel avaliacaoViewModel)
        {
            var servico = OpenServicesContext.Servicos.FirstOrDefault();
            if(servico != null)
                servico.Status = "Finalizado";

            var avaliacao = new Avaliacao { Descricao = avaliacaoViewModel.Descricao, Nota = avaliacaoViewModel.Nota, Servico = servico , Data = DateTime.Now, Usuario = usuarioLogado };
            OpenServicesContext.Avaliacoes.Add(avaliacao);
            servico.Avaliacoes.Add(avaliacao); 

            return RedirectToAction("LogadoPrestador");
        }

        public IActionResult HistoricoServicos()
        {
            var servicos = OpenServicesContext.Servicos.ToList();
            return View(new HistoricoServicoModel { Usuario = usuarioLogado, Servicos = servicos });
        }
    }
}