using CadoChat.Auth.EF;
using CadoChat.Auth.EF.Entities;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthService.Models;
using CadoChat.DAL.EF.FacadeRepository;
using CadoChat.DAL.Entity.FacadeRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenManagerService<User> _tokenManagerService;
    private readonly IAuthUnifOfWork _authUnifOfWork;

    public AuthController(ITokenManagerService<User> tokenManagerService, IAuthUnifOfWork authUnifOfWork)
    {
        _userManager = null;
        _signInManager = null;
        _tokenManagerService = tokenManagerService;
        _authUnifOfWork = authUnifOfWork;
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

        var res = await _authUnifOfWork.UserRepository.CreateRepository.AddEntityAsync(new User { Username = model.Username, Email = model.Email, PasswordHash = "23432432432" });

        if (res.Success)
        {
            await _authUnifOfWork.SaveChangesAsync();
        }

        var user = new User { Username = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { message = "User registered successfully" });
    }
}
