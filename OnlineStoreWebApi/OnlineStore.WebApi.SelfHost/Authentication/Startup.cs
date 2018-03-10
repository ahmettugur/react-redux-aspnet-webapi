//using Microsoft.Owin;
//using Microsoft.Owin.Security.OAuth;
//using Owin;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Http;

//[assembly: OwinStartup(typeof(OnlineStore.WebApi.SelfHost.Authentication.Startup))]
//namespace OnlineStore.WebApi.SelfHost.Authentication
//{
//    public class Startup
//    {
//        public void Configuration(IAppBuilder app)
//        {
//            HttpConfiguration config = new HttpConfiguration();
//            //config.DependencyResolver = new NinjectResolver(new Ninject.Web.Common.Bootstrapper().Kernel);
//            ConfigureOAuth(app);
//            OnlineStore.WebApi.SelfHost.Startup.Configuration(config);
//            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
//            app.UseWebApi(config);

//        }

//        private void ConfigureOAuth(IAppBuilder app)
//        {
//            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
//            {
//                TokenEndpointPath = new Microsoft.Owin.PathString("/token"),
//                AccessTokenExpireTimeSpan = TimeSpan.FromHours(10),
//                AllowInsecureHttp = true,
//                Provider = new AuthorizationServerProvider()
//            };


//            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
//            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
//        }
//    }
//}
