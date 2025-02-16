using CadoChat.AuthService.Models;
using CadoChat.AuthService.Services.Interfaces;
using IdentityModel;
using IdentityModel.Client;
using IdentityServer8;
using IdentityServer8.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IdentityServerTools _identityServerTools;

    public AuthController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IdentityServerTools identityServerTools, ITokenGenService tokenGenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _identityServerTools = identityServerTools;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var user = new IdentityUser { UserName = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Subject, user.Id),
            new Claim(JwtClaimTypes.Name, user.UserName),
            new Claim(JwtClaimTypes.Email, user.Email)
        };

        // Генерируем токен через IdentityServer8
        var token = await _identityServerTools.IssueClientJwtAsync(
            clientId: "chat_client",
            lifetime: 3600, // 1 час
            audiences: new[] { "chat_api" }, // Аудитория
            additionalClaims: claims
        );

        return Ok(new { access_token = token });
    }
}
