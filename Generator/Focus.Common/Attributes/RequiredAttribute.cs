using System;
using System.ComponentModel.DataAnnotations;

namespace Focus.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
                return ValidationResult.Success;
            
            return new ValidationResult($"{validationContext.DisplayName} field is required.");
        }
    }
}
