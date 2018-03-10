using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OnlineStore.Core.ResponseHandler;
using OnlineStore.WebApi.SelfHost.Authentication;
using OnlineStore.WebApi.SelfHost.Infastructure;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(OnlineStore.WebApi.SelfHost.Startup))]
namespace OnlineStore.WebApi.SelfHost
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(appBuilder);
            config.MessageHandlers.Add(new ApiResponseHandler());
            config.DependencyResolver = new NinjectResolver(NinjectConfig.CreateKernel());
            //config.MessageHandlers.Add(new AuthenticationHandler());

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            var cors = new EnableCorsAttribute("http://localhost:3000", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(10),
                AllowInsecureHttp = true,
                Provider = new AuthorizationServerProvider()
            };


            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
