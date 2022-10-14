using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern
{
    public class RegisterCustomerCommand : ICommand
    {
        [Required]
        public Guid Id { get; set; }
        [MinLength(5)]
        public string CustomerInfos { get; set; }
    }
}
