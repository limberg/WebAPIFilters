using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebAPIFilters.Controllers
{
    //Option implement IActionFilter interface
    public class MyActionFilter : Attribute, IActionFilter
    {

        private readonly string _id;
        public bool AllowMultiple => true;

        public MyActionFilter(string id)
        {
            _id = id;
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(
            HttpActionContext actionContext, 
            CancellationToken cancellationToken, 
            Func<Task<HttpResponseMessage>> continuation)
        {

            var actionName = actionContext.ActionDescriptor.ActionName;

            Trace.WriteLine($"{_id} Before action execution of ActionFilter {actionName}" );

            var result = continuation();
            result.Wait();

            Trace.WriteLine($"{_id} After action execution of ActionFilter {actionName}");

            return result;
        }
    }

    //Option Extend ActionFilterAttributte class
    public class MyActionFilter2 : ActionFilterAttribute
    {

        private readonly string _id;

        public MyActionFilter2(string id)
        {
            _id = id;
        }
        //Brfore
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var actionName = actionContext.ActionDescriptor.ActionName;

            Trace.WriteLine($"{_id} Before Action of {actionName}");
        }


        //After
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            Trace.WriteLine($"{_id} After Action of {actionName}");
        }
    }


    [MyActionFilter("Filter at Controller Level")]
    public class ActionFilterController : ApiController
    {
        [OverrideActionFilters] //Disable filters at controller and Global scopes
        [MyActionFilter("Filter 1")]
        [Route("api/af/get")]
        public string Get()
        {
            Trace.WriteLine("Inside action methode get...");
            return "Action Get from Action Filter Controller";
        }

        [MyActionFilter2("Filter 2")]
        [Route("api/af/get2")]
        public string Get2()
        {
            Trace.WriteLine("Inside action methode get2...");
            return "Action Get2 from Action Filter Controller";
        }
    }
}
