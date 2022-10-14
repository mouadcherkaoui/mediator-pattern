using Experimental.System.Messaging;
using MediatorPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern.CommandHandlers
{
    public class MessagingCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        public void Handle(TCommand command)
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

        CommandResult ICommandHandler<TCommand>.Handle(TCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
