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

            if (!Directory.Exists("Controllers"))
                Directory.CreateDirectory("Controllers");

            foreach (Module module in mb.Modules)
            {
                // create module folders
                string moduleFolderControllers = $"{OutputFolder}\\{projectName}\\Controllers\\{module.ModuleName}";
                if (!Directory.Exists(moduleFolderControllers))
                    Directory.CreateDirectory(moduleFolderControllers);

                // loop types in module
                foreach (var type in module.Models)
                {
                    if (type.BaseType == typeof(Enum))
                        continue;

                    // create controller
                    ApiControllerTemplate act = new ApiControllerTemplate(projectName, type, module.ModuleName);
                    string result = act.TransformText();
                    File.WriteAllText($"{moduleFolderControllers}\\{type.Name}Controller.cs", result);
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
                    File.WriteAllText($"{OutputFolder}\\{file.Value}", result);
                    continue;
                }

                string text = Resources.ResourceManager.GetString(file.Key);
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
        }
    }
}
