using CadoChat.Web.AspNetCore.WebResponsies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.Security.Authentication.Middlewaers
{
    public class AuthenticationErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationErrorMiddleware> _logger;

        public AuthenticationErrorMiddleware(RequestDelegate next, ILogger<AuthenticationErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

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
