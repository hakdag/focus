using System.Threading.Tasks;
using GeneratorBase;
using RazorLight;

namespace WebApiGenerator.Templates
{
    public partial class IBusinessTemplate : ITransformText
    {
        private GeneratorType type;
        private string moduleName;
        public string ProjectName { get; }

        public IBusinessTemplate(string projectName, GeneratorType type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;
            ProjectName = projectName;
        }

        public async Task<string> TransformText()
        {
            var engine = new RazorLightEngineBuilder()
                          .UseFilesystemProject("Views")
                          .UseMemoryCachingProvider()
                          .Build();

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
