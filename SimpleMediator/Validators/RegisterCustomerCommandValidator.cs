using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    public class RegisterCustomerCommandValidator : ICommandHandler<RegisterCustomerCommand>, ICommandValidator<RegisterCustomerCommand>
    {
        ICommandHandler<RegisterCustomerCommand> innerHandler;
        public RegisterCustomerCommandValidator(ICommandHandler<RegisterCustomerCommand> decorated)
        {
            innerHandler = decorated;
        }
        List<ValidationResult> validationResults = new List<ValidationResult>();
        public ICollection<ValidationResult> Validate(RegisterCustomerCommand command)
        {
            Validator.TryValidateObject(command, new ValidationContext(command), validationResults, true);
            return validationResults;
        }

        public CommandResult Handle(RegisterCustomerCommand command)
        {
            var resultToReturn = new CommandResult();
            var validationResults = Validate(command);
            if(validationResults.Count > 0)
            {
                resultToReturn.State = State.ValidationErrors;
                resultToReturn.ValidationResults = validationResults;
            }
            else
            {
                innerHandler.Handle(command);
                resultToReturn.State = State.Succes;
            }

            return resultToReturn;
        }
    }   
}
