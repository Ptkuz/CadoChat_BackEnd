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
    public class ChatAuthorizationConfiguration : AuthorizationConfiguration
    {
        public override void AddService(WebApplicationBuilder webApplicationBuilder)
        {

            var chatService = GlobalSettings.Services.ChatService;

            webApplicationBuilder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(chatService.ChatScopeConfig.SendMessageScope.Name, policy =>
                    policy.RequireClaim("scope", chatService.ChatScopeConfig.SendMessageScope.Name));

                options.AddPolicy(chatService.ChatScopeConfig.ReceiveMessageScope.Name, policy =>
                   policy.RequireClaim("scope", chatService.ChatScopeConfig.ReceiveMessageScope.Name));
            });
        }
    }
}
