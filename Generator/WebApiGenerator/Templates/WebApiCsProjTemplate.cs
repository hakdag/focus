using GeneratorBase;
using RazorLight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiGenerator.Templates
{
    public partial class WebApiCsProjTemplate : ITransformText
    {
        public string ProjectName { get; }
        public IList<Module> Modules { get; }

        public WebApiCsProjTemplate(string projectName, IList<Module> modules)
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
            string result = await engine.CompileRenderAsync("WebApiCsProjTemplate.cshtml", model);
            return result;
        }
    }
}
