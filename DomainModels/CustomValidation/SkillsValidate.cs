using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainModels.CustomValidation
{
    public class SkillsValidate : Attribute, IModelValidator
    {
        public string[]? Allowed { get; set; }
        public string? ErrorMessage { get; set; }
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {

#pragma warning disable CS8604 // Possible null reference argument.
            if (Allowed.Contains(context.Model as string))
#pragma warning restore CS8604 // Possible null reference argument.
                return Enumerable.Empty<ModelValidationResult>();
            else
                return new List<ModelValidationResult> {
                    new ModelValidationResult("", ErrorMessage)
                };
        }
    }
}
