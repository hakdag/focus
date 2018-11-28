using System.Reflection;
using Module = GeneratorBase.Module;

namespace UIGenerator.Templates
{
    public partial class EnumPipeTemplate
    {
        public Module Module { get; }
        public PropertyInfo Pi { get; }
        public string PropertyName => Pi.PropertyType.Name;

        public EnumPipeTemplate(PropertyInfo pi, Module module)
        {
            Module = module;
            Pi = pi;
        }
    }
}
