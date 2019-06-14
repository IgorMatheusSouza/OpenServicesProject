using OpenServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenServices.Models
{
    public class ChatViewModel
    {

        public Cliente  Cliente { get; set; }

        public PrestadorServico Prestador { get; set; }

        public Usuario UsuarioLogado { get; set; }

        public Usuario UsuarioConversando { get; set; }

        public string Msg { get; set; }

        public int Id { get; set; }
    }
}
