using System;
using System.Threading.Tasks;
using RazorLight;
using WebApiGenerator;

namespace WebApiGeneratorRunner
{
    class Program
    {
        private static readonly string OutputFolder = $"Backend{DateTime.Now:MMddyyyyhhmmss}";

        static async Task Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("Please specify source library as a parameter and a project name.");
                return;
            }

            var sourceLibrary = args[0];
            var projectName = args[1];

            // creating general files.
            try
            {
                Console.WriteLine("Starting...");
                var engine = CreateEngine();
                var transformer = new WebApiTransformer(engine, sourceLibrary, projectName, OutputFolder);
                Console.WriteLine("Loading Assembly...");
                transformer.Initialize();
                Console.WriteLine($"Modules found: {transformer.Modules.Count}");
                Console.WriteLine("Generating...");
                await transformer.Transform();
                Console.WriteLine("Finished!");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        private static RazorLightEngine CreateEngine()
        {
            var absolutePath = $"{System.AppDomain.CurrentDomain.BaseDirectory}";
            var root = $"{absolutePath}Views";
            var engine = new RazorLightEngineBuilder()
                        .UseFilesystemProject(root)
                        .UseMemoryCachingProvider()
                        .Build();
            return engine;
        }
    }
}
