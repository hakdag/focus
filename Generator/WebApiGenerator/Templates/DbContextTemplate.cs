using GeneratorBase;
using RazorLight;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiGenerator.Templates
{
    public partial class DbContextTemplate : ITransformText
    {
        private readonly RazorLightEngine engine;

        public IList<Module> Modules { get; }
        public string ProjectName { get; }

        public DbContextTemplate(RazorLightEngine engine, string projectName, IList<Module> modules)
        {
            Modules = modules;
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
            string result = await engine.CompileRenderAsync("DbContextTemplate.cshtml", model);
            return result;
        }
    }
}
