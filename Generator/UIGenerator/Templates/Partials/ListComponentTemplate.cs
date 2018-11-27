using Focus.Common.Attributes;
using GeneratorBase.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace UIGenerator.Templates
{
    public partial class ListComponentTemplate
    {
        private Type type;
        private string typeUIName;
        private string defaultSortProperty;
        public ListComponentTemplate(Type type)
        {
            this.type = type;
            this.typeUIName = type.GetAttributeValue((TitleAttribute ta) => ta.Title);

            PropertyInfo piSortProperty = type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(DefaultSortAttribute)));
            if (piSortProperty != null)
                defaultSortProperty = piSortProperty.Name;
        }
    }
}
