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

            CopyFiles(sourceLibrary);

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

            if (!Directory.Exists("Controllers"))
                Directory.CreateDirectory("Controllers");

            foreach (Module module in mb.Modules)
            {
                // create module folders
                string moduleFolderControllers = $"Output\\Backend\\{projectName}\\Controllers\\{module.ModuleName}";
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
                    File.WriteAllText($"Output\\{file.Value}", result);
                    continue;
                }

                string text = Resources.ResourceManager.GetString(file.Key);
                text = text.Replace("#projectname#", projectName);
                File.WriteAllText($"Output\\{file.Value}", text);
            }
        }

        private static void CopyFiles(string sourceLibrary)
        {
            File.Copy(sourceLibrary, $"Output\\Backend\\Libraries\\{sourceLibrary}");
        }

        private static void CreateFolders(string projectName)
        {
            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");
            if (!Directory.Exists("Output\\Backend"))
                Directory.CreateDirectory("Output\\Backend");
            if (!Directory.Exists("Output\\Backend\\Libraries"))
                Directory.CreateDirectory("Output\\Backend\\Libraries");
            if (!Directory.Exists($"Output\\Backend\\{projectName}"))
                Directory.CreateDirectory($"Output\\Backend\\{projectName}");
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
