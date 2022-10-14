using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    public interface ICommandValidator<TCommand> where TCommand: ICommand
    {
        ICollection<ValidationResult> Validate(TCommand command);
    }
}
