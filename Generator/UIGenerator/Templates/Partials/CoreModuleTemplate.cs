using System;
using GeneratorBase;
using System.Collections.Generic;
using System.Linq;

namespace UIGenerator.Templates
{
    public partial class CoreModuleTemplate : ITransformText
    {
        public List<Module> Modules { get; }
        public List<GeneratorType> Pipes { get; }

        public CoreModuleTemplate(List<Module> modules)
        {
            Modules = modules;
            Pipes = modules.SelectMany(m => m.Models).Where(t => t.BaseType == typeof(Enum)).ToList();
        }
    }
}
