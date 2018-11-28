using GeneratorBase;

namespace WebApiGenerator.Templates
{
    public partial class IBusinessTemplate
    {
        private GeneratorType type;
        private string moduleName;

        public IBusinessTemplate(GeneratorType type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;
        }
    }
}
