using CadoChat.Security.Authorization.Services;
using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.ChatManager.Services
{
    public class ConfigurationAuthorizationManagerService : ConfigurationAuthorizationService
    {
        public override void AddService(WebApplicationBuilder webApplicationBuilder)
        {

            var chatService = GlobalSettingsLoader.GetInstance().GlobalSettings.Services.ChatService;

            webApplicationBuilder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(chatService.SendMessageScope.Name, policy =>
                    policy.RequireClaim("scope", chatService.SendMessageScope.Name));

                options.AddPolicy(chatService.ReceiveMessageScope.Name, policy =>
                   policy.RequireClaim("scope", chatService.ReceiveMessageScope.Name));
            });
        }
    }
}
