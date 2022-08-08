using FluentValidation.Results;
using MediatR;
using System;

namespace NSE.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public Command()
        {
            TimeStamp = DateTime.Now;
        }
        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }


        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
