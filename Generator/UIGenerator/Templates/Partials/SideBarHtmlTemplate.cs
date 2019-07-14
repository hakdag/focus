using Focus.Common.Attributes;
using GeneratorBase;
using GeneratorBase.Extensions;
using System;
using System.Collections.Generic;

namespace UIGenerator.Templates
{
    public partial class SideBarHtmlTemplate : ITransformText
    {
        public List<Module> Modules { get; }

        public SideBarHtmlTemplate(List<Module> modules)
        {
            Modules = modules;
        }

        public string GetTitle(Type type) => type.GetAttributeValue((TitleAttribute ta) => ta.Title);
    }
}
