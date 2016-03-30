using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Storytime.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext context)
        {
            logger.Error(context.Exception, "An Error has occurred");
            
            context.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("Something went wrong, we are looking into it!"),
                ReasonPhrase = "Critical Exception"
            };
        }
    }
}