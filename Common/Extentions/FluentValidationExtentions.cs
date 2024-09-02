using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extentions
{
    public static class FluentValidationExtentions
    {
        public static List<string> GetErrors(this ValidationResult validation) => validation.Errors.Select(x => x.ErrorMessage).ToList();
    }
}
