using InternationalBusinessMen.ApplicationCore.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternationalBusinessMen.ApplicationCore.Providers
{
    public class LoggerProvider : ILoggerService
    {
        private readonly ILogger _logger;
        public LoggerProvider(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger("International_Busiuness_Men_Logs");
        }

        /// <summary>
        /// Agrega errores al log de la aplicación
        /// </summary>
        /// <param name="message">Mensaje que se escribirá en el log</param>
        public void LogError(string message)
        {
            this._logger.LogError(message);
        }

        /// <summary>
        /// Agrega errores al log de la aplicación
        /// </summary>
        /// <param name="exception">Objeto de la excepción que se escribirá en el log</param>
        public void LogError(Exception exception)
        {
            Exception internalException = exception;
            int errorPosition = 1;
            string messageException = string.Format("Error número {0}: {1}", errorPosition, exception.Message);
            while (internalException.InnerException != null)
            {
                internalException = internalException.InnerException;
                messageException = string.Concat(messageException, Environment.NewLine, string.Format("Error número {0}: {1}", errorPosition, internalException.Message));
            }
            this._logger.LogError(messageException);
        }

        /// <summary>
        /// Agrega información al log de la aplicación
        /// </summary>
        /// <param name="message">Mensaje que se escribirá en el log</param>
        public void LogInformation(string message)
        {
            this._logger.LogInformation(message);
        }

        /// <summary>
        /// Agrega advertencias al log de la aplicación
        /// </summary>
        /// <param name="message">Mensaje que se escribirá en el log</param>
        public void LogWarning(string message)
        {
            this._logger.LogWarning(message);
        }
    }
}
