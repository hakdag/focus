using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Focus.Common.Attributes
{
    public class ArraySizeAttribute : ValidationAttribute
    {
        private int minElements;
        private string resourceItemName;

        public ArraySizeAttribute(int minElements, string resourceItemName)
        {
            this.minElements = minElements;
            this.resourceItemName = resourceItemName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IList list = value as IList;
            if (list == null || list.Count < this.minElements)
                return new ValidationResult("At least 1 item must be added.");

            return ValidationResult.Success;
        }
    }
}