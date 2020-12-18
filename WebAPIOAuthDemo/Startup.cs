using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebAPIOAuthDemo.OAuthProvider;

[assembly: OwinStartup(typeof(WebAPIOAuthDemo.Startup))]

namespace WebAPIOAuthDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            //2. Configure Options fpr UseOAuthAuthorizationServer
            //OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions();
            //options.AllowInsecureHttp = true;
            //options.Provider = new MyOAuthProvider();

            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                TokenEndpointPath = new PathString("/GetToken"),
                Provider = new MyOAuthProvider()
            });

            //3.

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //1. Configurate Route for WebAPI

            HttpConfiguration config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                             name: "Default",
                             routeTemplate: "api/{controller}"
                         );

            app.UseWebApi(config);
        }
    }
}
