using BetonCRM.Filters;
using BetonCRM.Logs.Contracts;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BetonCRM
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.MapHttpAttributeRoutes();

            var cors = new EnableCorsAttribute("http://localhost:8080, http://localhost:8081, http://develibeton.net, http://www.betoncrm.com, http://hakdag-001-site1.atempurl.com", "*", "*");
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
