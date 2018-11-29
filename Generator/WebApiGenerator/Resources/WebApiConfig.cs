using System.Web.Http;
using System.Web.Http.Cors;

using #projectname#.Filters;
using #projectname#.Logs.Contracts;
using Microsoft.Owin.Security.OAuth;

namespace #projectname#
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.MapHttpAttributeRoutes();

            var cors = new EnableCorsAttribute("http://localhost:8080", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                //routeTemplate: "api/{module}/{controller}/{action}/{id}",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional, action = RouteParameter.Optional }
            );

            config.Filters.Add(new LogExceptionFilterAttribute(config.DependencyResolver.GetService(typeof(IErrorBusiness)) as IErrorBusiness));
        }
    }
}
