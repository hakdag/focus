using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiGenerator
{
    public static class Files
    {
        public static string ProjectName { get; set; }
        public static string[] List => new[]
        {
            "App_Start\\BundleConfig.cs",
            "App_Start\\FilterConfig.cs",
            "App_Start\\IdentityConfig.cs",
            "App_Start\\RouteConfig.cs",
            "App_Start\\Startup.Auth.cs",
            "App_Start\\WebApiConfig.cs",
            "Controllers\\AccountController.cs",
            "Controllers\\BaseController.cs",
        };

        public static Dictionary<string, string> Dictionary => new Dictionary<string, string>
        {
            { nameof(Resources.BundleConfig), $"Backend\\{ProjectName}\\App_Start\\BundleConfig.cs" },
            { nameof(Resources.FilterConfig), $"Backend\\{ProjectName}\\App_Start\\FilterConfig.cs" },
            { nameof(Resources.IdentityConfig), $"Backend\\{ProjectName}\\App_Start\\IdentityConfig.cs" },
            { nameof(Resources.RouteConfig), $"Backend\\{ProjectName}\\App_Start\\RouteConfig.cs" },
            { nameof(Resources.Startup_Auth), $"Backend\\{ProjectName}\\App_Start\\Startup.Auth.cs" },
            { nameof(Resources.WebApiConfig), $"Backend\\{ProjectName}\\App_Start\\WebApiConfig.cs" },

            { nameof(Resources.AccountController), $"Backend\\{ProjectName}\\Controllers\\AccountController.cs" },
            { nameof(Resources.BaseController), $"Backend\\{ProjectName}\\Controllers\\BaseController.cs" },

            { nameof(Resources.ClaimsAuthorizationAttribute), $"Backend\\{ProjectName}\\Filters\\ClaimsAuthorizationAttribute.cs" },
            { nameof(Resources.CustomAuthorizeAttribute), $"Backend\\{ProjectName}\\Filters\\CustomAuthorizeAttribute.cs" },
            { nameof(Resources.LogActionFilter), $"Backend\\{ProjectName}\\Filters\\LogActionFilter.cs" },
            { nameof(Resources.LogExceptionFilterAttribute), $"Backend\\{ProjectName}\\Filters\\LogExceptionFilterAttribute.cs" },
            { nameof(Resources.UpdateFKAttribute), $"Backend\\{ProjectName}\\Filters\\UpdateFKAttribute.cs" },
            { nameof(Resources.ValidateModelAttribute), $"Backend\\{ProjectName}\\Filters\\ValidateModelAttribute.cs" },

            { nameof(Resources.ReflectionExtensions), $"Backend\\{ProjectName}\\Helpers\\ReflectionExtensions.cs" },
            { nameof(Resources.MemoryCacheManager), $"Backend\\{ProjectName}\\Helpers\\MemoryCacheManager.cs" },

            { nameof(Resources.AssemblyInfo), $"Backend\\{ProjectName}\\Properties\\AssemblyInfo.cs" },

            { nameof(Resources.CustomJwtFormat), $"Backend\\{ProjectName}\\Providers\\CustomJwtFormat.cs" },
            { nameof(Resources.CustomOAuthProvider), $"Backend\\{ProjectName}\\Providers\\CustomOAuthProvider.cs" },

            { nameof(Resources.AccountBindingModels), $"Backend\\{ProjectName}\\Models\\AccountBindingModels.cs" },
            { nameof(Resources.AccountViewModels), $"Backend\\{ProjectName}\\Models\\AccountViewModels.cs" },
            { nameof(Resources.ApplicationRoleManager), $"Backend\\{ProjectName}\\Models\\ApplicationRoleManager.cs" },
            { nameof(Resources.ModelFactory), $"Backend\\{ProjectName}\\Models\\ModelFactory.cs" },
            { nameof(Resources.IdentityModels), $"Backend\\{ProjectName}\\Models\\IdentityModels.cs" },
            { nameof(Resources.Role), $"Backend\\{ProjectName}\\Models\\Role.cs" },

            { nameof(Resources.ChallengeResult), $"Backend\\{ProjectName}\\Results\\ChallengeResult.cs" },

            { nameof(Resources.EmailService), $"Backend\\{ProjectName}\\Services\\EmailService.cs" },

            { nameof(Resources.Global), $"Backend\\{ProjectName}\\Global.asax" },
            { nameof(Resources.packages), $"Backend\\{ProjectName}\\packages.config" },
            { nameof(Resources.Startup), $"Backend\\{ProjectName}\\Startup.cs" },
            { nameof(Resources.Web), $"Backend\\{ProjectName}\\Web.config" },

            { nameof(Resources.SolutionFile), $"Backend\\{ProjectName}.sln" },
        };
    }
}
