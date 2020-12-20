using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(WebAPIOwinSample.Startup))]

namespace WebAPIOwinSample
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions Options {get; private set;}

        static Startup()
        {
            Options = new OAuthAuthorizationServerOptions()
            {
                Provider = new OAuthProvider.OAutthAppProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(2),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token")
            };
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            app.UseOAuthAuthorizationServer(Options);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
