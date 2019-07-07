using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace auth0.helloserverless.com
{
    public class GlobalErrorHandler
    {
        public static async Task ExceptionHandlerDelegate(HttpContext context)
        {
            var logger = (ILogger<GlobalErrorHandler>)context.RequestServices.GetService(typeof(ILogger<GlobalErrorHandler>));

            var exceptionHandler = context.Features.Get<IExceptionHandlerFeature>();

            var resp = context.Response;

            if (exceptionHandler == null)
            {
                logger.LogError("IExceptionHandlerFeature is not available");

                await WriteErrorMessage(resp, null);

                return;
            }

            var ex = exceptionHandler.Error;

            try
            {
                _onError?.Invoke(context, ex);
            }
            catch { /* do nothing */ }

            if (ex == null)
            {
                logger.LogError("unknown exception caught in the global handler");

                await WriteErrorMessage(resp, null);

                return;
            }

            logger.LogError(ex, "caught in global error handler");

            await WriteErrorMessage(resp, null);
        }

        static Task WriteErrorMessage(HttpResponse response, string errorMsg)
        {
            response.StatusCode = 500;

            if (string.IsNullOrEmpty(errorMsg))
                errorMsg = "Appolgies, something went wrong. The error has been logged.";

            return response.WriteAsync(errorMsg, CancellationToken.None);
        }

        static Action<HttpContext, Exception> _onError;
        static readonly object _onErrorLock = new object();

        public static event Action<HttpContext, Exception> OnError
        {
            add
            {
                lock (_onErrorLock)
                {
                    _onError += value;
                }
            }
            remove
            {
                lock (_onErrorLock)
                {
                    _onError -= value;
                }
            }
        }
    }
}
