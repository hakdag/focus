using GeneratorBase;
using RazorLight;
using System.Threading.Tasks;

namespace WebApiGenerator.Templates
{
    public partial class DataAccessCsProjTemplate : ITransformText
    {
        private readonly RazorLightEngine engine;

        public string ProjectName { get; }

        public DataAccessCsProjTemplate(RazorLightEngine engine, string projectName)
        {
            this.engine = engine;
            ProjectName = projectName;
        }

        public async Task<string> TransformText()
        {
            var model = new GeneratorModel
            {
                ProjectName = ProjectName
            };
            string result = await engine.CompileRenderAsync("DataAccessCsProjTemplate.cshtml", model);
            return result;
        }
    }
}
