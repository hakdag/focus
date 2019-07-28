using GeneratorBase;
using RazorLight;
using System.Threading.Tasks;

namespace WebApiGenerator.Templates
{
    public partial class IBusinessTemplate : ITransformText
    {
        private readonly RazorLightEngine engine;
        private GeneratorType type;
        private string moduleName;
        public string ProjectName { get; }

        public IBusinessTemplate(RazorLightEngine engine, string projectName, GeneratorType type, string moduleName)
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
                Type = type
            };
            string result = await engine.CompileRenderAsync("IBusinessTemplate.cshtml", model);
            return result;
        }
    }
}
