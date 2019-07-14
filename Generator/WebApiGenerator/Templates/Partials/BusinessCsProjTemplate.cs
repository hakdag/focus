using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class BusinessCsProjTemplate
    {
        public string ProjectName { get; }
        public List<Module> Modules { get; }

        public BusinessCsProjTemplate(string projectName, List<Module> modules)
        {
            ProjectName = projectName;
            Modules = modules;
        }
    }
}
