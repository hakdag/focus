using GeneratorBase;
using System.Collections.Generic;
using System.Threading.Tasks;
using RazorLight;

namespace WebApiGenerator.Templates
{
    public partial class GlobalAsaxTemplate : ITransformText
    {
        public IList<Module> Modules { get; }
        public string ProjectName { get; }

        public GlobalAsaxTemplate(string projectName, IList<Module> modules)
        {
            this.Modules = modules;
            ProjectName = projectName;
        }

        public async Task<string> TransformText()
        {
            var absolutePath = $"{System.AppDomain.CurrentDomain.BaseDirectory}";
            var root = $"{absolutePath}Views";
            var engine = new RazorLightEngineBuilder()
                          .UseFilesystemProject(root)
                          .UseMemoryCachingProvider()
                          .Build();

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
