using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class DbContextTemplate : ITransformText
    {
        public List<Module> Modules { get; }
        public string ProjectName { get; }

        public DbContextTemplate(string projectName, List<Module> modules)
        {
            Modules = modules;
            ProjectName = projectName;
        }
    }
}
