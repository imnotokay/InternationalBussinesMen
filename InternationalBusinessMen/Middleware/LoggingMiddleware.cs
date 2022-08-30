using InternationalBusinessMen.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using InternationalBusinessMen.Domain.Exceptions;

namespace InternationalBusinessMen.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger("International_Busiuness_Men_Logs");
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(ErrorException error)
            {
                this._logger.LogError(error.Message);
            }catch(WarningException warning)
            {
                this._logger.LogWarning(warning.Message);
            }
            catch(Exception exception)
            {
                Exception internalException = exception;
                int errorPosition = 1;
                string messageException = string.Format("Error número {0}: {1}", errorPosition, exception.Message);
                while(internalException.InnerException != null)
                {
                    internalException = internalException.InnerException;
                    messageException = string.Concat(messageException, Environment.NewLine, string.Format("Error número {0}: {1}", errorPosition, internalException.Message));
                }
                this._logger.LogError(messageException);
            }
        }
    }

}
