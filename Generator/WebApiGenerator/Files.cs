using System.Collections.Generic;
using System.IO;

namespace WebApiGenerator
{
    public static class Files
    {
        public static string ProjectName { get; set; }
        public static string[] List => new[]
        {
            $"App_Start{Path.DirectorySeparatorChar}BundleConfig.cs",
            $"App_Start{Path.DirectorySeparatorChar}FilterConfig.cs",
            $"App_Start{Path.DirectorySeparatorChar}IdentityConfig.cs",
            $"App_Start{Path.DirectorySeparatorChar}RouteConfig.cs",
            $"App_Start{Path.DirectorySeparatorChar}Startup.Auth.cs",
            $"App_Start{Path.DirectorySeparatorChar}WebApiConfig.cs",
            $"Controllers{Path.DirectorySeparatorChar}AccountController.cs",
            $"Controllers{Path.DirectorySeparatorChar}BaseController.cs",
        };

        public static Dictionary<string, string> Dictionary => new Dictionary<string, string>
        {
            { nameof(Resources.BundleConfig), $"{ProjectName}{Path.DirectorySeparatorChar}App_Start{Path.DirectorySeparatorChar}BundleConfig.cs" },
            { nameof(Resources.FilterConfig), $"{ProjectName}{Path.DirectorySeparatorChar}App_Start{Path.DirectorySeparatorChar}FilterConfig.cs" },
            { nameof(Resources.IdentityConfig), $"{ProjectName}{Path.DirectorySeparatorChar}App_Start{Path.DirectorySeparatorChar}IdentityConfig.cs" },
            { nameof(Resources.RouteConfig), $"{ProjectName}{Path.DirectorySeparatorChar}App_Start{Path.DirectorySeparatorChar}RouteConfig.cs" },
            { nameof(Resources.Startup_Auth), $"{ProjectName}{Path.DirectorySeparatorChar}App_Start{Path.DirectorySeparatorChar}Startup.Auth.cs" },
            { nameof(Resources.WebApiConfig), $"{ProjectName}{Path.DirectorySeparatorChar}App_Start{Path.DirectorySeparatorChar}WebApiConfig.cs" },

            { nameof(Resources.AccountController), $"{ProjectName}{Path.DirectorySeparatorChar}Controllers{Path.DirectorySeparatorChar}AccountController.cs" },
            { nameof(Resources.BaseController), $"{ProjectName}{Path.DirectorySeparatorChar}Controllers{Path.DirectorySeparatorChar}BaseController.cs" },

            { nameof(Resources.ClaimsAuthorizationAttribute), $"{ProjectName}{Path.DirectorySeparatorChar}Filters{Path.DirectorySeparatorChar}ClaimsAuthorizationAttribute.cs" },
            { nameof(Resources.CustomAuthorizeAttribute), $"{ProjectName}{Path.DirectorySeparatorChar}Filters{Path.DirectorySeparatorChar}CustomAuthorizeAttribute.cs" },
            { nameof(Resources.LogActionFilter), $"{ProjectName}{Path.DirectorySeparatorChar}Filters{Path.DirectorySeparatorChar}LogActionFilter.cs" },
            { nameof(Resources.LogExceptionFilterAttribute), $"{ProjectName}{Path.DirectorySeparatorChar}Filters{Path.DirectorySeparatorChar}LogExceptionFilterAttribute.cs" },
            { nameof(Resources.UpdateFKAttribute), $"{ProjectName}{Path.DirectorySeparatorChar}Filters{Path.DirectorySeparatorChar}UpdateFKAttribute.cs" },
            { nameof(Resources.ValidateModelAttribute), $"{ProjectName}{Path.DirectorySeparatorChar}Filters{Path.DirectorySeparatorChar}ValidateModelAttribute.cs" },

            { nameof(Resources.ReflectionExtensions), $"{ProjectName}{Path.DirectorySeparatorChar}Helpers{Path.DirectorySeparatorChar}ReflectionExtensions.cs" },
            { nameof(Resources.MemoryCacheManager), $"{ProjectName}{Path.DirectorySeparatorChar}Helpers{Path.DirectorySeparatorChar}MemoryCacheManager.cs" },

            { nameof(Resources.AssemblyInfo), $"{ProjectName}{Path.DirectorySeparatorChar}Properties{Path.DirectorySeparatorChar}AssemblyInfo.cs" },

            { nameof(Resources.CustomJwtFormat), $"{ProjectName}{Path.DirectorySeparatorChar}Providers{Path.DirectorySeparatorChar}CustomJwtFormat.cs" },
            { nameof(Resources.CustomOAuthProvider), $"{ProjectName}{Path.DirectorySeparatorChar}Providers{Path.DirectorySeparatorChar}CustomOAuthProvider.cs" },

            { nameof(Resources.AccountBindingModels), $"{ProjectName}{Path.DirectorySeparatorChar}Models{Path.DirectorySeparatorChar}AccountBindingModels.cs" },
            { nameof(Resources.AccountViewModels), $"{ProjectName}{Path.DirectorySeparatorChar}Models{Path.DirectorySeparatorChar}AccountViewModels.cs" },
            { nameof(Resources.ApplicationRoleManager), $"{ProjectName}{Path.DirectorySeparatorChar}Models{Path.DirectorySeparatorChar}ApplicationRoleManager.cs" },
            { nameof(Resources.ModelFactory), $"{ProjectName}{Path.DirectorySeparatorChar}Models{Path.DirectorySeparatorChar}ModelFactory.cs" },
            { nameof(Resources.IdentityModels), $"{ProjectName}{Path.DirectorySeparatorChar}Models{Path.DirectorySeparatorChar}IdentityModels.cs" },
            { nameof(Resources.Role), $"{ProjectName}{Path.DirectorySeparatorChar}Models{Path.DirectorySeparatorChar}Role.cs" },

            { nameof(Resources.ChallengeResult), $"{ProjectName}{Path.DirectorySeparatorChar}Results{Path.DirectorySeparatorChar}ChallengeResult.cs" },

            { nameof(Resources.EmailService), $"{ProjectName}{Path.DirectorySeparatorChar}Services{Path.DirectorySeparatorChar}EmailService.cs" },

            { nameof(Resources.Global), $"{ProjectName}{Path.DirectorySeparatorChar}Global.asax" },
            { nameof(Resources.packages), $"{ProjectName}{Path.DirectorySeparatorChar}packages.config" },
            { nameof(Resources.Startup), $"{ProjectName}{Path.DirectorySeparatorChar}Startup.cs" },
            { nameof(Resources.Web), $"{ProjectName}{Path.DirectorySeparatorChar}Web.config" },

            { nameof(Resources.SolutionFile), $"{ProjectName}.sln" },

            // Business Proj Files
            { nameof(Resources.AssemblyInfoBusiness), $"{ProjectName}.Business{Path.DirectorySeparatorChar}Properties{Path.DirectorySeparatorChar}AssemblyInfo.cs" },
            { nameof(Resources.packagesBusiness), $"{ProjectName}.Business{Path.DirectorySeparatorChar}packages.config" },
            { nameof(Resources.BaseBusiness), $"{ProjectName}.Business{Path.DirectorySeparatorChar}BaseBusiness.cs" },

            // Contracts Proj Files
            { nameof(Resources.AssemblyInfoContracts), $"{ProjectName}.Contracts{Path.DirectorySeparatorChar}Properties{Path.DirectorySeparatorChar}AssemblyInfo.cs" },
            { nameof(Resources.IBaseData), $"{ProjectName}.Contracts{Path.DirectorySeparatorChar}DataAccess{Path.DirectorySeparatorChar}IBaseData.cs" },
            { nameof(Resources.IRepository), $"{ProjectName}.Contracts{Path.DirectorySeparatorChar}DataAccess{Path.DirectorySeparatorChar}IRepository.cs" },
            { nameof(Resources.IUnitOfWork), $"{ProjectName}.Contracts{Path.DirectorySeparatorChar}DataAccess{Path.DirectorySeparatorChar}IUnitOfWork.cs" },
            { nameof(Resources.IBaseBusiness), $"{ProjectName}.Contracts{Path.DirectorySeparatorChar}Business{Path.DirectorySeparatorChar}IBaseBusiness.cs" },

            // DataAccess Proj Files
            { nameof(Resources.AssemblyInfoDataAccess), $"{ProjectName}.DataAccess{Path.DirectorySeparatorChar}Properties{Path.DirectorySeparatorChar}AssemblyInfo.cs" },
            { nameof(Resources.packagesDataAccess), $"{ProjectName}.DataAccess{Path.DirectorySeparatorChar}packages.config" },
            { nameof(Resources.AppDataAccess), $"{ProjectName}.DataAccess{Path.DirectorySeparatorChar}App.config" },
            { nameof(Resources.BaseData), $"{ProjectName}.DataAccess{Path.DirectorySeparatorChar}Data{Path.DirectorySeparatorChar}BaseData.cs" },
            { nameof(Resources.EFRepository), $"{ProjectName}.DataAccess{Path.DirectorySeparatorChar}Repositories{Path.DirectorySeparatorChar}EFRepository.cs" },
            { nameof(Resources.EFRepositoryBase), $"{ProjectName}.DataAccess{Path.DirectorySeparatorChar}Repositories{Path.DirectorySeparatorChar}EFRepositoryBase.cs" },
            { nameof(Resources.EFUnitOfWork), $"{ProjectName}.DataAccess{Path.DirectorySeparatorChar}UnitOfWork{Path.DirectorySeparatorChar}EFUnitOfWork.cs" },
            { nameof(Resources.EFUnitOfWorkBase), $"{ProjectName}.DataAccess{Path.DirectorySeparatorChar}UnitOfWork{Path.DirectorySeparatorChar}EFUnitOfWorkBase.cs" },
        };
    }
}
