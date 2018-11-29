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
        public string ProjectName { get; }

        public ApiControllerTemplate(string projectName, GeneratorType type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;
            ProjectName = projectName;

            searchProperty = type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
            defaultSortProperty = type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(DefaultSortAttribute)));
        }
    }
}
