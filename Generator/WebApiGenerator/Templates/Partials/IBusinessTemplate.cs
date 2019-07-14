using GeneratorBase;

namespace WebApiGenerator.Templates
{
    public partial class IBusinessTemplate
    {
        private GeneratorType type;
        private string moduleName;
        public string ProjectName { get; }

        public IBusinessTemplate(string projectName, GeneratorType type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;
            ProjectName = projectName;
        }
    }
}
