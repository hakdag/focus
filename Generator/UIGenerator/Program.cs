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
        private static string OutputFolder = $"Frontend{DateTime.Now:MMddyyyyhhmmss}";
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please specify source library as a parameter.");
                return;
            }

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

            var sourceLibrary = args[0];
            ModulesBuilder mb = new ModulesBuilder(sourceLibrary, "BaseModel");
            mb.Build();

            if (!mb.Modules.Any())
            {
                Console.WriteLine("Could not find any modules.");
                return;
            }

            // copying base ui files.
            CopyFiles();

            // creating general files.
            // create navigation-tree.ts
            NavigationTreeTemplate navigationTreeTemplate = new NavigationTreeTemplate(mb.Modules);
            string strNavigationTree = navigationTreeTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\src\\app\\shared\\navigation-tree.ts", strNavigationTree);

            // create sidebar.template.html
            SideBarHtmlTemplate sideBarHtmlTemplate = new SideBarHtmlTemplate(mb.Modules);
            string strSideBarHtml = sideBarHtmlTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\src\\app\\layout\\sidebar\\sidebar.template.html", strSideBarHtml);

            // create layout.routes.ts
            LayoutRoutesTemplate layoutRoutesTemplate = new LayoutRoutesTemplate(mb.Modules);
            string strLayoutRoutes = layoutRoutesTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\src\\app\\layout\\layout.routes.ts", strLayoutRoutes);

            // create core.module.ts
            CoreModuleTemplate coreModuleTemplate = new CoreModuleTemplate(mb.Modules);
            string strCoreModule = coreModuleTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\src\\app\\core\\core.module.ts", strCoreModule);

            foreach (GeneratorBase.Module module in mb.Modules)
            {
                // create module folder
                var moduleFolder = $"{OutputFolder}\\src\\app\\{module.ModuleName}";
                if (!Directory.Exists(moduleFolder))
                    Directory.CreateDirectory(moduleFolder);

                // create module.ts file
                ModuleTemplate mt = new ModuleTemplate(module);
                string result = mt.TransformText();
                File.WriteAllText($"{moduleFolder}\\{module.ModuleName}.module.ts", result);

                // loop types in module
                foreach (var type in module.Models)
                {
                    string typeFolder = $"{moduleFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}";
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
                        PropertyInfo[] enumProperties = getEnumProperties(type.Type);
                        foreach (PropertyInfo pi in enumProperties)
                        {
                            EnumPipeTemplate enumPipeTemplate = new EnumPipeTemplate(pi, module);
                            string strEnumPipeTemplate = enumPipeTemplate.TransformText();
                            string typeName = extractTypeName(pi);
                            var enumModule = searchTypeInModules(mb.Modules, typeName);
                            var pipeFolder = $"{OutputFolder}\\src\\app\\core\\pipes";
                            if (!Directory.Exists(pipeFolder))
                                Directory.CreateDirectory(pipeFolder);
                            File.WriteAllText($"{pipeFolder}\\{typeName.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.pipe.ts", strEnumPipeTemplate);
                        }
                    }
                }
            }
        }

        private static void CopyFiles()
        {
            var zipPath = "Frontend.zip";
            using (var resource = new MemoryStream(Resources.Frontend))
            using (var file = new FileStream(zipPath, FileMode.Create, FileAccess.Write))
            {
                resource.CopyTo(file);
            }
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, OutputFolder);
        }

        private static void WriteResourceToFile(string resourceName, string fileName)
        {
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
