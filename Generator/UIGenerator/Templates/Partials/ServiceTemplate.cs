using System;
using System.Linq;
using System.Reflection;

namespace UIGenerator.Templates
{
    public partial class ServiceTemplate
    {
        private Type type;
        private string moduleName;
        private PropertyInfo[] datetimeProperties;
        public ServiceTemplate(Type type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;

            datetimeProperties = type.GetProperties().Where(pi => pi.PropertyType == typeof(DateTime)).ToArray();
        }
    }
}
