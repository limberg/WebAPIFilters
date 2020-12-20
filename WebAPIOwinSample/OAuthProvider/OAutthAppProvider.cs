using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPIOwinSample.OAuthProvider
{


    //Install with Managed Package
    //Microsoft.AspNet.WebApi.Owin
    //Microsoft.Owin.Host.SystemWeb
    //Microsoft.AspNet.Identity.Owin
    public class OAutthAppProvider : OAuthAuthorizationServerProvider
    {

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            return Task.Factory.StartNew(() =>
            {
                
                var username = context.UserName;
                var password = context.Password;
                Service.UserService userService = new Service.UserService();

                Models.User user = userService.GetUserByCredentials(username, password);

                if (user == null)
                {
                    context.SetError("invalid:grant", "Error");
                }
                else
                {
                    //ClaimsIdentity userIdentity = new ClaimsIdentity(context.Options.AuthenticationType);

                    //userIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                    //userIdentity.AddClaim(new Claim("UserID", user.Id.ToString()));

                    //context.Validated(userIdentity);


                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim("UserID", user.Id.ToString()) //para encotrar el UserID en otra instancia se usa la extension OwinContextExtentions
                    };

                    ClaimsIdentity oautIdentity = new ClaimsIdentity(claims, context.Options.AuthenticationType);
                    context.Validated(new AuthenticationTicket(oautIdentity, new AuthenticationProperties()));

                    //IOwinContext ctx = context.OwinContext;
                    //var ID = ctx.GetUserId();
                }
            });

        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
                context.Validated();

            return Task.FromResult<object>(null);
        }
    }
}