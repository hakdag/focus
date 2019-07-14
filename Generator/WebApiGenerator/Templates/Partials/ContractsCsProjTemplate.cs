using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class ContractsCsProjTemplate : ITransformText
    {
        public string ProjectName { get; }
        public IList<Module> Modules { get; }

        public ContractsCsProjTemplate(string projectName, IList<Module> modules)
        {
            ProjectName = projectName;
            Modules = modules;
        }
    }
}
