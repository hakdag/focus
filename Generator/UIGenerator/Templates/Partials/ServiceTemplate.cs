using System;
using System.Linq;
using System.Reflection;
using GeneratorBase;

namespace UIGenerator.Templates
{
    public partial class ServiceTemplate
    {
        private GeneratorType type;
        private string moduleName;
        private PropertyInfo[] datetimeProperties;
        public ServiceTemplate(GeneratorType type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;

            datetimeProperties = type.Type.GetProperties().Where(pi => pi.PropertyType == typeof(DateTime)).ToArray();
        }
    }
}
