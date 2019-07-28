using Focus.Common.Attributes;
using GeneratorBase;
using RazorLight;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApiGenerator.Templates
{
    public partial class ApiControllerTemplate : ITransformText
    {
        public GeneratorType Type { get; }

        private readonly RazorLightEngine engine;
        private string moduleName;
        public PropertyInfo SearchProperty { get; }
        public PropertyInfo DefaultSortProperty { get; }
        public string ProjectName { get; }

        public ApiControllerTemplate(RazorLightEngine engine, string projectName, GeneratorType type, string moduleName)
        {
            this.Type = type;
            this.moduleName = moduleName;
            this.engine = engine;
            ProjectName = projectName;

            SearchProperty = type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
            DefaultSortProperty = type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(DefaultSortAttribute)));
        }

        public async Task<string> TransformText()
        {
            var model = new GeneratorModel
            {
                ProjectName = ProjectName,
                ModuleName = moduleName,
                Type = Type,
                SearchProperty = SearchProperty,
                DefaultSortProperty = DefaultSortProperty
            };
            string result = await engine.CompileRenderAsync("ApiControllerTemplate.cshtml", model);
            return result;
        }
    }
}
