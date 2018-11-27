using System;
using System.Collections.Generic;

namespace GeneratorBase
{
    public class Module
    {
        private string moduleName;
        private string uiName;
        private List<Type> models;

        public Module(string moduleName, string uiName, List<Type> models)
        {
            this.moduleName = moduleName;
            this.models = models;
            this.uiName = uiName;
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

        public List<Type> Models
        {
            get
            {
                return models;
            }

            set
            {
                models = value;
            }
        }
    }
}
