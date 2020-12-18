using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace WebAPIOAuthDemo.OAuthProvider
{
    public class MyOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userIdentity = new ClaimsIdentity(context.Options.AuthenticationType);

            //Read Credentitials from context and Validate wirh User DB

            userIdentity.AddClaim(new Claim(ClaimTypes.Name, "Limberg"));
            userIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));

            context.Validated(userIdentity);

            return;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }
    }
}