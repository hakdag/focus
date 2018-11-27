using Focus.Common.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace WebApiGenerator.Templates
{
    public partial class ApiControllerTemplate
    {
        private Type type;
        private string moduleName;
        private PropertyInfo searchProperty;
        private PropertyInfo defaultSortProperty;

        public ApiControllerTemplate(Type type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;

            searchProperty = type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
            defaultSortProperty = type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(DefaultSortAttribute)));
        }
    }
}
