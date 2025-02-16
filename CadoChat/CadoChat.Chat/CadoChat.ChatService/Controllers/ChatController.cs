using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadoChat.ChatService.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        [HttpGet("messages")]
        [Authorize]
        public IActionResult GetMessages()
        {
            return Ok(new { messages = new[] { "Hello", "How are you?", "Goodbye" } });
        }
    }
}
