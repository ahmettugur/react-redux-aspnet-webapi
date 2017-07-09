using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OnlineStore.Api.Core;
using OnlineStore.Services.Users;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

[assembly: OwinStartup(typeof(OnlineStore.Api.Authentication.Startup))]
namespace OnlineStore.Api.Authentication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            //config.Services.Replace(typeof(IHttpControllerActivator), new WindsorHttpControllerActivator());
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
           
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new Microsoft.Owin.PathString("/token"), // token adresi
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(10),//10 Saat geçerli
                AllowInsecureHttp = true,
                Provider = new AuthorizationServerProvider()
            // Burada hata alırsanızz saglayıcı klasınızız doğru ayarladığınızdan emin olun.
            };


            app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions); // Ayarladığımız config dosyasının server'a kullanması için gönderiyoruz.
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());// Bearer Authentication'ı kullanacağımızı belirttik.
        }
    }
}