using System;

namespace Focus.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, Inherited = false)]
    public class ModuleAttribute : Attribute
    {
        private string moduleName;
        private string uiName;

        public ModuleAttribute(string moduleName, string uiName)
        {
            this.moduleName = moduleName;
            this.uiName = uiName;
        }

        public string ModuleName
        {
            get
            {
                return moduleName;
            }

            set
            {
                moduleName = value;
            }
        }

        public string UIName
        {
            get
            {
                return uiName;
            }

            set
            {
                uiName = value;
            }
        }
    }
}
