using CadoChat.IO.Json.Services.Interfaces;
using CadoChat.Security.Validation.Services.Interfaces;
using CadoChat.Web.Common.Services;
using Microsoft.IdentityModel.Tokens;

namespace CadoChat.AuthService.Initialize
{
    public static class ApplicationBuilderInitializer
    {

        public static void InitWebApplicationSettings(this WebApplicationBuilder applicationBuilder,
            ISecurityKeyService<RsaSecurityKey> securityKeyService, IFileSerializer fileSerializer)
        {

            var globalSettingsPath = applicationBuilder.Configuration["GlobalSettingsPath"];

            if (globalSettingsPath != null)
            {
                GlobalSettingsLoader.CreateInstance(globalSettingsPath, fileSerializer);
            }

            applicationBuilder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            applicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }
    }
}
