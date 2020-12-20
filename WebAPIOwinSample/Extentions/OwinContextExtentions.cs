using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPIOwinSample
{
    public static class OwinContextExtentions
    {

        public static string GetUserId(this IOwinContext context)
        {
            var result = "-1";

            var claim = context.Authentication.User.Claims.FirstOrDefault(c => c.Type == "UserID");
            if (claim != null)
                result = claim.Value;

            return result;
        }
    }
}