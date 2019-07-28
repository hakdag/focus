using GeneratorBase;
using RazorLight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiGenerator.Templates
{
    public partial class WebApiCsProjTemplate : ITransformText
    {
        private readonly RazorLightEngine engine;

        public string ProjectName { get; }
        public IList<Module> Modules { get; }

        public WebApiCsProjTemplate(RazorLightEngine engine, string projectName, IList<Module> modules)
        {
            this.engine = engine;
            ProjectName = projectName;
            Modules = modules;
        }

        public async Task<string> TransformText()
        {
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
