using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using GeneratorBase;
using Module = GeneratorBase.Module;

namespace UIGenerator.Templates
{
    public partial class ModelTemplate
    {
        private GeneratorType type;
        private List<KeyValuePair<string, string>> properties = new List<KeyValuePair<string, string>>();
        private Dictionary<string, string> imports = new Dictionary<string, string>();
        private List<Module> modules;

        public ModelTemplate(GeneratorType type, List<Module> modules)
        {
            this.type = type;
            this.modules = modules;

            if (type.BaseType == typeof(Enum))
            {
                Array enumValues = Enum.GetValues(type.Type);
                for (int i = 0; i < enumValues.Length; i++)
                {
                    int intValue = (int)enumValues.GetValue(i);
                    properties.Add(new KeyValuePair<string, string>(enumValues.GetValue(i).ToString(), intValue.ToString()));
                }
            }
            else
            {
                PropertyInfo[] pis = type.Type.GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    string typeName = addToProperties(pi);
                    addToImports(typeName);
                }
            }
        }

        private void addToImports(string typeName)
        {
            if (imports.ContainsKey(typeName) || typeName == type.Name)
                return;

            if (typeName == "Parametre")
                imports.Add(typeName, "import { Parametre } from \"app/yonetim/parametre/parametre\"");
            else
            {
                Module module = searchTypeInModules(typeName);
                if (module != null)
                {
                    string lowTypeName = typeName.ToLower(new System.Globalization.CultureInfo("en-EN", false));
                    imports.Add(typeName, "import { " + typeName + " } from \"app/" + module.ModuleName + "/" + lowTypeName + "/" + lowTypeName + "\"");
                }
            }
        }

        private string addToProperties(PropertyInfo pi)
        {
            string typeName = extractTypeName(pi);
            typeName = typeNameForPrimitives(typeName);

            properties.Add(new KeyValuePair<string, string>(pi.Name, typeName));
            return typeName;
        }

        private string typeNameForPrimitives(string typeName)
        {
            if (typeName == "Int32" || typeName == "Decimal")
                typeName = "number";
            else if (typeName == "DateTime")
                typeName = "Date";
            return typeName;
        }

        private string extractTypeName(PropertyInfo pi)
        {
            string typeName = pi.PropertyType.Name;
            if (pi.PropertyType.BaseType == typeof(System.Array))
                typeName = typeName.TrimEnd('[', ']');
            if (typeName == "Nullable`1")
                typeName = pi.PropertyType.GenericTypeArguments[0].Name;
            if (typeName == "ICollection`1")
            {
                typeName = $"{pi.PropertyType.GenericTypeArguments[0].Name}[]";
            }
            return typeName;
        }

        private Module searchTypeInModules(string typeName)
        {
            return modules.FirstOrDefault(m => m.Models.Any(mo => mo.Name == typeName));
        }
    }
}
