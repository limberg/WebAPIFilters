using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace WebAPIFilters.Controllers
{
    [BasicAuthentication.BasicAuthentication]
    public class BasicAuthenticationController : ApiController
    {
       
        [Route("api/basicauthentication")]
        public string Get()
        {
            string usernameAuthenticated = Thread.CurrentPrincipal.Identity.Name;

            return $"Authenticated Username: {usernameAuthenticated}";
        }
    }
}
