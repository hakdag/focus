using System;
using System.Threading.Tasks;
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
                var transformer = new WebApiTransformer(sourceLibrary, projectName, OutputFolder);
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
    }
}
