using Focus.Common.Attributes;
using GeneratorBase;
using GeneratorBase.Extensions;
using System.Linq;
using System.Reflection;

namespace UIGenerator.Templates
{
    public partial class ListComponentTemplate
    {
        private GeneratorType type;
        private string typeUIName;
        private string defaultSortProperty;
        public ListComponentTemplate(GeneratorType type)
        {
            this.type = type;
            this.typeUIName = type.Type.GetAttributeValue((TitleAttribute ta) => ta.Title);

            PropertyInfo piSortProperty = type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(DefaultSortAttribute)));
            if (piSortProperty != null)
                defaultSortProperty = piSortProperty.Name;
        }
    }
}
