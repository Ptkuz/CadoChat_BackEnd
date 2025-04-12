using CadoChat.Auth.EF;
using CadoChat.Auth.EF.Entities;
using CadoChat.AuthManager.Services.Interfaces;
using CadoChat.AuthService.MediatR.RequstCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ITokenManagerService<User> _tokenManagerService;

    public AuthController(ITokenManagerService<User> tokenManagerService, IMediator mediator)
    {
        _tokenManagerService = tokenManagerService;
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginViaEmail([FromBody] LoginViaEmailCommand command)
    {
        var result = await _mediator.Send(command).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {

        var result = await _mediator.Send(command).ConfigureAwait(false);
        return Ok(result);
    }
}
