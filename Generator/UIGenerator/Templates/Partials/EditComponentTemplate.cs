using Focus.Common.Attributes;
using GeneratorBase;
using GeneratorBase.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Module = GeneratorBase.Module;

namespace UIGenerator.Templates
{
    public partial class EditComponentTemplate : ITransformText
    {
        private string title;
        private GeneratorType type;
        private PropertyInfo[] properties;
        private Dictionary<string, string> imports = new Dictionary<string, string>();
        private string moduleName;
        private IList<Module> modules;
        private List<string> providers = new List<string>();
        private Dictionary<string, string> listProperties = new Dictionary<string, string>();
        private Dictionary<string, string> relationProperties = new Dictionary<string, string>();
        private Dictionary<string, string> parametreProperties = new Dictionary<string, string>();
        private List<PropertyInfo> enumProperties = new List<PropertyInfo>();
        private List<PropertyInfo> serviceProperties = new List<PropertyInfo>();
        public EditComponentTemplate(GeneratorType type, string moduleName, IList<Module> modules)
        {
            this.type = type;
            this.moduleName = moduleName;
            this.modules = modules;
            this.title = type.Type.GetAttributeValue((TitleAttribute ta) => ta.Title);
            properties = type.Type.GetProperties();

            foreach (PropertyInfo pi in properties)
            {
                string typeName = extractTypeName(pi);
                addToImports(typeName);
                addToProviders(typeName);
                addToListProperties(typeName, pi.Name);
                if (IsCollection(pi.PropertyType))
                {
                    addToRelations(typeName, pi.Name);
                }
                addToServiceProperties(pi);
                //if (typeName == "Parametre")
                //    addToParameterProperties(pi);
                if (pi.PropertyType.BaseType == typeof(Enum))
                    enumProperties.Add(pi);
            }
        }

        private bool IsCollection(Type type) =>
            type.IsGenericType && typeof(ICollection<>).IsAssignableFrom(type.GetGenericTypeDefinition()) ||
            type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>));

        //private void addToParameterProperties(PropertyInfo pi)
        //{
        //    var tcAttr = pi.CustomAttributes.FirstOrDefault(ca => ca.AttributeType == typeof(TypeCodeAttribute));
        //    if (tcAttr != null)
        //    {
        //        var arg = tcAttr.ConstructorArguments.FirstOrDefault(ca => ca.ArgumentType.BaseType == typeof(Enum));
        //        parametreProperties.Add(pi.Name, Enum.ToObject(arg.ArgumentType, arg.Value).ToString().ToUpper());
        //    }
        //}

        private void addToServiceProperties(PropertyInfo pi)
        {
            if (IsCollection(pi.PropertyType) || serviceProperties.Any(sp => sp.Name == pi.Name))
                return;
            string typeName = extractTypeName(pi);
            if (typeName == "Parametre" || pi.PropertyType.BaseType == typeof(Enum))
                return;
            Module module = searchTypeInModules(typeName);
            if (module != null)
                serviceProperties.Add(pi);
        }

        private void addToListProperties(string typeName, string pName)
        {
            if (listProperties.ContainsKey(typeName))
                return;

            if (typeName == "Parametre")
                listProperties.Add(pName, typeName);
            else
            {
                Module module = searchTypeInModules(typeName);
                if (module != null)
                    listProperties.Add(typeName, typeName);
            }
        }

        private void addToRelations(string typeName, string piName)
        {
            relationProperties.Add(typeName, piName);
        }

        private void addToProviders(string typeName)
        {
            if (providers.Contains(typeName) || typeName == type.Name)
                return;

            if (typeName == "Parametre")
            {
                providers.Add(typeName);
            }
            else
            {
                Module module = searchTypeInModules(typeName);
                if (module != null)
                    providers.Add(typeName);
            }
        }

        private Module searchTypeInModules(string typeName)
        {
            return modules.FirstOrDefault(m => m.Models.Any(mo => mo.Name == typeName));
        }

        private void addToImports(string typeName)
        {
            if (imports.ContainsKey(typeName) || typeName == type.Name)
                return;

            if (typeName == "Parametre")
            {
                imports.Add(typeName, "import { Parametre } from \"app/yonetim/parametre/parametre\"");
                imports.Add(typeName + "Service", "import { ParametreService } from \"app/yonetim/parametre/parametre.service\"");
            }
            else
            {
                Module module = searchTypeInModules(typeName);
                if (module != null)
                {
                    string lowTypeName = typeName.ToLower(new System.Globalization.CultureInfo("en-EN", false));
                    imports.Add(typeName, "import { " + typeName + " } from \"app/" + module.ModuleName + "/" + lowTypeName + "/" + lowTypeName + "\"");
                    imports.Add(typeName + "Service", "import { " + typeName + "Service } from \"app/" + module.ModuleName + "/" + lowTypeName + "/" + lowTypeName + ".service\"");
                }
            }
        }

        private string extractTypeName(PropertyInfo pi)
        {
            string typeName = pi.PropertyType.Name;
            if (pi.PropertyType.BaseType == typeof(System.Array))
                typeName = typeName.TrimEnd('[', ']');
            if (typeName == "Nullable`1" || typeName == "ICollection`1")
                typeName = pi.PropertyType.GenericTypeArguments[0].Name;
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

        private string getConstructorArgument<T>(PropertyInfo pi)
        {
            if (type != null)
            {
                var displayProperty = pi.CustomAttributes.FirstOrDefault(ca => ca.AttributeType == typeof(T));
                if (displayProperty != null)
                {
                    if (displayProperty.ConstructorArguments[0].ArgumentType.BaseType == typeof(Enum))
                    {
                        return Enum.ToObject(displayProperty.ConstructorArguments[0].ArgumentType, displayProperty.ConstructorArguments[0].Value).ToString();
                    }
                    else
                    {
                        return displayProperty.ConstructorArguments[0].Value.ToString();
                    }
                }
            }

            return String.Empty;
        }
    }
}
