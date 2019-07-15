using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class BusinessCsProjTemplate : ITransformText
    {
        public string ProjectName { get; }
        public IList<Module> Modules { get; }

        public BusinessCsProjTemplate(string projectName, IList<Module> modules)
        {
            ProjectName = projectName;
            Modules = modules;
        }
    }
}
