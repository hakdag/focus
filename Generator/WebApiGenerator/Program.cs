using GeneratorBase;
using System;
using System.IO;
using System.Linq;
using WebApiGenerator.Templates;

namespace WebApiGenerator
{
    class Program
    {
        private static string OutputFolder = $"Backend{DateTime.Now:MMddyyyyhhmmss}";

        static void Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("Please specify source library as a parameter and a project name.");
                return;
            }

            var sourceLibrary = args[0];
            var projectName = Files.ProjectName = args[1];
            ModulesBuilder mb = new ModulesBuilder(sourceLibrary, "BaseModel");
            mb.Build();

            if (!mb.Modules.Any())
            {
                Console.WriteLine("Could not find any modules.");
                return;
            }

            CreateFolders(projectName);

            CopyFiles(sourceLibrary);

            // traverse every file in the API folder and replace namespace
            GenerateGeneralFiles(projectName);

            // create Global.asax
            var gat = new GlobalAsaxTemplate(projectName, mb.Modules);
            Transform(gat, projectName, $"{OutputFolder}\\{projectName}\\Global.asax.cs");

            // create web api .csproj file
            var webApiCsProjTemplate = new WebApiCsProjTemplate(projectName, mb.Modules);
            Transform(webApiCsProjTemplate, projectName, $"{OutputFolder}\\{projectName}\\{projectName}.csproj");

            // create business .csproj file
            var businessCsProjTemplate = new BusinessCsProjTemplate(projectName, mb.Modules);
            Transform(businessCsProjTemplate, projectName, $"{OutputFolder}\\{projectName}.Business\\{projectName}.Business.csproj");

            // create contracts .csproj file
            var contractsCsProjTemplate = new ContractsCsProjTemplate(projectName, mb.Modules);
            Transform(contractsCsProjTemplate, projectName, $"{OutputFolder}\\{projectName}.Contracts\\{projectName}.Contracts.csproj");

            // create data access .csproj file
            var dataAccessCsProjTemplate = new DataAccessCsProjTemplate(projectName);
            Transform(dataAccessCsProjTemplate, projectName, $"{OutputFolder}\\{projectName}.DataAccess\\{projectName}.DataAccess.csproj");

            // create DbContext file
            var dbContextTemplate = new DbContextTemplate(projectName, mb.Modules);
            Transform(dbContextTemplate, projectName, $"{OutputFolder}\\{projectName}.DataAccess\\{projectName}Context.cs");

            foreach (Module module in mb.Modules)
            {
                // create module folders
                var moduleFolderBusinesses = $"{OutputFolder}\\{projectName}.Business\\{module.ModuleName}";
                CreateFolder(moduleFolderBusinesses);
                var moduleFolderContracts = $"{OutputFolder}\\{projectName}.Contracts\\Business\\{module.ModuleName}";
                CreateFolder(moduleFolderContracts);
                var moduleFolderControllers = $"{OutputFolder}\\{projectName}\\Controllers\\{module.ModuleName}";
                CreateFolder(moduleFolderControllers);

                // loop types in module
                foreach (var type in module.Models)
                {
                    if (type.BaseType == typeof(Enum))
                        continue;

                    // create controller
                    var act = new ApiControllerTemplate(projectName, type, module.ModuleName);
                    Transform(act, projectName, $"{moduleFolderControllers}\\{type.Name}Controller.cs");

                    // create IBusiness interface for business class
                    var ibt = new IBusinessTemplate(projectName, type, module.ModuleName);
                    Transform(ibt, projectName, $"{moduleFolderContracts}\\I{type.Name}Business.cs");

                    // create Business classes
                    var bt = new BusinessTemplate(projectName, type, module.ModuleName);
                    Transform(bt, projectName, $"{moduleFolderBusinesses}\\{type.Name}Business.cs");
                }
            }
        }

        private static void Transform(ITransformText tt, string projectName, string path)
        {
            var text = tt.TransformText();
            File.WriteAllText(path, text);
        }

        private static void CreateFolder(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }

        private static void GenerateGeneralFiles(string projectName)
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

        private static void CopyFiles(string sourceLibrary)
        {
            File.Copy(sourceLibrary, $"{OutputFolder}\\Libraries\\{sourceLibrary}", true);
            File.Copy("Focus.Common.dll", $"{OutputFolder}\\Libraries\\Focus.Common.dll", true);
        }

        private static void CreateFolders(string projectName)
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
