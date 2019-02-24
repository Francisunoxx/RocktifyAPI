using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RocktifyAPI.Filters
{
    public class LoggerFilter : IActionFilter
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public bool AllowMultiple => throw new NotImplementedException();

        //HttpActionContext - Contains information for the executing action.
        /*public override void OnActionExecuting(HttpActionContext actionContext)
        {
            logger.Debug("Uri: " + actionContext.Request.RequestUri);
            logger.Debug("Action: " + actionContext.Request.Method);
            logger.Debug("Method: "+actionContext.ActionDescriptor.ActionName);
            logger.Debug(actionContext);
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
        }*/

        

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");

            var result = continuation();

            if(result.Exception == null)
            {

            }

            result.Wait();

            Trace.WriteLine(string.Format("Action Method {0} executed at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");

            return result;
        }
    }
}