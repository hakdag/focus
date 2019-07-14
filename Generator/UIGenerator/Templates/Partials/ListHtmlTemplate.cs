using Focus.Common.Attributes;
using GeneratorBase;
using GeneratorBase.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace UIGenerator.Templates
{
    public partial class ListHtmlTemplate : ITransformText
    {
        private GeneratorType type;
        private string moduleName;
        private string moduleUIName;
        private string title;
        private string searchProperty;
        private PropertyInfo[] listableProperties;
        public ListHtmlTemplate(GeneratorType type, string moduleName, string moduleUIName)
        {
            this.type = type;
            this.moduleName = moduleName;
            this.moduleUIName = moduleUIName;

            string attrValueTitle = type.Type.GetAttributeValue((TitleAttribute ta) => ta.Title);
            this.title = attrValueTitle;

            searchProperty = getNamedArgument<DisplayAttribute>(type.Type.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute))));


            listableProperties = type.Type.GetProperties().Where(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(DisplayAttribute))).ToArray();
        }

        private string getSearchProperty()
        {
            var properties = type.Type.GetProperties();
            var sp = properties.FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
            if (sp == null) return "Id";

            if (sp.PropertyType.BaseType != null && sp.PropertyType.BaseType.Name == "BaseModel")
            {
                var searchProperty = sp.PropertyType.GetProperties().FirstOrDefault(pi2 => pi2.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
                return searchProperty != null ? searchProperty.Name : sp.PropertyType.GetProperties().FirstOrDefault().Name;
            }
            else if (sp.PropertyType.BaseType != null && sp.PropertyType.BaseType.BaseType != null && sp.PropertyType.BaseType.BaseType.Name == "BaseModel")
            {
                var searchProperty = sp.PropertyType.BaseType.GetProperties().FirstOrDefault(pi2 => pi2.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
                return searchProperty != null ? searchProperty.Name : sp.PropertyType.GetProperties().FirstOrDefault().Name;
            }
            else
            {
                return sp.Name;
            }
        }

        private string getListPropertyByType(PropertyInfo pi)
        {
            Type propertyType = pi.PropertyType;

            if (propertyType == typeof(DateTime))
            {
                return type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false)) + "." + pi.Name + " | date:\"dd/MM/yyyy HH:mm\"";
            }
            //else if (propertyType == typeof(Adres))
            //{
            //    return type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false)) + "." + pi.Name + " | AdresShortPipe";
            //}
            //else if (propertyType == typeof(Parametre))
            //{
            //    return type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false)) + "." + pi.Name + ".Name";
            //}
            else if (propertyType.BaseType != null && propertyType.BaseType.Name == "BaseModel")
            {
                var searchProperty = propertyType.GetProperties().FirstOrDefault(pi2 => pi2.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
                return type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false)) + "." + pi.Name + "." + (searchProperty != null ? searchProperty.Name : propertyType.GetProperties().FirstOrDefault().Name);
            }
            else if (propertyType.BaseType != null && propertyType.BaseType == typeof(Enum))
            {
                return type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false)) + "." + pi.Name + " | " + propertyType.Name + "Pipe";
			}
            else
            {
                return type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false)) + "." + pi.Name;
			}
        }

        private string getNamedArgument<T>(PropertyInfo pi)
        {
            if (pi != null)
            {
                var displayProperty = pi.CustomAttributes.FirstOrDefault(ca => ca.AttributeType == typeof(T));
                if (displayProperty != null)
                {
                    return displayProperty.NamedArguments[0].TypedValue.Value.ToString();
                }
            }

            return String.Empty;
        }

        private string getConstructorArgument<T>(Type type)
        {
            if (type != null)
            {
                var displayProperty = type.CustomAttributes.FirstOrDefault(ca => ca.AttributeType == typeof(T));
                if (displayProperty != null)
                {
                    return displayProperty.ConstructorArguments[0].Value.ToString();
                }
            }

            return String.Empty;
        }
    }
}
