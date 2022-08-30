using InternationalBusinessMen.Middleware;
using Microsoft.AspNetCore.Builder;

namespace InternationalBusinessMen.Extensions
{
    public static class LoggingMiddlewareExtensions
    {
        /// <summary>
        /// Función encargada de inyectar el middleware para registrar información en caso de generarse excepciónes.
        /// </summary>
        /// <param name="builder">Objeto de construcción de la aplicación</param>
        /// <returns>Referencia de la inyección de dependencias.</returns>
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
