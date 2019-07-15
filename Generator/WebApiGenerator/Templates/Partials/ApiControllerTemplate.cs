using Focus.Common.Attributes;
using GeneratorBase;
using System.Linq;
using System.Reflection;

namespace WebApiGenerator.Templates
{
    public partial class ApiControllerTemplate : ITransformText
    {
        public GeneratorType Type { get; }
        private string moduleName;
        public PropertyInfo SearchProperty { get; }
        public PropertyInfo DefaultSortProperty { get; }
        public string ProjectName { get; }

        public ApiControllerTemplate(string projectName, GeneratorType type, string moduleName)
        {
            this.Type = type;
            this.moduleName = moduleName;
            ProjectName = projectName;

            SearchProperty = type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
            DefaultSortProperty = type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(DefaultSortAttribute)));
        }
    }
}
