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

            var sourceLibrary = args[0];
            var mb = new ModulesBuilder(sourceLibrary, "BaseModel");
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
            var navigationTreeTemplate = new NavigationTreeTemplate(mb.Modules);
            var strNavigationTree = navigationTreeTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\src\\app\\shared\\navigation-tree.ts", strNavigationTree);

            // create sidebar.template.html
            var sideBarHtmlTemplate = new SideBarHtmlTemplate(mb.Modules);
            var strSideBarHtml = sideBarHtmlTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\src\\app\\layout\\sidebar\\sidebar.template.html", strSideBarHtml);

            // create layout.routes.ts
            var layoutRoutesTemplate = new LayoutRoutesTemplate(mb.Modules);
            var strLayoutRoutes = layoutRoutesTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\src\\app\\layout\\layout.routes.ts", strLayoutRoutes);

            // create core.module.ts
            var coreModuleTemplate = new CoreModuleTemplate(mb.Modules);
            var strCoreModule = coreModuleTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\src\\app\\core\\core.module.ts", strCoreModule);

            foreach (var module in mb.Modules)
            {
                // create module folder
                var moduleFolder = $"{OutputFolder}\\src\\app\\{module.ModuleName}";
                if (!Directory.Exists(moduleFolder))
                    Directory.CreateDirectory(moduleFolder);

                // create module.ts file
                var mt = new ModuleTemplate(module);
                var result = mt.TransformText();
                File.WriteAllText($"{moduleFolder}\\{module.ModuleName}.module.ts", result);

                // loop types in module
                foreach (var type in module.Models)
                {
                    var typeFolder = $"{moduleFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}";
                    if (!Directory.Exists(typeFolder))
                        Directory.CreateDirectory(typeFolder);

                    if (type.BaseType == typeof(Enum))
                    {
                        // create model.ts file
                        var modelTemplate = new ModelTemplate(type, mb.Modules);
                        var strModel = modelTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.ts", strModel);

                        // create service.ts file
                        var serviceTemplate = new EnumServiceTemplate(type);
                        var strService = serviceTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.service.ts", strService);
                    }
                    else
                    {
                        // create model.ts file
                        var modelTemplate = new ModelTemplate(type, mb.Modules);
                        var strModel = modelTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.ts", strModel);

                        // create service.ts file
                        var serviceTemplate = new ServiceTemplate(type, module.ModuleName);
                        var strService = serviceTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}.service.ts", strService);

                        // create listcomponent.ts file
                        var listComponentTemplate = new ListComponentTemplate(type);
                        var strListComponent = listComponentTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}-list.component.ts", strListComponent);

                        // create list html.ts file
                        var listHtmlTemplate = new ListHtmlTemplate(type, module.ModuleName, module.UIName);
                        var strListHtml = listHtmlTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}-list.template.html", strListHtml);

                        // create edit component.ts file
                        var editComponentTemplate = new EditComponentTemplate(type, module.ModuleName, mb.Modules);
                        var strEditComponent = editComponentTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}-edit.component.ts", strEditComponent);

                        // create edit html.ts file
                        var editHtmlTemplate = new EditHtmlTemplate(type, module.ModuleName, module.UIName);
                        var strEditHtml = editHtmlTemplate.TransformText();
                        File.WriteAllText($"{typeFolder}\\{type.Name.ToLower(new System.Globalization.CultureInfo("en-EN", false))}-edit.template.html", strEditHtml);

                        // create enum pipe files
                        var enumProperties = getEnumProperties(type.Type);
                        foreach (var pi in enumProperties)
                        {
                            var enumPipeTemplate = new EnumPipeTemplate(pi, module);
                            var strEnumPipeTemplate = enumPipeTemplate.TransformText();
                            var typeName = extractTypeName(pi);
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
            var typeName = pi.PropertyType.Name;
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
