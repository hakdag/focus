using GeneratorBase;
using System.Collections.Generic;

namespace UIGenerator.Templates
{
    public partial class LayoutRoutesTemplate : ITransformText
    {
        public IList<Module> Modules { get; }

        public LayoutRoutesTemplate(IList<Module> modules)
        {
            Modules = modules;
        }
    }
}
