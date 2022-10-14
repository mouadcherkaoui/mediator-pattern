using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    public interface ICommandHandler<TCommand> 
    {
        void HandleCommad(TCommand command);
    }
    public interface IMessagingCommandHandler<TCommand> : ICommandHandler<TCommand> 
    {
    }    

    public class CommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        public virtual void HandleCommad(TCommand command)
        {

        }
    }    
    
    public class MessagingCommandHandler<TCommand> : IMessagingCommandHandler<TCommand>
    {
        public void HandleCommad(TCommand command)
        {
            var message = new Message(command);
            MessageQueue queue = new MessageQueue(@$".\private$\mediatr");
            queue.Send(message);
        }
    }
}
