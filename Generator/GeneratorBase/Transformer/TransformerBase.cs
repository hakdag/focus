using Focus.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GeneratorBase
{
    public abstract class TransformerBase
    {
        protected string OutputFolder { get; }
        protected string SourceLibrary { get; }
        public IList<Module> Modules { get; protected set; }

        public TransformerBase(string sourceLibrary, string outputFolder)
        {
            SourceLibrary = sourceLibrary;
            OutputFolder = outputFolder;
        }

        public abstract void Transform();

        public void Initialize()
        {
            var mb = new ModulesBuilder(SourceLibrary, nameof(BaseModel));
            mb.Build();
            Modules = mb.Modules;
        }

        protected string ExtractTypeName(PropertyInfo pi)
        {
            var typeName = pi.PropertyType.Name;
            if (pi.PropertyType.BaseType == typeof(Array))
                typeName = typeName.TrimEnd('[', ']');
            if (typeName == "Nullable`1")
                typeName = pi.PropertyType.GenericTypeArguments[0].Name;
            return typeName;
        }

        protected Module SearchTypeInModules(IList<Module> modules, string typeName)
        {
            return modules.FirstOrDefault(m => m.Models.Any(mo => mo.Name == typeName));
        }

        protected PropertyInfo[] GetEnumProperties(Type type)
        {
            var properties = type.GetProperties();
            return properties.Where(p => p.PropertyType.BaseType == typeof(Enum)).ToArray();
        }

        protected async Task TransformText(ITransformText tt, string path)
        {
            var text = await tt.TransformText();
            File.WriteAllText(path, text);
        }

        protected void CreateFolder(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
    }
}
