using GeneratorBase;
using System.Collections.Generic;
using System.Threading.Tasks;
using RazorLight;

namespace WebApiGenerator.Templates
{
    public partial class DbContextTemplate : ITransformText
    {
        public IList<Module> Modules { get; }
        public string ProjectName { get; }

        public DbContextTemplate(string projectName, IList<Module> modules)
        {
            Modules = modules;
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
                Modules = Modules
            };
            string result = await engine.CompileRenderAsync("DbContextTemplate.cshtml", model);
            return result;
        }
    }
}
