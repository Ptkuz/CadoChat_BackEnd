using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Security.Validation.SecutiryInfo
{
    public static class AccessScopes
    {
        public static readonly KeyValuePair<string, string> SendMessage = new KeyValuePair<string, string>("send_message", "Send message");
        public static readonly KeyValuePair<string, string> ReceiveMessage = new KeyValuePair<string, string>("admin_access", "Full admin access");
    }
}
