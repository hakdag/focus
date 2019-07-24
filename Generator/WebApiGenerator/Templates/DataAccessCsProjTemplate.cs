using System.Threading.Tasks;
using GeneratorBase;
using RazorLight;

namespace WebApiGenerator.Templates
{
    public partial class DataAccessCsProjTemplate : ITransformText
    {
        public string ProjectName { get; }

        public DataAccessCsProjTemplate(string projectName)
        {
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
                ProjectName = ProjectName
            };
            string result = await engine.CompileRenderAsync("DataAccessCsProjTemplate.cshtml", model);
            return result;
        }
    }
}
