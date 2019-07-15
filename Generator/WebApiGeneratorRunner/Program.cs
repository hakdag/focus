using System;
using WebApiGenerator;

namespace WebApiGeneratorRunner
{
    class Program
    {
        private static readonly string OutputFolder = $"Backend{DateTime.Now:MMddyyyyhhmmss}";

        static void Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("Please specify source library as a parameter and a project name.");
                return;
            }

            var sourceLibrary = args[0];
            var projectName = args[1];

            // creating general files.
            var transformer = new WebApiTransformer(sourceLibrary, projectName, OutputFolder);
            transformer.Initialize();
            transformer.Transform();
        }
    }
}
