using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Entities
{
    public class OpenServicesContextData
    {
        public List<Categoria> Categorias = CategoriasData;

        public List<Usuario> Usuarios = UsuariosData;
        public List<Cliente> Clientes { get; set; }
        public List<PrestadorServico> PrestadorServicos { get; set; }
        public List<FormaPagamento> FormaPagamentos { get; set; }
        public List<CategoriaPrestador> CategoriaPrestadors { get; set; }

        public List<Avaliacao> Avaliacoes = AvaliacaoData;

        public List<Mensagem> Mensagems = MensagemsData;

        public List<Servico> Servicos = ServicoData;

        private static List<Categoria> CategoriasData = new List<Categoria> { new Categoria { Nome = "Hidráulica", IdCategoria = 1 }, new Categoria { Nome = "Pintura", IdCategoria = 2 }, new Categoria { Nome = "Serviços domésticos", IdCategoria = 3 }, new Categoria { Nome = "Babá", IdCategoria = 4 } };

        private static List<Usuario> UsuariosData = new List<Usuario> { new Cliente { Nome = "Igor Matheus", Email = "igor.dorbacao@uniriotec.br", Senha = "123456", IdUsuario = 6 }, new PrestadorServico { Nome = "Matheus Carvalho", Email = "matheus.moreira@uniriotec.br", Senha = "123456", IdUsuario = 7, Categorias = { CategoriasData.ElementAt(0) } } };

        private static List<Servico> ServicoData = new List<Servico>();

        private static List<Mensagem> MensagemsData = new List<Mensagem>();

        public static List<Avaliacao> AvaliacaoData = new List<Avaliacao>();

    }
}
