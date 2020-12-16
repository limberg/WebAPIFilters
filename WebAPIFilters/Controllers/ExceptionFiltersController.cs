using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;

namespace WebAPIFilters.Controllers
{

    //OPTION 1
    public class MyExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public Task ExecuteExceptionFilterAsync(
            HttpActionExecutedContext context, 
            CancellationToken cancellationToken)
        {
            
            Action action = () => 
            {
                if (context.Exception is EndOfWorldEception)
                {
                    var actionName = context.ActionContext.ActionDescriptor.ActionName;

                    var msg = $"Exception Filter {actionName}";

                    Trace.WriteLine(msg);

                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, msg);

                }
            };
            //Func<int, bool > func = (i) => { return true; };

            var task = new Task(action);
            task.Start();

            return task;
        }
    }

    //OPTION 2
    public class MyExceptionFilter2 : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if(context.Exception is EndOfWorldEception)
            {
                var actionName = context.ActionContext.ActionDescriptor.ActionName;
                var msg = $"My exception Filter {actionName}";

                Trace.WriteLine(msg);

                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, msg);
            }
        }
    }
    public class EndOfWorldEception : Exception { }

    //[RoutePrefix("excecptionsexample")]
    public class ExceptionFiltersController : ApiController
    {
        [MyExceptionFilter]
        [Route ("api/ef/get")]
        public string Get()
        {
            throw new EndOfWorldEception();
        }

        //[MyExceptionFilter2]
        [Route("api/ef/get2")]
        public string Get2()
        {
            throw new EndOfWorldEception();
        }

        [Route("api/ef/getnotexceptions")]
        public string GetNotEsception()
        {
            return "Not Exception";
        }
    }
}
