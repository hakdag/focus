using GeneratorBase;
using System;
using System.IO;
using System.Linq;
using WebApiGenerator.Templates;

namespace WebApiGenerator
{
    class Program
    {
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

            // traverse every file in the API folder and replace namespace
            GenerateGeneralFiles(projectName);

            // create Global.asax
            GlobalAsaxTemplate gat = new GlobalAsaxTemplate(projectName, mb.Modules);
            string resultgat = gat.TransformText();
            File.WriteAllText($"Output\\Backend\\{projectName}\\Global.asax.cs", resultgat);


            // create web api .csproj file
            WebApiCsProjTemplate webApiCsProjTemplate = new WebApiCsProjTemplate(projectName, mb.Modules);
            string strWebApiCsProj = webApiCsProjTemplate.TransformText();
            File.WriteAllText($"Output\\Backend\\{projectName}\\{projectName}.csproj", strWebApiCsProj);

            // create DbContext file
            DbContextTemplate dct = new DbContextTemplate(projectName, mb.Modules);
            string resultdct = dct.TransformText();
            File.WriteAllText("BetonCRMContext.cs", resultdct);

            if (!Directory.Exists("Controllers"))
                Directory.CreateDirectory("Controllers");

            foreach (Module module in mb.Modules)
            {
                // create module folders
                string moduleFolderControllers = $"Output\\Backend\\{projectName}\\Controllers\\{module.ModuleName}";
                string moduleFolderContracts = $"Output\\Backend\\{projectName}.Contracts\\{module.ModuleName}";
                string moduleFolderBusinesses = $"Output\\Backend\\{projectName}.Business\\{module.ModuleName}";
                if (!Directory.Exists(moduleFolderControllers))
                    Directory.CreateDirectory(moduleFolderControllers);
                if (!Directory.Exists(moduleFolderContracts))
                    Directory.CreateDirectory(moduleFolderContracts);
                if (!Directory.Exists(moduleFolderBusinesses))
                    Directory.CreateDirectory(moduleFolderBusinesses);

                // loop types in module
                foreach (var type in module.Models)
                {
                    if (type.BaseType == typeof(Enum))
                        continue;

                    // create controller
                    ApiControllerTemplate act = new ApiControllerTemplate(projectName, type, module.ModuleName);
                    string result = act.TransformText();
                    File.WriteAllText($"{moduleFolderControllers}\\{type.Name}Controller.cs", result);

                    // create IBusiness interface for business class
                    IBusinessTemplate ibt = new IBusinessTemplate(projectName, type, module.ModuleName);
                    string result2 = ibt.TransformText();
                    File.WriteAllText($"{moduleFolderContracts}\\I{type.Name}Business.cs", result2);

                    // create Business classes
                    BusinessTemplate bt = new BusinessTemplate(projectName, type, module.ModuleName);
                    string result3 = bt.TransformText();
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
                    string result = System.Text.Encoding.UTF8.GetString(byteArray);
                    result = result.Replace("#projectname#", projectName);
                    File.WriteAllText($"Output\\{file.Value}", result);
                    continue;
                }

                string text = Resources.ResourceManager.GetString(file.Key);
                text = text.Replace("#projectname#", projectName);
                File.WriteAllText($"Output\\{file.Value}", text);
            }
        }

        private static void CreateFolders(string projectName)
        {
            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");
            if (!Directory.Exists("Output\\Backend"))
                Directory.CreateDirectory("Output\\Backend");
            if (!Directory.Exists($"Output\\Backend\\{projectName}"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}");
            if (!Directory.Exists($"Output\\Backend\\{projectName}.Contracts"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}.Contracts");
            if (!Directory.Exists($"Output\\Backend\\{projectName}.Business"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}.Business");
            if (!Directory.Exists($"Output\\Backend\\{projectName}.Common"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}.Common");
            if (!Directory.Exists($"Output\\Backend\\{projectName}.DataAccess"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}.DataAccess");
            if (!Directory.Exists($"Output\\Backend\\{projectName}\\App_Start"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}\\App_Start");
            if (!Directory.Exists($"Output\\Backend\\{projectName}\\Controllers"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}\\Controllers");
            if (!Directory.Exists($"Output\\Backend\\{projectName}\\Filters"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}\\Filters");
            if (!Directory.Exists($"Output\\Backend\\{projectName}\\Helpers"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}\\Helpers");
            if (!Directory.Exists($"Output\\Backend\\{projectName}\\Properties"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}\\Properties");
            if (!Directory.Exists($"Output\\Backend\\{projectName}\\Providers"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}\\Providers");
            if (!Directory.Exists($"Output\\Backend\\{projectName}\\Models"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}\\Models");
            if (!Directory.Exists($"Output\\Backend\\{projectName}\\Results"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}\\Results");
            if (!Directory.Exists($"Output\\Backend\\{projectName}\\Services"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}\\Services");
        }
    }
}
