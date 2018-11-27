using GeneratorBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiGenerator.Templates
{
    public partial class DbContextTemplate
    {
        private List<Module> modules;

        public DbContextTemplate(List<Module> modules)
        {
            this.modules = modules;
        }
    }
}
