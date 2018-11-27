using System;

namespace WebApiGenerator.Templates
{
    public partial class IBusinessTemplate
    {
        private Type type;
        private string moduleName;

        public IBusinessTemplate(Type type, string moduleName)
        {
            this.type = type;
            this.moduleName = moduleName;
        }
    }
}
