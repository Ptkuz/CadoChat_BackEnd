using CadoChat.Web.Common.Services;
using CadoChat.Web.Common.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadoChat.ChatService.Controllers
{
    [ApiController]
    [Authorize(Policy = "ChatService.SendMessage")]
    [Route("api/chat")]
    [Authorize] // Требует авторизацию с JWT
    public class ChatController : ControllerBase
    {
        [HttpGet("messages")]
        public IActionResult GetMessages()
        {
            return Ok(new { messages = new[] { "Hello", "How are you?", "Goodbye" } });
        }
    }
}
