using GeneratorBase;
using System.Collections.Generic;

namespace UIGenerator.Templates
{
    public partial class LayoutRoutesTemplate : ITransformText
    {
        public List<Module> Modules { get; }

        public LayoutRoutesTemplate(List<Module> modules)
        {
            Modules = modules;
        }
    }
}
