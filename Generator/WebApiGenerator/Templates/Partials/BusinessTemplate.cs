using GeneratorBase;

namespace WebApiGenerator.Templates
{
    public partial class BusinessTemplate
    {
        private GeneratorType type;
        private string moduleName;

        public BusinessTemplate(GeneratorType type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;
        }
    }
}
