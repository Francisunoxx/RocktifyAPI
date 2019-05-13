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
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System.ServiceModel.Security.Tokens;

[assembly: OwinStartup(typeof(RocktifyAPI.Startup))]

namespace RocktifyAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = new HttpConfiguration();

            // Enable attribute routing
            webApiConfiguration.MapHttpAttributeRoutes();

            // Convention-based routing.
            webApiConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, controller = "values" });

            // Config content type to JSON
            webApiConfiguration.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            // Adding JSON type web api formatting.  
            webApiConfiguration.Formatters.Clear();
            webApiConfiguration.Formatters.Add(formatter);

            ConfigureOAuth(app);

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
            // Configure Web API to use only bearer token authentication.  
            //webApiConfiguration.SuppressDefaultHostAuthentication();
            //Add authentication filter that authenticates via OWIN middleware.
            webApiConfiguration.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //Apply filter to all Web API Controllers
            webApiConfiguration.Filters.Add(new LoggerFilter());

            // Inject Ninject into Owin Pipeline
            // Inject Web API Config into Ninject
            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(webApiConfiguration).UseCors(corsOptions);

        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = "http://localhost:15891";
            var audience = "QjQ3NjJCMjFFNzVGOTI5Q0E4RTRFQTg3RjVDQUE=";
            var secret = TextEncodings.Base64Url.Decode("eiVDKkYtSmFOZFJnVWtYcDJyNXU4eC9BP0QoRytLYlA=");

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[]
                    {
                        new SymmetricKeyIssuerSecurityKeyProvider(issuer, secret)
                    }
                });
        }


        public static StandardKernel CreateKernel()
        {
            return new StandardKernel(new NinjectBindings());
        }
    }
}
