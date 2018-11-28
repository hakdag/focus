using Focus.Common.Attributes;
using GeneratorBase;
using System.Linq;
using System.Reflection;

namespace WebApiGenerator.Templates
{
    public partial class ApiControllerTemplate
    {
        private GeneratorType type;
        private string moduleName;
        private PropertyInfo searchProperty;
        private PropertyInfo defaultSortProperty;

        public ApiControllerTemplate(GeneratorType type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;

            searchProperty = type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
            defaultSortProperty = type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(DefaultSortAttribute)));
        }
    }
}
