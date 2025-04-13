using CadoChat.Web.Common.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CadoChat.ChatManager.WebConfigurations
{
    public static class ChatAuthorizationConfiguration
    {

        private static GlobalSettingsLoader GlobalSettingsLoader =>
            GlobalSettingsLoader.Instance
            ?? throw new ArgumentNullException();

        public static void UseChatAuthorizationService(this WebApplication applicationBuilder)
        {
            applicationBuilder.UseAuthorization();
        }

        public static void AddChatAuthorizationService(this WebApplicationBuilder webApplicationBuilder)
        {

            var chatService = GlobalSettingsLoader.GlobalSettings.Services.ChatService;

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
