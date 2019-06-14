using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Entities
{
    public class Mensagem
    {
        public string Texto { get; set; }

        public DateTime Data { get; set; }

        public bool Visualizada { get; set; }

        public int IdSender{ get; set; }

        public int IdReceiver { get; set; }

        public Cliente Cliente { get; set; }

        public PrestadorServico PrestadorServico { get; set; }

        public Mensagem EnviarMensagem(string msg, int idUsuario, int idUsuarioConversando)
        {
            Texto = msg;
            IdSender = idUsuario;
            IdReceiver = idUsuarioConversando;
            Data = DateTime.Now;

            return this;
        }
    }
}
