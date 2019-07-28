using GeneratorBase;
using RazorLight;
using System.Threading.Tasks;

namespace WebApiGenerator.Templates
{
    public partial class BusinessTemplate : ITransformText
    {
        private readonly RazorLightEngine engine;
        private GeneratorType type;
        private string moduleName;
        public string ProjectName { get; }

        public BusinessTemplate(RazorLightEngine engine, string projectName, GeneratorType type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;
            this.engine = engine;
            ProjectName = projectName;
        }

        public async Task<string> TransformText()
        {
            var model = new GeneratorModel
            {
                ProjectName = ProjectName,
                ModuleName = moduleName,
                Type = type,
            };
            string result = await engine.CompileRenderAsync("BusinessTemplate.cshtml", model);
            return result;
        }
    }
}
