using System;

namespace WebApiGenerator.Templates
{
    public partial class BusinessTemplate
    {
        private Type type;
        private string moduleName;

        public BusinessTemplate(Type type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;
        }
    }
}
