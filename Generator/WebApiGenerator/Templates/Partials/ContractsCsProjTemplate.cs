using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class ContractsCsProjTemplate
    {
        public string ProjectName { get; }
        public List<Module> Modules { get; }

        public ContractsCsProjTemplate(string projectName, List<Module> modules)
        {
            ProjectName = projectName;
            Modules = modules;
        }
    }
}
