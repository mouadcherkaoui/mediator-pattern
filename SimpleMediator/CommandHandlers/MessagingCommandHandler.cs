using Experimental.System.Messaging;
using MediatorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMediator.CommandHandlers
{
    public class MessagingCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        public void HandleCommad(TCommand command)
        {
            var queueName = @$".\private$\mediatr";
            var message = new Message(command);
            message.Label = command.GetType().Name.ToLower();


            if (!MessageQueue.Exists(queueName))
            {
                MessageQueue.Create(queueName);
            }

            MessageQueue queue = new MessageQueue(queueName);

            queue.Send(message);
        }
    }
}
