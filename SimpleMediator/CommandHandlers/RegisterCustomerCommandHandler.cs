using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Experimental.System.Messaging;

namespace MediatorPattern
{
    [HandleCommandOfType(typeof(RegisterCustomerCommand), Validator = typeof(RegisterCustomerCommandValidator))]
    public class RegisterCustomerCommandHandler : CommandHandler<RegisterCustomerCommand>
    {
        public override CommandResult Handle(RegisterCustomerCommand command)
        {
            var resultToReturn = new CommandResult();

            try
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
                resultToReturn.State = State.Succes;
            }
            catch (Exception ex)            
            {
                resultToReturn.State = State.Failed;
            }
            return resultToReturn;
        }
    }
}
