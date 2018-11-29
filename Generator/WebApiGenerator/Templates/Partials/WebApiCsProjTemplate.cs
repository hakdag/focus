using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class WebApiCsProjTemplate
    {
        public string ProjectName { get; }
        public List<Module> Modules { get; }

        public WebApiCsProjTemplate(string projectName, List<Module> modules)
        {
            ProjectName = projectName;
            Modules = modules;
        }
    }
}
