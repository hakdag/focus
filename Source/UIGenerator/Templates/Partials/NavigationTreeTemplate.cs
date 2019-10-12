using Focus.Common.Attributes;
using GeneratorBase;
using GeneratorBase.Extensions;
using System;
using System.Collections.Generic;

namespace UIGenerator.Templates
{
    public partial class NavigationTreeTemplate : ITransformText
    {
        public IList<Module> Modules { get; }

        public NavigationTreeTemplate(IList<Module> modules)
        {
            Modules = modules;
        }

        public string GetTitle(Type type) => type.GetAttributeValue((TitleAttribute ta) => ta.Title);
    }
}
