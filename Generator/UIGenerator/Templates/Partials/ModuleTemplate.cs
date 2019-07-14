using GeneratorBase;

namespace UIGenerator.Templates
{
    public partial class ModuleTemplate : ITransformText
    {
        private Module module;
        public ModuleTemplate(Module module)
        {
            this.module = module;
        }
    }
}
