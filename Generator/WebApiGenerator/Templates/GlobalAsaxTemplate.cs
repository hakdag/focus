using GeneratorBase;
using RazorLight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiGenerator.Templates
{
    public partial class GlobalAsaxTemplate : ITransformText
    {
        private readonly RazorLightEngine engine;

        public IList<Module> Modules { get; }
        public string ProjectName { get; }

        public GlobalAsaxTemplate(RazorLightEngine engine, string projectName, IList<Module> modules)
        {
            this.Modules = modules;
            this.engine = engine;
            ProjectName = projectName;
        }

        public async Task<string> TransformText()
        {
            var model = new GeneratorModel
            {
                ProjectName = ProjectName,
                Modules = Modules
            };
            string result = await engine.CompileRenderAsync("GlobalAsaxTemplate.cshtml", model);
            return result;
        }
    }
}
