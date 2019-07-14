using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class WebApiCsProjTemplate : ITransformText
    {
        public string ProjectName { get; }
        public IList<Module> Modules { get; }

        public WebApiCsProjTemplate(string projectName, IList<Module> modules)
        {
            ProjectName = projectName;
            Modules = modules;
        }
    }
}
