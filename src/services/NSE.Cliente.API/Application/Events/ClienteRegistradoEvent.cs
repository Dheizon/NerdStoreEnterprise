using NSE.Core.Messages;
using System;

namespace NSE.Cliente.API.Application.Events
{
    public class ClienteRegistradoEvent : Event
    {
        public ClienteRegistradoEvent(Guid id, string nome, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = email;            
        }

        public Guid Id { get; set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        
    }
}
