using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Entities
{
    public class Servico
    {
        public string Descricao { get; set; }

        public Categoria Categoria { get; set; }

        public Cliente Cliente { get; set; }

        public string Status { get; set; }

        public List<Avaliacao> Avaliacoes = new List<Avaliacao>();

        public Servico Cadastrar(String descricao, Categoria categoria , Cliente cliente) {
            return new Servico { Descricao = descricao, Cliente = cliente, Categoria = categoria, Status = "andamento" };
        }

        public void Cancelar()
        {
            this.Status = "Cancelado";
        }
    }
}
