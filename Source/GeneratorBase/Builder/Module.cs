using System;
using System.Collections.Generic;

namespace GeneratorBase
{
    public class Module
    {
        private string moduleName;
        private string uiName;
        private List<GeneratorType> models;
        public int ModuleId { get; }

        public Module(string moduleName, string uiName, List<GeneratorType> models, int id)
        {
            this.moduleName = moduleName;
            this.models = models;
            this.uiName = uiName;
            ModuleId = id;
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

        public List<GeneratorType> Models
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
