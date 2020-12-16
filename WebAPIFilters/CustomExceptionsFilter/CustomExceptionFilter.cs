using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http;
using System.Net.Http;

namespace WebAPIFilters.CustomExceptionsFilter
{
    public class CustomExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            string msg = string.Empty;

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;


            //if(actionExecutedContext.Exception.GetType() == typeof(UnauthorizedAccessException))
            if(actionExecutedContext.Exception is UnauthorizedAccessException)
            {
                msg = $"User not authorized";
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if(actionExecutedContext.Exception is NullReferenceException)
            {
                msg = "Data not found";
                statusCode = HttpStatusCode.NotFound;
            }
            else
            {
                msg = "Error Unknow: Contact Admin";
                statusCode = HttpStatusCode.InternalServerError;
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(statusCode, msg);

            base.OnException(actionExecutedContext);
        }
    }
}