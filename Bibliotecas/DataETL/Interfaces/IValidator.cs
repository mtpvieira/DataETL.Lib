using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataETL.Interfaces
{
    public interface IValidator<T>
    {
        Task<ValidationResult> ValidateAsync(IEnumerable<T> input);
    }
}
