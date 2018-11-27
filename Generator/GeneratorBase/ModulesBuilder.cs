using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Focus.Common.Attributes;
using GeneratorBase.Extensions;

namespace GeneratorBase
{
    public class ModulesBuilder
    {
        private string sourceLib;
        private string baseModelName;

        public List<Module> Modules { get; internal set; }

        public ModulesBuilder(string sourceLib, string baseModelName)
        {
            this.sourceLib = sourceLib;
            this.baseModelName = baseModelName;
            Modules = new List<Module>();
        }

        public void Build()
        {
            Assembly assembly = Assembly.Load(sourceLib);
            var types = assembly.GetTypes();
            var baseTypes = types.Where(t => t.BaseType.Name == baseModelName || (t.BaseType.BaseType != null && t.BaseType.BaseType.Name == baseModelName) || t.BaseType == typeof(Enum)).ToList();
            foreach (var type in baseTypes)
            {
                string moduleName = type.GetAttributeValue((ModuleAttribute dna) => dna.ModuleName);
                string uiName = type.GetAttributeValue((ModuleAttribute dna) => dna.UIName);
                if (String.IsNullOrEmpty(moduleName)) continue;

                var module = Modules.FirstOrDefault(m => m.ModuleName == moduleName);
                if (module == null)
                    Modules.Add(new Module(moduleName, uiName, new List<Type> { type }));
                else
                    module.Models.Add(type);
            }
        }
    }
}
