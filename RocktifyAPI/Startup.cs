using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Net.Http.Headers;
using Ninject.Web.Common.OwinHost;
using Ninject;
using Ninject.Web.WebApi.OwinHost;
using RocktifyAPI.Filters;
using Microsoft.Owin.Cors;
using System.Web.Cors;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(RocktifyAPI.Startup))]

namespace RocktifyAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            var webApiConfiguration = new HttpConfiguration();

            // Web API routes
            webApiConfiguration.MapHttpAttributeRoutes();

            var corsOptions = new CorsOptions
            {
                //Add CORS Policy
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => 
                    {
                        //Task.FromResult(new CorsPolicy
                        var policy = new CorsPolicy
                        {
                            AllowAnyHeader = true,
                            AllowAnyMethod = true,
                            AllowAnyOrigin = true,
                            SupportsCredentials = true
                        };
                        policy.Origins.Add("http://localhost:8080");
                        policy.Origins.Add("https://accounts.spotify.com/api/token");

                        return Task.FromResult(policy);
                    }
                }
            };

            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");

            webApiConfiguration.EnableCors(cors);
            
            //Apply filter to all Web API Controllers
            webApiConfiguration.Filters.Add(new LoggerFilter());

            // Config content type to JSON
            webApiConfiguration.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            // Convention-based routing.
            webApiConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "values" });


            // Inject Ninject into Owin Pipeline
            // Inject Web API Config into Ninject
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(webApiConfiguration).UseCors(corsOptions);

        }

        public static StandardKernel CreateKernel()
        {
            return new StandardKernel(new NinjectBindings());
        }
    }
}
