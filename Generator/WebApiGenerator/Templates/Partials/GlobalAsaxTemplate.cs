﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneratorBase;

namespace WebApiGenerator.Templates
{
    public partial class GlobalAsaxTemplate
    {
        private List<Module> modules;

        public GlobalAsaxTemplate(List<Module> modules)
        {
            this.modules = modules;
        }
    }
}