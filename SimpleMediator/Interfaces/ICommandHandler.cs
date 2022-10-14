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
        // void Handle(TCommand command);
        CommandResult Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, TResult>
    {
        // void Handle(TCommand command);
        CommandResult Handle(TCommand command);
    }

    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        public abstract CommandResult Handle(TCommand command);
    }    
}
