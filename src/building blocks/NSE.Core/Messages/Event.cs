using MediatR;
using System;

namespace NSE.Core.Messages
{
    public class Event : Message, INotification
    {
        protected DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
