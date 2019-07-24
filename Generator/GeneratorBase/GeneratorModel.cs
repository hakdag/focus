using System.Collections.Generic;
using System.Reflection;

namespace GeneratorBase
{
    public class GeneratorModel
    {
        public string ProjectName { get; set; }
        public string ModuleName { get; set; }
        public PropertyInfo SearchProperty { get; set; }
        public PropertyInfo DefaultSortProperty { get; set; }
        public GeneratorType Type { get; set; }
        public IList<Module> Modules { get; set; }
    }
}