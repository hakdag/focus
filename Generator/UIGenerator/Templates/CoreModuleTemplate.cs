using GeneratorBase;
using RazorLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIGenerator.Templates
{
    public partial class CoreModuleTemplate : ITransformText
    {
        private readonly RazorLightEngine engine;

        public IList<Module> Modules { get; }
        public List<GeneratorType> Pipes { get; }

        public CoreModuleTemplate(RazorLightEngine engine, IList<Module> modules)
        {
            this.engine = engine;
            Modules = modules;
            Pipes = modules.SelectMany(m => m.Models).Where(t => t.BaseType == typeof(Enum)).ToList();
        }

        public async Task<string> TransformText()
        {
            var model = new GeneratorModel
            {
                Modules = this.Modules,
                Types = this.Pipes
            };
            string result = await engine.CompileRenderAsync("CoreModuleTemplate.cshtml", model);
            return result;
        }
    }
}
