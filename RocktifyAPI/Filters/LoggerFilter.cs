using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RocktifyAPI.Filters
{
    public class LoggerFilter : IActionFilter, IExceptionFilter
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public bool AllowMultiple => throw new NotImplementedException();

        private LogMetadata BuildRequestMetadata(HttpRequestMessage request, HttpActionContext actionContext)
        {
            LogMetadata log = new LogMetadata()
            {
                Message = "Executing",
                ActionMethod = actionContext.ActionDescriptor.ActionName,
                RequestUri = request.RequestUri.AbsolutePath,
                RequestMethod = request.Method.Method,
                RequestTimestamp = DateTime.Now
            };

            return log;
        }

        private LogMetadata BuildResponseMetadata(LogMetadata logMetadata, HttpResponseMessage response)
        {
            if (logMetadata.HasException)
            {
                logMetadata.Message = "Exception found!!!";
                logMetadata.ResponseTimestamp = DateTime.Now;
            }
            else
            {
                logMetadata.Message = "Executed";
                logMetadata.ResponseContentType = response.Content.Headers.ContentType.ToString();
                logMetadata.ResponseStatusCode = response.StatusCode;
                logMetadata.ResponseTimestamp = DateTime.Now;
            }


            return logMetadata;
        }

        private async Task SendToLog(bool onExecuting, LogMetadata logMetadata)
        {
            Task task = null;

            if (onExecuting)
            {
                task = Task.Factory.StartNew(() =>
                {
                    logger.Debug("{0} {1} method" + Environment.NewLine + "Uri: {2}, Verb: {3}, Date: {4}",
                        logMetadata.Message, logMetadata.ActionMethod, logMetadata.RequestUri, logMetadata.RequestMethod, logMetadata.RequestTimestamp);
                }
                );

                await task;
            }
            else
            {
                if (logMetadata.HasException)
                {
                    task = Task.Factory.StartNew(() =>
                    {
                        /*logger.Error(string.Format(Environment.NewLine +
                            "Exception: {0}" + Environment.NewLine +
                            "Content Type: {1}" + Environment.NewLine +
                            "Verb: {2}" + Environment.NewLine +
                            "Status Code: {3}" + Environment.NewLine +
                            "Date: {4}", logMetadata.Exception, logMetadata.RequestContentType,
                            logMetadata.RequestMethod, logMetadata.ResponseStatusCode, logMetadata.ResponseTimestamp));*/

                        /*logger.Error(string.Format(Environment.NewLine + "{0}" +
                            Environment.NewLine + "Exception: {1}" +
                            Environment.NewLine + "Verb: {2}" +
                            Environment.NewLine + "Status Code: {3}" +
                            Environment.NewLine + "Date: {4}" + logMetadata.Message, logMetadata.Exception, logMetadata.RequestMethod, logMetadata.ResponseStatusCode));*/

                        logger.Error($@": {Environment.NewLine} {logMetadata.Message}
                                    Exception: {logMetadata.Exception} {Environment.NewLine}
                                    Verb: {logMetadata.RequestMethod} {Environment.NewLine}");
                    }
                    );

                    await task;
                }
                else
                {
                    task = Task.Factory.StartNew(() =>
                    {
                        logger.Debug(string.Format(Environment.NewLine +
                            "Message: {0} {1}",
                            "Content Type: {2}" + Environment.NewLine +
                            "Verb: {3}" + Environment.NewLine +
                            "Status Code: {4}" + Environment.NewLine +
                            "Date: {5}", logMetadata.Message, logMetadata.ActionMethod,
                            logMetadata.RequestContentType, logMetadata.RequestMethod,
                            logMetadata.ResponseStatusCode, logMetadata.ResponseTimestamp));
                    }
                    );

                    await task;
                }
            }
        }


        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var logMetadata = BuildRequestMetadata(actionContext.Request, actionContext);

            await SendToLog(true, logMetadata);

            var result = continuation();

            //logMetadata.Exception = result.Exception.InnerException.Message;
            await SendToLog(false, BuildResponseMetadata(logMetadata, result.Result));

            return await result;
        }

        public async Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var logMetadata = BuildRequestMetadata(actionExecutedContext.Request, actionExecutedContext.ActionContext);
            var result = actionExecutedContext;

            logMetadata.HasException = true;
            logMetadata.ResponseStatusCode = HttpStatusCode.InternalServerError;
            logMetadata.Exception = result.Exception.InnerException.Message;

            await SendToLog(false, BuildResponseMetadata(logMetadata, null));
        }
    }
}