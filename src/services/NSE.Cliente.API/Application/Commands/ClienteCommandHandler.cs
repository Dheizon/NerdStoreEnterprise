using FluentValidation.Results;
using MediatR;
using NSE.Cliente.API.Application.Events;
using NSE.Cliente.API.Model;
using NSE.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.Cliente.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        private readonly IClientRepository _clienteRepository;

        public ClienteCommandHandler(IClientRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = new NSE.Cliente.API.Model.Cliente(message.Id, message.Nome, message.Email, message.Cpf);

            //Validadores de negócio
            var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

            //Persistir no banco
            if (clienteExistente != null)
            {
                AdicionarErro("Este cpf já esta em uso");
                return ValidationResult;
            }

            _clienteRepository.Adicionar(cliente);

            // lançar um evento cliente ok! Ex.: Enviar um E-mail
            cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

            return await PersistirDados(_clienteRepository.UnityOfWork);
        }
    }
}
