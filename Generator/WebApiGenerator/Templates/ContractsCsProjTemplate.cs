using GeneratorBase;
using System.Collections.Generic;
using System.Threading.Tasks;
using RazorLight;

namespace WebApiGenerator.Templates
{
    public partial class ContractsCsProjTemplate : ITransformText
    {
        public string ProjectName { get; }
        public IList<Module> Modules { get; }

        public ContractsCsProjTemplate(string projectName, IList<Module> modules)
        {
            ProjectName = projectName;
            Modules = modules;
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
                Modules = Modules
            };
            string result = await engine.CompileRenderAsync("ContractsCsProjTemplate.cshtml", model);
            return result;
        }
    }
}
