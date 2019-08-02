using GeneratorBase;
using UIGenerator.Templates;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using RazorLight;

namespace UIGenerator
{
    public class UITransformer : TransformerBase
    {
        private readonly CultureInfo cultureInfo = new CultureInfo("en-EN", false);

        public UITransformer(RazorLightEngine engine, string sourceLibrary, string outputFolder)
            : base(engine, sourceLibrary, outputFolder)
        { }

        public override async Task Transform()
        {
            // copying base ui files.
            CopyFiles();

            // var factory = new TemplateFactory(Modules);

            // create navigation-tree.ts
            // factory.Create<NavigationTreeTemplate>();
            var navigationTreeTemplate = new NavigationTreeTemplate(Modules);
            await TransformText(navigationTreeTemplate, $"{OutputFolder}\\src\\app\\shared\\navigation-tree.ts");

            // create sidebar.template.html
            var sideBarHtmlTemplate = new SideBarHtmlTemplate(Modules);
            await TransformText(sideBarHtmlTemplate, $"{OutputFolder}\\src\\app\\layout\\sidebar\\sidebar.template.html");

            // create layout.routes.ts
            var layoutRoutesTemplate = new LayoutRoutesTemplate(Modules);
            await TransformText(layoutRoutesTemplate, $"{OutputFolder}\\src\\app\\layout\\layout.routes.ts");

            // create core.module.ts
            var coreModuleTemplate = new CoreModuleTemplate(RLEngine, Modules);
            await TransformText(coreModuleTemplate, $"{OutputFolder}\\src\\app\\core\\core.module.ts");

            await CreateModules();
        }

        private async Task CreateModules()
        {
            foreach (var module in Modules)
            {
                // create module folder
                var moduleFolder = $"{OutputFolder}\\src\\app\\{module.ModuleName}";
                CreateFolder(moduleFolder);

                await CreateModule(module, moduleFolder);
            }
        }

        private async Task CreateModule(Module module, string moduleFolder)
        {
            // create module.ts file
            var mt = new ModuleTemplate(module);
            await TransformText(mt, $"{moduleFolder}\\{module.ModuleName}.module.ts");

            await CreateTypes(module, moduleFolder);
        }

        private async Task CreateTypes(Module module, string moduleFolder)
        {
            // loop types in module
            foreach (var type in module.Models)
            {
                var typeFolder = $"{moduleFolder}\\{type.Name.ToLower(cultureInfo)}";
                CreateFolder(typeFolder);

                await CreateType(module, type, typeFolder);
            }
        }

        private async Task CreateType(Module module, GeneratorType type, string typeFolder)
        {
            if (type.BaseType == typeof(Enum))
            {
                // create model.ts file
                var modelTemplate = new ModelTemplate(type, Modules);
                await TransformText(modelTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}.ts");

                // create service.ts file
                var serviceTemplate = new EnumServiceTemplate(type);
                await TransformText(serviceTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}.service.ts");
            }
            else
            {
                // create model.ts file
                var modelTemplate = new ModelTemplate(type, Modules);
                await TransformText(modelTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}.ts");

                // create service.ts file
                var serviceTemplate = new ServiceTemplate(type, module.ModuleName);
                await TransformText(serviceTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}.service.ts");

                // create listcomponent.ts file
                var listComponentTemplate = new ListComponentTemplate(type);
                await TransformText(listComponentTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}-list.component.ts");

                // create list html.ts file
                var listHtmlTemplate = new ListHtmlTemplate(type, module.ModuleName, module.UIName);
                await TransformText(listHtmlTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}-list.template.html");

                // create edit component.ts file
                var editComponentTemplate = new EditComponentTemplate(RLEngine, type, module.ModuleName, Modules);
                await TransformText(editComponentTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}-edit.component.ts");

                // create edit html.ts file
                var editHtmlTemplate = new EditHtmlTemplate(type, module.ModuleName, module.UIName);
                await TransformText(editHtmlTemplate, $"{typeFolder}\\{type.Name.ToLower(cultureInfo)}-edit.template.html");

                // create enum pipe files
                var enumProperties = GetEnumProperties(type.Type);
                foreach (var pi in enumProperties)
                {
                    var typeName = ExtractTypeName(pi);
                    var enumModule = SearchTypeInModules(Modules, typeName);
                    var pipeFolder = $"{OutputFolder}\\src\\app\\core\\pipes";
                    CreateFolder(pipeFolder);
                    var enumPipeTemplate = new EnumPipeTemplate(pi, module);
                    await TransformText(enumPipeTemplate, $"{pipeFolder}\\{typeName.ToLower(cultureInfo)}.pipe.ts");
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
