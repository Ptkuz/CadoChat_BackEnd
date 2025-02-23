using CadoChat.Web.AspNetCore.WebResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Security.Authentication.Middlewaers
{

    /// <summary>
    /// Middleware для обработки ошибок аутентификации
    /// </summary>
    public class AuthenticationErrorMiddleware
    {

        /// <summary>
        /// Следующий обработчик запроса
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger<AuthenticationErrorMiddleware> _logger;

        /// <summary>
        /// Инициализировать Middleware для обработки ошибок аутентификации
        /// </summary>
        /// <param name="next">Следующий обработчик запроса</param>
        /// <param name="logger">Логгер</param>
        public AuthenticationErrorMiddleware(RequestDelegate next, ILogger<AuthenticationErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Обработать запрос
        /// </summary>
        /// <param name="context">Http контекст</param>
        public async Task Invoke(HttpContext context)
        {
            var response = context.Response;

            try
            {
                await _next(context);

                if (response.StatusCode == 401)
                {
                    var errorMessage = response.Headers["WWW-Authenticate"].ToString();
                    throw new SecurityTokenException(errorMessage);
                }

            }
            catch (SecurityTokenException ex)
            {

                var error = ex.GetType().Name;
                var message = ex;

                response.StatusCode = 401;
                response.ContentType = "application/json";

                var errorDetails = new BaseResponse(ex);
                _logger.LogError(ex, ex.Message);

                await response.WriteAsJsonAsync(errorDetails);

            }
        }
    }
}
