using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadoChat.ChatService.Controllers
{
    [ApiController]
    [Route("api/messages")]
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
