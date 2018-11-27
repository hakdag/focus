using GeneratorBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiGenerator.Templates;

namespace WebApiGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ModulesBuilder mb = new ModulesBuilder("BetonCRM.Common", "BaseModel");
            mb.Build();

            // create Global.asax
            GlobalAsaxTemplate gat = new GlobalAsaxTemplate(mb.Modules);
            string resultgat = gat.TransformText();
            File.WriteAllText("Global.asax.cs", resultgat);

            // create DbContext file
            DbContextTemplate dct = new DbContextTemplate(mb.Modules);
            string resultdct = dct.TransformText();
            File.WriteAllText("BetonCRMContext.cs", resultdct);

            foreach (Module module in mb.Modules)
            {
                if (!Directory.Exists("Controllers"))
                    Directory.CreateDirectory("Controllers");

                // create module folders
                string moduleFolderControllers = $"Controllers\\{module.ModuleName}";
                string moduleFolderContracts = $"Contracts\\{module.ModuleName}";
                string moduleFolderBusinesses = $"Businesses\\{module.ModuleName}";
                if (!Directory.Exists(moduleFolderControllers))
                    Directory.CreateDirectory(moduleFolderControllers);
                if (!Directory.Exists(moduleFolderContracts))
                    Directory.CreateDirectory(moduleFolderContracts);
                if (!Directory.Exists(moduleFolderBusinesses))
                    Directory.CreateDirectory(moduleFolderBusinesses);

                // loop types in module
                foreach (Type type in module.Models)
                {
                    if (type.BaseType == typeof(Enum))
                        continue;

                    // create controller
                    ApiControllerTemplate act = new ApiControllerTemplate(type, module.ModuleName);
                    string result = act.TransformText();
                    File.WriteAllText($"{moduleFolderControllers}\\{type.Name}Controller.cs", result);

                    // create IBusiness interface for business class
                    IBusinessTemplate ibt = new IBusinessTemplate(type, module.ModuleName);
                    string result2 = ibt.TransformText();
                    File.WriteAllText($"{moduleFolderContracts}\\I{type.Name}Business.cs", result2);

                    // create Business classes
                    BusinessTemplate bt = new BusinessTemplate(type, module.ModuleName);
                    string result3 = bt.TransformText();
                    File.WriteAllText($"{moduleFolderBusinesses}\\{type.Name}Business.cs", result3);
                }
            }
        }
    }
}
