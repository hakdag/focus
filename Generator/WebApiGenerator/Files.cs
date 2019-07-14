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
            { nameof(Resources.BundleConfig), $"{ProjectName}\\App_Start\\BundleConfig.cs" },
            { nameof(Resources.FilterConfig), $"{ProjectName}\\App_Start\\FilterConfig.cs" },
            { nameof(Resources.IdentityConfig), $"{ProjectName}\\App_Start\\IdentityConfig.cs" },
            { nameof(Resources.RouteConfig), $"{ProjectName}\\App_Start\\RouteConfig.cs" },
            { nameof(Resources.Startup_Auth), $"{ProjectName}\\App_Start\\Startup.Auth.cs" },
            { nameof(Resources.WebApiConfig), $"{ProjectName}\\App_Start\\WebApiConfig.cs" },

            { nameof(Resources.AccountController), $"{ProjectName}\\Controllers\\AccountController.cs" },
            { nameof(Resources.BaseController), $"{ProjectName}\\Controllers\\BaseController.cs" },

            { nameof(Resources.ClaimsAuthorizationAttribute), $"{ProjectName}\\Filters\\ClaimsAuthorizationAttribute.cs" },
            { nameof(Resources.CustomAuthorizeAttribute), $"{ProjectName}\\Filters\\CustomAuthorizeAttribute.cs" },
            { nameof(Resources.LogActionFilter), $"{ProjectName}\\Filters\\LogActionFilter.cs" },
            { nameof(Resources.LogExceptionFilterAttribute), $"{ProjectName}\\Filters\\LogExceptionFilterAttribute.cs" },
            { nameof(Resources.UpdateFKAttribute), $"{ProjectName}\\Filters\\UpdateFKAttribute.cs" },
            { nameof(Resources.ValidateModelAttribute), $"{ProjectName}\\Filters\\ValidateModelAttribute.cs" },

            { nameof(Resources.ReflectionExtensions), $"{ProjectName}\\Helpers\\ReflectionExtensions.cs" },
            { nameof(Resources.MemoryCacheManager), $"{ProjectName}\\Helpers\\MemoryCacheManager.cs" },

            { nameof(Resources.AssemblyInfo), $"{ProjectName}\\Properties\\AssemblyInfo.cs" },

            { nameof(Resources.CustomJwtFormat), $"{ProjectName}\\Providers\\CustomJwtFormat.cs" },
            { nameof(Resources.CustomOAuthProvider), $"{ProjectName}\\Providers\\CustomOAuthProvider.cs" },

            { nameof(Resources.AccountBindingModels), $"{ProjectName}\\Models\\AccountBindingModels.cs" },
            { nameof(Resources.AccountViewModels), $"{ProjectName}\\Models\\AccountViewModels.cs" },
            { nameof(Resources.ApplicationRoleManager), $"{ProjectName}\\Models\\ApplicationRoleManager.cs" },
            { nameof(Resources.ModelFactory), $"{ProjectName}\\Models\\ModelFactory.cs" },
            { nameof(Resources.IdentityModels), $"{ProjectName}\\Models\\IdentityModels.cs" },
            { nameof(Resources.Role), $"{ProjectName}\\Models\\Role.cs" },

            { nameof(Resources.ChallengeResult), $"{ProjectName}\\Results\\ChallengeResult.cs" },

            { nameof(Resources.EmailService), $"{ProjectName}\\Services\\EmailService.cs" },

            { nameof(Resources.Global), $"{ProjectName}\\Global.asax" },
            { nameof(Resources.packages), $"{ProjectName}\\packages.config" },
            { nameof(Resources.Startup), $"{ProjectName}\\Startup.cs" },
            { nameof(Resources.Web), $"{ProjectName}\\Web.config" },

            { nameof(Resources.SolutionFile), $"{ProjectName}.sln" },

            // Business Proj Files
            { nameof(Resources.AssemblyInfoBusiness), $"{ProjectName}.Business\\Properties\\AssemblyInfo.cs" },
            { nameof(Resources.packagesBusiness), $"{ProjectName}.Business\\packages.config" },
            { nameof(Resources.BaseBusiness), $"{ProjectName}.Business\\BaseBusiness.cs" },

            // Contracts Proj Files
            { nameof(Resources.AssemblyInfoContracts), $"{ProjectName}.Contracts\\Properties\\AssemblyInfo.cs" },
            { nameof(Resources.IBaseData), $"{ProjectName}.Contracts\\DataAccess\\IBaseData.cs" },
            { nameof(Resources.IRepository), $"{ProjectName}.Contracts\\DataAccess\\IRepository.cs" },
            { nameof(Resources.IUnitOfWork), $"{ProjectName}.Contracts\\DataAccess\\IUnitOfWork.cs" },
            { nameof(Resources.IBaseBusiness), $"{ProjectName}.Contracts\\Business\\IBaseBusiness.cs" },

            // DataAccess Proj Files
            { nameof(Resources.AssemblyInfoDataAccess), $"{ProjectName}.DataAccess\\Properties\\AssemblyInfo.cs" },
            { nameof(Resources.packagesDataAccess), $"{ProjectName}.DataAccess\\packages.config" },
            { nameof(Resources.AppDataAccess), $"{ProjectName}.DataAccess\\App.config" },
            { nameof(Resources.BaseData), $"{ProjectName}.DataAccess\\Data\\BaseData.cs" },
            { nameof(Resources.EFRepository), $"{ProjectName}.DataAccess\\Repositories\\EFRepository.cs" },
            { nameof(Resources.EFRepositoryBase), $"{ProjectName}.DataAccess\\Repositories\\EFRepositoryBase.cs" },
            { nameof(Resources.EFUnitOfWork), $"{ProjectName}.DataAccess\\UnitOfWork\\EFUnitOfWork.cs" },
            { nameof(Resources.EFUnitOfWorkBase), $"{ProjectName}.DataAccess\\UnitOfWork\\EFUnitOfWorkBase.cs" },
        };
    }
}
