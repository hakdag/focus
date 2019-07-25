using GeneratorBase;
using System;
using System.IO;
using System.Threading.Tasks;
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

        public override async Task Transform()
        {
            CreateFolders(projectName);

            CopyFiles(SourceLibrary);

            // traverse every file in the API folder and replace namespace
            GenerateGeneralFiles(projectName);

            // create Global.asax
            var gat = new GlobalAsaxTemplate(projectName, Modules);
            await TransformText(gat, $"{OutputFolder}\\{projectName}\\Global.asax.cs");

            // create web api .csproj file
            var webApiCsProjTemplate = new WebApiCsProjTemplate(projectName, Modules);
            await TransformText(webApiCsProjTemplate, $"{OutputFolder}\\{projectName}\\{projectName}.csproj");

            // create business .csproj file
            var businessCsProjTemplate = new BusinessCsProjTemplate(projectName, Modules);
            await TransformText(businessCsProjTemplate, $"{OutputFolder}\\{projectName}.Business\\{projectName}.Business.csproj");

            // create contracts .csproj file
            var contractsCsProjTemplate = new ContractsCsProjTemplate(projectName, Modules);
            await TransformText(contractsCsProjTemplate, $"{OutputFolder}\\{projectName}.Contracts\\{projectName}.Contracts.csproj");

            // create data access .csproj file
            var dataAccessCsProjTemplate = new DataAccessCsProjTemplate(projectName);
            await TransformText(dataAccessCsProjTemplate, $"{OutputFolder}\\{projectName}.DataAccess\\{projectName}.DataAccess.csproj");

            // create DbContext file
            var dbContextTemplate = new DbContextTemplate(projectName, Modules);
            await TransformText(dbContextTemplate, $"{OutputFolder}\\{projectName}.DataAccess\\{projectName}Context.cs");

            await CreateModules();
        }

        private async Task CreateModules()
        {
            foreach (Module module in Modules)
            {
                await CreateModule(module);
            }
        }

        private async Task CreateModule(Module module)
        {
            // create module folders
            var moduleFolderBusinesses = $"{OutputFolder}\\{projectName}.Business\\{module.ModuleName}";
            CreateFolder(moduleFolderBusinesses);
            var moduleFolderContracts = $"{OutputFolder}\\{projectName}.Contracts\\Business\\{module.ModuleName}";
            CreateFolder(moduleFolderContracts);
            var moduleFolderControllers = $"{OutputFolder}\\{projectName}\\Controllers\\{module.ModuleName}";
            CreateFolder(moduleFolderControllers);

            await CreateTypes(module, moduleFolderBusinesses, moduleFolderContracts, moduleFolderControllers);
        }

        private async Task CreateTypes(Module module, string moduleFolderBusinesses, string moduleFolderContracts, string moduleFolderControllers)
        {
            // loop types in module
            foreach (var type in module.Models)
            {
                if (type.BaseType == typeof(Enum))
                    continue;

                await CreateType(module, moduleFolderBusinesses, moduleFolderContracts, moduleFolderControllers, type);
            }
        }

        private async Task CreateType(Module module, string moduleFolderBusinesses, string moduleFolderContracts, string moduleFolderControllers, GeneratorType type)
        {
            // create controller
            var act = new ApiControllerTemplate(projectName, type, module.ModuleName);
            await TransformText(act, $"{moduleFolderControllers}\\{type.Name}Controller.cs");

            // create IBusiness interface for business class
            var ibt = new IBusinessTemplate(projectName, type, module.ModuleName);
            await TransformText(ibt, $"{moduleFolderContracts}\\I{type.Name}Business.cs");

            // create Business classes
            var bt = new BusinessTemplate(projectName, type, module.ModuleName);
            await TransformText(bt, $"{moduleFolderBusinesses}\\{type.Name}Business.cs");
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
            File.Copy(sourceLibrary, $"{OutputFolder}{Path.DirectorySeparatorChar}Libraries{Path.DirectorySeparatorChar}{sourceLibrary}", true);
            File.Copy("Focus.Common.dll", $"{OutputFolder}{Path.DirectorySeparatorChar}Libraries{Path.DirectorySeparatorChar}Focus.Common.dll", true);
        }

        private void CreateFolders(string projectName)
        {
            // Create Api folders
            CreateFolder($"{OutputFolder}");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}Libraries");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}{Path.DirectorySeparatorChar}App_Start");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}{Path.DirectorySeparatorChar}Controllers");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}{Path.DirectorySeparatorChar}Filters");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}{Path.DirectorySeparatorChar}Helpers");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}{Path.DirectorySeparatorChar}Properties");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}{Path.DirectorySeparatorChar}Providers");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}{Path.DirectorySeparatorChar}Models");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}{Path.DirectorySeparatorChar}Results");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}{Path.DirectorySeparatorChar}Services");
            // Business Project folder
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.Business");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.Business{Path.DirectorySeparatorChar}Properties");
            // Contracts Project folders
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.Contracts");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.Contracts{Path.DirectorySeparatorChar}Properties");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.Contracts{Path.DirectorySeparatorChar}Business");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.Contracts{Path.DirectorySeparatorChar}DataAccess");
            // DataAccess Project Folders
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.DataAccess");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.DataAccess{Path.DirectorySeparatorChar}Properties");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.DataAccess{Path.DirectorySeparatorChar}Data");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.DataAccess{Path.DirectorySeparatorChar}UnitOfWork");
            CreateFolder($"{OutputFolder}{Path.DirectorySeparatorChar}{projectName}.DataAccess{Path.DirectorySeparatorChar}Repositories");
        }
    }
}
