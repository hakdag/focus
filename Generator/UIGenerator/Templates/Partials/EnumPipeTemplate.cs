using System.Reflection;

namespace UIGenerator.Templates
{
    public partial class EnumPipeTemplate
    {
        private GeneratorBase.Module module;
        private PropertyInfo pi;
        private string pName;

        public EnumPipeTemplate(PropertyInfo pi, GeneratorBase.Module module)
        {
            this.module = module;
            this.pi = pi;
            this.pName = pi.Name;
        }
    }
}
