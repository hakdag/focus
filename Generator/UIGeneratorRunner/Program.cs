using System;
using UIGenerator;

namespace UIGeneratorRunner
{
    class Program
    {
        private static readonly string OutputFolder = $"Frontend{DateTime.Now:MMddyyyyhhmmss}";

        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("Please specify source library as a parameter.");
                return;
            }

            var sourceLibrary = args[0];

            // creating general files.
            var transformer = new UITransformer(sourceLibrary, OutputFolder);
            transformer.Transform();
        }
    }
}