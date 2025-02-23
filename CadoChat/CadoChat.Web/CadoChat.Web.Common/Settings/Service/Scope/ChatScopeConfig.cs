using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.Common.Settings.Service.Scope
{

    /// <summary>
    /// Кофигурация областей видимости чата
    /// </summary>
    public class ChatScopeConfig
    {

        /// <summary>
        /// Конфигурация области видимости отправки сообщения
        /// </summary>
        public ScopeConfig SendMessageScope { get; set; } = null!;

        /// <summary>
        /// Конфигурация области видимости получения сообщения
        /// </summary>
        public ScopeConfig ReceiveMessageScope { get; set; } = null!;

        /// <summary>
        /// Инициализация областей видимости чата
        /// </summary>
        public ChatScopeConfig()
        {
            SendMessageScope = new ScopeConfig() { Name = "ChatService.SendMessage", DisplayValiue = "Send Message" };
            ReceiveMessageScope = new ScopeConfig() { Name = "ChatService.ReceiveMessage", DisplayValiue = "Receive Message" };
        }
    }
}
