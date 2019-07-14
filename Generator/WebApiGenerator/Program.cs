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
            GlobalAsaxTemplate gat = new GlobalAsaxTemplate(projectName, mb.Modules);
            string resultgat = gat.TransformText();
            File.WriteAllText($"{OutputFolder}\\{projectName}\\Global.asax.cs", resultgat);

            // create web api .csproj file
            WebApiCsProjTemplate webApiCsProjTemplate = new WebApiCsProjTemplate(projectName, mb.Modules);
            string strWebApiCsProj = webApiCsProjTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\{projectName}\\{projectName}.csproj", strWebApiCsProj);

            // create business .csproj file
            BusinessCsProjTemplate businessCsProjTemplate = new BusinessCsProjTemplate(projectName, mb.Modules);
            string strBusinessCsProj = businessCsProjTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\{projectName}.Business\\{projectName}.Business.csproj", strBusinessCsProj);

            // create contracts .csproj file
            ContractsCsProjTemplate contractsCsProjTemplate = new ContractsCsProjTemplate(projectName, mb.Modules);
            string strContractsCsProj = contractsCsProjTemplate.TransformText();
            File.WriteAllText($"{OutputFolder}\\{projectName}.Contracts\\{projectName}.Contracts.csproj", strContractsCsProj);

            foreach (Module module in mb.Modules)
            {
                // create module folders
                var moduleFolderBusinesses = $"{OutputFolder}\\{projectName}.Business\\{module.ModuleName}";
                if (!Directory.Exists(moduleFolderBusinesses))
                    Directory.CreateDirectory(moduleFolderBusinesses);
                var moduleFolderContracts = $"{OutputFolder}\\{projectName}.Contracts\\Business\\{module.ModuleName}";
                if (!Directory.Exists(moduleFolderContracts))
                    Directory.CreateDirectory(moduleFolderContracts);
                var moduleFolderControllers = $"{OutputFolder}\\{projectName}\\Controllers\\{module.ModuleName}";
                if (!Directory.Exists(moduleFolderControllers))
                    Directory.CreateDirectory(moduleFolderControllers);

                // loop types in module
                foreach (var type in module.Models)
                {
                    if (type.BaseType == typeof(Enum))
                        continue;

                    // create controller
                    var act = new ApiControllerTemplate(projectName, type, module.ModuleName);
                    var result = act.TransformText();
                    File.WriteAllText($"{moduleFolderControllers}\\{type.Name}Controller.cs", result);

                    // create IBusiness interface for business class
                    var ibt = new IBusinessTemplate(projectName, type, module.ModuleName);
                    var result2 = ibt.TransformText();
                    File.WriteAllText($"{moduleFolderContracts}\\I{type.Name}Business.cs", result2);

                    // create Business classes
                    var bt = new BusinessTemplate(projectName, type, module.ModuleName);
                    var result3 = bt.TransformText();
                    File.WriteAllText($"{moduleFolderBusinesses}\\{type.Name}Business.cs", result3);
                }
            }
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
            if (!Directory.Exists($"{OutputFolder}"))
                Directory.CreateDirectory($"{OutputFolder}");
            if (!Directory.Exists($"{OutputFolder}\\Libraries"))
                Directory.CreateDirectory($"{OutputFolder}\\Libraries");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}\\App_Start"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}\\App_Start");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}\\Controllers"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}\\Controllers");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}\\Filters"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}\\Filters");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}\\Helpers"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}\\Helpers");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}\\Properties"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}\\Properties");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}\\Providers"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}\\Providers");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}\\Models"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}\\Models");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}\\Results"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}\\Results");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}\\Services"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}\\Services");
            // Business Project folder
            if (!Directory.Exists($"{OutputFolder}\\{projectName}.Business"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}.Business");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}.Business\\Properties"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}.Business\\Properties");
            // Contracts Project folders
            if (!Directory.Exists($"{OutputFolder}\\{projectName}.Contracts"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}.Contracts");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}.Contracts\\Properties"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}.Contracts\\Properties");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}.Contracts\\Business"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}.Contracts\\Business");
            if (!Directory.Exists($"{OutputFolder}\\{projectName}.Contracts\\DataAccess"))
                Directory.CreateDirectory($"{OutputFolder}\\{projectName}.Contracts\\DataAccess");
        }
    }
}
