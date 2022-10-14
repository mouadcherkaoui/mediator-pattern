using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediatorPattern
{
    public enum State
    {
        Succes,
        ValidationErrors,
        Validated,
        Failed
    }

    public class CommandResult<TResult>
    {
        public TResult Result { get; set; }
        public State State { get; set; }
        public ICollection<ValidationResult> ValidationResults { get; set; }
    }
    public class CommandResult
    {
        public Dictionary<string, object> Result { get; set; }
        public State State { get; set; }
        public ICollection<ValidationResult> ValidationResults { get; set; }
    }
}