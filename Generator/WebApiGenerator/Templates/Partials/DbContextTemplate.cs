using GeneratorBase;
using System.Collections.Generic;

namespace WebApiGenerator.Templates
{
    public partial class DbContextTemplate : ITransformText
    {
        public IList<Module> Modules { get; }
        public string ProjectName { get; }

        public DbContextTemplate(string projectName, IList<Module> modules)
        {
            Modules = modules;
            ProjectName = projectName;
        }
    }
}
