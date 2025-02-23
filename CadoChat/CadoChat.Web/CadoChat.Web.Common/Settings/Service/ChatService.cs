using CadoChat.Web.Common.Settings.Service.Base;
using CadoChat.Web.Common.Settings.Service.Scope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.Service
{
    public class ChatService : ServiceConfig
    {
        public ScopeConfig SendMessageScope { get; set; }

        public ScopeConfig ReceiveMessageScope { get; set; }

        public ChatService() 
        {
            SendMessageScope = new ScopeConfig() { Name = "ChatService.SendMessage", DisplayValiue = "Send Message" };
            ReceiveMessageScope = new ScopeConfig() { Name = "ChatService.ReceiveMessage", DisplayValiue = "Receive Message" };
        }
    }
}
