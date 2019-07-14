using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class GlobalAsaxTemplate : ITransformText
    {
        public IList<Module> Modules { get; }
        public string ProjectName { get; }

        public GlobalAsaxTemplate(string projectName, IList<Module> modules)
        {
            this.Modules = modules;
            ProjectName = projectName;
        }
    }
}
