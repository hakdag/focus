using GeneratorBase;
using System;
using System.Globalization;
using System.IO;
using UIGenerator.Templates;
using System.ComponentModel.DataAnnotations;

namespace UIGenerator
{
    public class UITransformer : TransformerBase
    {
        private readonly CultureInfo cultureInfo = new CultureInfo("en-EN", false);

        public UITransformer(string sourceLibrary, string outputFolder)
            : base(sourceLibrary, outputFolder)
        { }

        public override void Transform()
        {
            DisplayAttribute attr;
            // copying base ui files.
            CopyFiles();

            // var factory = new TemplateFactory(Modules);

            // create navigation-tree.ts
            // factory.Create<NavigationTreeTemplate>();
            var navigationTreeTemplate = new NavigationTreeTemplate(Modules);
            TransformText(navigationTreeTemplate, $"{OutputFolder}\\src\\app\\shared\\navigation-tree.ts");

            // create sidebar.template.html
            var sideBarHtmlTemplate = new SideBarHtmlTemplate(Modules);
            TransformText(sideBarHtmlTemplate, $"{OutputFolder}\\src\\app\\layout\\sidebar\\sidebar.template.html");

            // create layout.routes.ts
            var layoutRoutesTemplate = new LayoutRoutesTemplate(Modules);
            TransformText(layoutRoutesTemplate, $"{OutputFolder}\\src\\app\\layout\\layout.routes.ts");

            // create core.module.ts
            var coreModuleTemplate = new CoreModuleTemplate(Modules);
            TransformText(coreModuleTemplate, $"{OutputFolder}\\src\\app\\core\\core.module.ts");

            CreateModules();
        }

        private void CreateModules()
        {
            foreach (var module in Modules)
            {
                // create module folder
                var moduleFolder = $"{OutputFolder}\\src\\app\\{module.ModuleName}";
                CreateFolder(moduleFolder);

                CreateModule(module, moduleFolder);
            }
        }

        private void CreateModule(Module module, string moduleFolder)
        {
            // create module.ts file
            var mt = new ModuleTemplate(module);
            TransformText(mt, $"{moduleFolder}\\{module.ModuleName}.module.ts");

            CreateTypes(module, moduleFolder);
        }

        private void CreateTypes(Module module, string moduleFolder)
        {
            // loop types in module
            foreach (var type in module.Models)
            {
                var typeFolder = $"{moduleFolder}\\{type.Name.ToLower(cultureInfo)}";
                CreateFolder(typeFolder);

                CreateType(module, type, typeFolder);
            }
        }

        private void CreateType(Module module, GeneratorType type, string typeFolder)
        {
            if (type.BaseType == typeof(Enum))
            {
                // create model.ts file
                var modelTemplate = new ModelTemplate(type, Modules);
                TransformText(modelTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}.ts");

                // create service.ts file
                var serviceTemplate = new EnumServiceTemplate(type);
                TransformText(serviceTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}.service.ts");
            }
            else
            {
                // create model.ts file
                var modelTemplate = new ModelTemplate(type, Modules);
                TransformText(modelTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}.ts");

                // create service.ts file
                var serviceTemplate = new ServiceTemplate(type, module.ModuleName);
                TransformText(serviceTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}.service.ts");

                // create listcomponent.ts file
                var listComponentTemplate = new ListComponentTemplate(type);
                TransformText(listComponentTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}-list.component.ts");

                // create list html.ts file
                var listHtmlTemplate = new ListHtmlTemplate(type, module.ModuleName, module.UIName);
                TransformText(listHtmlTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}-list.template.html");

                // create edit component.ts file
                var editComponentTemplate = new EditComponentTemplate(type, module.ModuleName, Modules);
                TransformText(editComponentTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}-edit.component.ts");

                // create edit html.ts file
                var editHtmlTemplate = new EditHtmlTemplate(type, module.ModuleName, module.UIName);
                TransformText(editHtmlTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}-edit.template.html");

                // create enum pipe files
                var enumProperties = GetEnumProperties(type.Type);
                foreach (var pi in enumProperties)
                {
                    var typeName = ExtractTypeName(pi);
                    var enumModule = SearchTypeInModules(Modules, typeName);
                    var pipeFolder = $"{OutputFolder}\\src\\app\\core\\pipes";
                    CreateFolder(pipeFolder);
                    var enumPipeTemplate = new EnumPipeTemplate(pi, module);
                    TransformText(enumPipeTemplate, $"{pipeFolder}\\{typeName.ToLower(cultureInfo)}.pipe.ts");
                }
            }
        }

        private void CopyFiles()
        {
            var zipPath = "Frontend.zip";
            using (var resource = new MemoryStream(Resources.Frontend))
            using (var file = new FileStream(zipPath, FileMode.Create, FileAccess.Write))
            {
                resource.CopyTo(file);
            }
            System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, OutputFolder);
        }
    }
}
