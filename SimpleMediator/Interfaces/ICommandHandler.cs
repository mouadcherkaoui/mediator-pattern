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

    public class CommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        public virtual void HandleCommad(TCommand command)
        {

        }
    }    
}
