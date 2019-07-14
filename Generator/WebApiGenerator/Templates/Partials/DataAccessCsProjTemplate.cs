using GeneratorBase;

namespace WebApiGenerator.Templates
{
    public partial class DataAccessCsProjTemplate : ITransformText
    {
        public string ProjectName { get; }

        public DataAccessCsProjTemplate(string projectName)
        {
            ProjectName = projectName;
        }
    }
}
