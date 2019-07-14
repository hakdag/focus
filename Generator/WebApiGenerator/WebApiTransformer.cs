using GeneratorBase;
using System;
using System.IO;
using WebApiGenerator.Templates;

namespace WebApiGenerator
{
    public class WebApiTransformer : TransformerBase
    {
        private readonly string projectName;

        public WebApiTransformer(string sourceLibrary, string projectName, string outputFolder)
            : base(sourceLibrary, outputFolder)
        {
            this.projectName = Files.ProjectName = projectName;
        }

        public override void Transform()
        {
            if (Modules == null)
            {
                return;
            }

            CreateFolders(projectName);

            CopyFiles(SourceLibrary);

            // traverse every file in the API folder and replace namespace
            GenerateGeneralFiles(projectName);

            // create Global.asax
            var gat = new GlobalAsaxTemplate(projectName, Modules);
            TransformText(gat, $"{OutputFolder}\\{projectName}\\Global.asax.cs");

            // create web api .csproj file
            var webApiCsProjTemplate = new WebApiCsProjTemplate(projectName, Modules);
            TransformText(webApiCsProjTemplate, $"{OutputFolder}\\{projectName}\\{projectName}.csproj");

            // create business .csproj file
            var businessCsProjTemplate = new BusinessCsProjTemplate(projectName, Modules);
            TransformText(businessCsProjTemplate, $"{OutputFolder}\\{projectName}.Business\\{projectName}.Business.csproj");

            // create contracts .csproj file
            var contractsCsProjTemplate = new ContractsCsProjTemplate(projectName, Modules);
            TransformText(contractsCsProjTemplate, $"{OutputFolder}\\{projectName}.Contracts\\{projectName}.Contracts.csproj");

            // create data access .csproj file
            var dataAccessCsProjTemplate = new DataAccessCsProjTemplate(projectName);
            TransformText(dataAccessCsProjTemplate, $"{OutputFolder}\\{projectName}.DataAccess\\{projectName}.DataAccess.csproj");

            // create DbContext file
            var dbContextTemplate = new DbContextTemplate(projectName, Modules);
            TransformText(dbContextTemplate, $"{OutputFolder}\\{projectName}.DataAccess\\{projectName}Context.cs");

            CreateModules();
        }

        private void CreateModules()
        {
            foreach (Module module in Modules)
            {
                CreateModule(module);
            }
        }

        private void CreateModule(Module module)
        {
            // create module folders
            var moduleFolderBusinesses = $"{OutputFolder}\\{projectName}.Business\\{module.ModuleName}";
            CreateFolder(moduleFolderBusinesses);
            var moduleFolderContracts = $"{OutputFolder}\\{projectName}.Contracts\\Business\\{module.ModuleName}";
            CreateFolder(moduleFolderContracts);
            var moduleFolderControllers = $"{OutputFolder}\\{projectName}\\Controllers\\{module.ModuleName}";
            CreateFolder(moduleFolderControllers);

            CreateTypes(module, moduleFolderBusinesses, moduleFolderContracts, moduleFolderControllers);
        }

        private void CreateTypes(Module module, string moduleFolderBusinesses, string moduleFolderContracts, string moduleFolderControllers)
        {
            // loop types in module
            foreach (var type in module.Models)
            {
                if (type.BaseType == typeof(Enum))
                    continue;

                CreateType(module, moduleFolderBusinesses, moduleFolderContracts, moduleFolderControllers, type);
            }
        }

        private void CreateType(Module module, string moduleFolderBusinesses, string moduleFolderContracts, string moduleFolderControllers, GeneratorType type)
        {
            // create controller
            var act = new ApiControllerTemplate(projectName, type, module.ModuleName);
            TransformText(act, $"{moduleFolderControllers}\\{type.Name}Controller.cs");

            // create IBusiness interface for business class
            var ibt = new IBusinessTemplate(projectName, type, module.ModuleName);
            TransformText(ibt, $"{moduleFolderContracts}\\I{type.Name}Business.cs");

            // create Business classes
            var bt = new BusinessTemplate(projectName, type, module.ModuleName);
            TransformText(bt, $"{moduleFolderBusinesses}\\{type.Name}Business.cs");
        }

        private void GenerateGeneralFiles(string projectName)
        {
            foreach (var file in Files.Dictionary)
            {
                if (file.Key.Equals(nameof(Resources.SolutionFile)))
                {
                    var byteArray = Resources.ResourceManager.GetObject(file.Key) as byte[];
                    var result = System.Text.Encoding.UTF8.GetString(byteArray);
                    result = result.Replace("#projectname#", projectName);
                    File.WriteAllText($"{OutputFolder}\\{file.Value}", result);
                    continue;
                }

                var text = Resources.ResourceManager.GetString(file.Key);
                text = text.Replace("#projectname#", projectName);
                File.WriteAllText($"{OutputFolder}\\{file.Value}", text);
            }
        }

        private void CopyFiles(string sourceLibrary)
        {
            File.Copy(sourceLibrary, $"{OutputFolder}\\Libraries\\{sourceLibrary}", true);
            File.Copy("Focus.Common.dll", $"{OutputFolder}\\Libraries\\Focus.Common.dll", true);
        }

        private void CreateFolders(string projectName)
        {
            // Create Api folders
            CreateFolder($"{OutputFolder}");
            CreateFolder($"{OutputFolder}\\Libraries");
            CreateFolder($"{OutputFolder}\\{projectName}");
            CreateFolder($"{OutputFolder}\\{projectName}\\App_Start");
            CreateFolder($"{OutputFolder}\\{projectName}\\Controllers");
            CreateFolder($"{OutputFolder}\\{projectName}\\Filters");
            CreateFolder($"{OutputFolder}\\{projectName}\\Helpers");
            CreateFolder($"{OutputFolder}\\{projectName}\\Properties");
            CreateFolder($"{OutputFolder}\\{projectName}\\Providers");
            CreateFolder($"{OutputFolder}\\{projectName}\\Models");
            CreateFolder($"{OutputFolder}\\{projectName}\\Results");
            CreateFolder($"{OutputFolder}\\{projectName}\\Services");
            // Business Project folder
            CreateFolder($"{OutputFolder}\\{projectName}.Business");
            CreateFolder($"{OutputFolder}\\{projectName}.Business\\Properties");
            // Contracts Project folders
            CreateFolder($"{OutputFolder}\\{projectName}.Contracts");
            CreateFolder($"{OutputFolder}\\{projectName}.Contracts\\Properties");
            CreateFolder($"{OutputFolder}\\{projectName}.Contracts\\Business");
            CreateFolder($"{OutputFolder}\\{projectName}.Contracts\\DataAccess");
            // DataAccess Project Folders
            CreateFolder($"{OutputFolder}\\{projectName}.DataAccess");
            CreateFolder($"{OutputFolder}\\{projectName}.DataAccess\\Properties");
            CreateFolder($"{OutputFolder}\\{projectName}.DataAccess\\Data");
            CreateFolder($"{OutputFolder}\\{projectName}.DataAccess\\UnitOfWork");
            CreateFolder($"{OutputFolder}\\{projectName}.DataAccess\\Repositories");
        }
    }
}
