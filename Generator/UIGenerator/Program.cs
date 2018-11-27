using GeneratorBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UIGenerator.Templates;

namespace UIGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             1- Loop through Modules
             2- create a folder for each module
             3- in each module folder create following file:
                - <module name>.module.ts
             4- Loop through models in module
             5- create a folder for each model
             6- in each folder there will be 6 files:
                - <model name>-edit.component.ts
                - <model name>-edit.template.html
                - <model name>-list.template.html
                - <model name>.component.ts
                - <model name>.service.ts
                - <model name>.ts
             7- 
             */

            ModulesBuilder mb = new ModulesBuilder("BetonCRM.Common", "BaseModel");
            mb.Build();
            foreach (GeneratorBase.Module module in mb.Modules)
            {
                // create module folder
                if (!Directory.Exists(module.ModuleName))
                    Directory.CreateDirectory(module.ModuleName);

                // create module.ts file
                ModuleTemplate mt = new ModuleTemplate(module);
                string result = mt.TransformText();
                File.WriteAllText($"{module.ModuleName}\\{module.ModuleName}.module.ts", result);

                // loop types in module
                foreach (Type type in module.Models)
                {
                    string typeFolder = $"{module.ModuleName}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}";
                    if (!Directory.Exists(typeFolder))
                        Directory.CreateDirectory(typeFolder);

                    if (type.BaseType == typeof(Enum))
                    {
                        // create model.ts file
                        ModelTemplate modelTemplate = new ModelTemplate(type, mb.Modules);
                        string strModel = modelTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.ts", strModel);

                        // create service.ts file
                        EnumServiceTemplate serviceTemplate = new EnumServiceTemplate(type);
                        string strService = serviceTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.service.ts", strService);
                    }
                    else
                    {
                        // create model.ts file
                        ModelTemplate modelTemplate = new ModelTemplate(type, mb.Modules);
                        string strModel = modelTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.ts", strModel);

                        // create service.ts file
                        ServiceTemplate serviceTemplate = new ServiceTemplate(type, module.ModuleName);
                        string strService = serviceTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.service.ts", strService);

                        // create listcomponent.ts file
                        ListComponentTemplate listComponentTemplate = new ListComponentTemplate(type);
                        string strListComponent = listComponentTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}-list.component.ts", strListComponent);

                        // create list html.ts file
                        ListHtmlTemplate listHtmlTemplate = new ListHtmlTemplate(type, module.ModuleName, module.UIName);
                        string strListHtml = listHtmlTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}-list.template.html", strListHtml);

                        // create edit component.ts file
                        EditComponentTemplate editComponentTemplate = new EditComponentTemplate(type, module.ModuleName, mb.Modules);
                        string strEditComponent = editComponentTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}-edit.component.ts", strEditComponent);

                        // create edit html.ts file
                        EditHtmlTemplate editHtmlTemplate = new EditHtmlTemplate(type, module.ModuleName, module.UIName);
                        string strEditHtml = editHtmlTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}-edit.template.html", strEditHtml);

                        // create enum pipe files
                        PropertyInfo[] enumProperties = getEnumProperties(type);
                        foreach (PropertyInfo pi in enumProperties)
                        {
                            EnumPipeTemplate enumPipeTemplate = new EnumPipeTemplate(pi, module);
                            string strEnumPipeTemplate = enumPipeTemplate.TransformText();
                            string typeName = extractTypeName(pi);
                            var enumModule = searchTypeInModules(mb.Modules, typeName);
                            var pipeFolder = $"app\\core\\pipes";
                            if (!Directory.Exists(pipeFolder))
                                Directory.CreateDirectory(pipeFolder);
                            File.WriteAllText($"{pipeFolder}\\{typeName.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.pipe.ts", strEnumPipeTemplate);
                        }
                    }
                }
            }
        }

        private static string extractTypeName(PropertyInfo pi)
        {
            string typeName = pi.PropertyType.Name;
            if (pi.PropertyType.BaseType == typeof(System.Array))
                typeName = typeName.TrimEnd('[', ']');
            if (typeName == "Nullable`1")
                typeName = pi.PropertyType.GenericTypeArguments[0].Name;
            return typeName;
        }

        private static GeneratorBase.Module searchTypeInModules(List<GeneratorBase.Module> modules, string typeName)
        {
            return modules.FirstOrDefault(m => m.Models.Any(mo => mo.Name == typeName));
        }

        private static PropertyInfo[] getEnumProperties(Type type)
        {
            var properties = type.GetProperties();
            return properties.Where(p => p.PropertyType.BaseType == typeof(Enum)).ToArray();
        }
    }
}
