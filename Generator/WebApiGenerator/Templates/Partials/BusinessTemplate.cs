using GeneratorBase;

namespace WebApiGenerator.Templates
{
    public partial class BusinessTemplate
    {
        private GeneratorType type;
        private string moduleName;
        public string ProjectName { get; }

        public BusinessTemplate(string projectName, GeneratorType type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;
            ProjectName = projectName;
        }
    }
}
