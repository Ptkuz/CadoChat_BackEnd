using CadoChat.Web.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.ChatManager.Services
{
    public class ChatSwaggerConfiguration : SwaggerConfiguration
    {
        public override string SwaggerTitle 
        { 
            get
            {
                return GlobalSettings.Services.ChatService.Name;
            }
        }
    }
}
