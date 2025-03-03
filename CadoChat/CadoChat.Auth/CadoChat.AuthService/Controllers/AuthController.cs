using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthService.Entities;
using CadoChat.AuthService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenManagerService<User> _tokenManagerService;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenManagerService<User> tokenManagerService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenManagerService = tokenManagerService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return Unauthorized("Invalid credentials");

        var token = _tokenManagerService.CreateAccessTokenAsync(user);
        return Ok(new { token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new User { UserName = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "User registered successfully" });
    }
}
