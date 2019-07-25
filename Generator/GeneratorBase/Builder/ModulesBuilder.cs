using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Focus.Common.Attributes;
using GeneratorBase.Extensions;

namespace GeneratorBase
{
    public class ModulesBuilder
    {
        private string sourceLib;
        private string baseModelName;
        private int idCounter;

        public List<Module> Modules { get; internal set; }

        public ModulesBuilder(string sourceLib, string baseModelName)
        {
            this.sourceLib = sourceLib;
            this.baseModelName = baseModelName;
            Modules = new List<Module>();
            idCounter = 2;
        }

        public void Build()
        {
            try
            {
                // AssemblyName assemblyName = AssemblyName.GetAssemblyName(sourceLib);
                // Assembly assembly = Assembly.Load(assemblyName);
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(sourceLib);
                var types = assembly.GetTypes();
                var baseTypes = types.Where(t => t.BaseType.Name == baseModelName || (t.BaseType.BaseType != null && t.BaseType.BaseType.Name == baseModelName) || t.BaseType == typeof(Enum)).ToList();
                foreach (var type in baseTypes)
                {
                    string moduleName = type.GetAttributeValue((ModuleAttribute dna) => dna.ModuleName);
                    string uiName = type.GetAttributeValue((ModuleAttribute dna) => dna.UIName);
                    if (String.IsNullOrEmpty(moduleName)) continue;

                    var module = Modules.FirstOrDefault(m => m.ModuleName == moduleName);
                    var moduleId = module?.ModuleId ?? idCounter;
                    var typeId = idCounter + 1;
                    var generatorType = new GeneratorType(type, typeId, moduleId);
                    if (module == null)
                    {
                        Modules.Add(new Module(moduleName, uiName, new List<GeneratorType> { generatorType }, idCounter));
                    }
                    else
                    {
                        module.Models.Add(generatorType);
                    }
                    idCounter += 2;
                }
            }
            catch (FileNotFoundException)
            {
                throw new Exception($"File provided could not be found: {sourceLib}");
            }
        }
    }
}
