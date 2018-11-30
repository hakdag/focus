using Focus.Common;
using Focus.Common.Attributes;
using GeneratorBase;
using GeneratorBase.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using RequiredAttribute = Focus.Common.Attributes.RequiredAttribute;

namespace UIGenerator.Templates
{
    public partial class EditHtmlTemplate
    {
        public GeneratorType Type { get; }
        private string moduleName;
        private string moduleUIName;
        private string title;
        private PropertyInfo[] editableProperties;
        private PropertyInfo[] collectionProperties;

        public EditHtmlTemplate(GeneratorType type, string moduleName, string moduleUIName)
        {
            this.Type = type;
            this.moduleName = moduleName;
            this.moduleUIName = moduleUIName;

            string attrValueTitle = type.Type.GetAttributeValue((TitleAttribute ta) => ta.Title);
            this.title = $"{attrValueTitle}";

            var properties = type.Type.GetProperties();
            editableProperties = properties.Where(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(DisplayAttribute))).ToArray();
            collectionProperties = properties.Where(pi => IsCollection(pi.PropertyType)).ToArray();
        }

        private string GetSearchPropertyName(Type type)
        {
            var sp = GetSearchProperty(type);
            if (sp == null) return "Id";

            if (sp.PropertyType.BaseType != null && sp.PropertyType.BaseType.Name == "BaseModel")
            {
                Type propertyType = sp.PropertyType;
                var searchProperty = propertyType.GetProperties().FirstOrDefault(pi2 => pi2.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
                return sp.Name + "." + (searchProperty != null ? searchProperty.Name : propertyType.GetProperties().FirstOrDefault().Name);
            }
            else if (sp.PropertyType.BaseType != null && sp.PropertyType.BaseType.BaseType != null && sp.PropertyType.BaseType.BaseType.Name == "BaseModel")
            {
                Type propertyType = sp.PropertyType.BaseType;
                var searchProperty = propertyType.GetProperties().FirstOrDefault(pi2 => pi2.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
                return sp.Name + "." + (searchProperty != null ? searchProperty.Name : propertyType.GetProperties().FirstOrDefault().Name);
            }
            else
            {
                return sp.Name;
            }
        }

        private PropertyInfo GetSearchProperty(Type type)
        {
            var properties = type.GetProperties();
            var sp = properties.FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
            return sp;
        }

        private Type GetGenericProperty(Type genericType) => genericType.GetGenericArguments()[0];

        private bool IsCollection(Type type) =>
            type.IsGenericType && typeof(ICollection<>).IsAssignableFrom(type.GetGenericTypeDefinition()) ||
            type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>));

        private string getSearchPropertyName() => GetSearchPropertyName(Type.Type);

        private string getPropertyAttributeName<T>(PropertyInfo pi)
        {
            if (pi == null)
                return String.Empty;

            var t = GetPropertyAttribute<T>(pi.PropertyType);

            return t.Name;
        }

        private PropertyInfo GetPropertyAttribute<T>(Type type)
        {
            var properties = type.GetProperties();
            var prop = properties.FirstOrDefault(p => p.CustomAttributes.Any(ca => ca.AttributeType == typeof(T)));
            return prop;
        }

        private bool isPropertyRequired(PropertyInfo pi)
        {
            return pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(RequiredAttribute));
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

        private string getInputType(PropertyInfo pi)
        {
            if (pi.PropertyType == typeof(bool) || pi.PropertyType == typeof(bool?))
                return "bool";
            if (pi.PropertyType == typeof(string))
                return "text";
            if (pi.PropertyType == typeof(int) || pi.PropertyType == typeof(int?))
                return "int";
            if (pi.PropertyType == typeof(decimal) || pi.PropertyType == typeof(decimal?))
                return "decimal";
            if (pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(DateTime?))
                return "datetime";
            if (pi.PropertyType.BaseType == typeof(Enum))
                return "enum";
            //if (pi.PropertyType == typeof(Adres))
            //    return "adres";
            //if (pi.PropertyType == typeof(Parametre))
            //    return "parametre";
            if (pi.PropertyType.BaseType != null && pi.PropertyType.BaseType == typeof(BaseModel))
                return pi.PropertyType.Name;
            if (pi.PropertyType.BaseType != null && pi.PropertyType.BaseType.BaseType == typeof(BaseModel))
                return pi.PropertyType.BaseType.Name;
            return null;
        }
    }
}
