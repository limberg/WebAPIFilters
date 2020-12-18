using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebAPIFilters.MessageHandlersAuthentication
{
    public class APIKeyMessageHandler:DelegatingHandler
    {
        private const string APIKeyToCheck = "apikeypassword";
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool validKey = false;
            IEnumerable<string> checkvalues;
            bool checkApiKeyExist = request.Headers.TryGetValues("APIKey", out checkvalues);

            if (checkApiKeyExist && checkvalues.FirstOrDefault().Equals(APIKeyToCheck))
            {
                validKey = true;
            }
            
            if (!validKey)
            {
                return request.CreateResponse(HttpStatusCode.Forbidden, "Invalid Authentication");
            }

            var response = await base.SendAsync(request, cancellationToken);
            return response;

        }
    }
}