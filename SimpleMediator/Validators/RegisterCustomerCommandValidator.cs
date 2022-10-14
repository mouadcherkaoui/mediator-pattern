using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    public class RegisterCustomerCommandValidator : ICommandValidator<RegisterCustomerCommand>
    {
        List<ValidationResult> validationResults = new List<ValidationResult>();
        public ICollection<ValidationResult> Validate(RegisterCustomerCommand command)
        {
            Validator.TryValidateObject(command, new ValidationContext(command), validationResults, true);
            return validationResults;
        }
    }
}
