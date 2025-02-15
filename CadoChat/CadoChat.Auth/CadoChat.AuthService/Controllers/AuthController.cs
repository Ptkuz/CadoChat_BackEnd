using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpGet("check")]
    public IActionResult CheckAuth()
    {
        return Ok(new { message = "AuthService is working through API Gateway!" });
    }
}
