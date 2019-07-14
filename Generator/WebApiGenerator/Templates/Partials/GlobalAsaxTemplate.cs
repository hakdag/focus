using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class GlobalAsaxTemplate
    {
        public List<Module> Modules { get; }
        public string ProjectName { get; }

        public GlobalAsaxTemplate(string projectName, List<Module> modules)
        {
            this.Modules = modules;
            ProjectName = projectName;
        }
    }
}
