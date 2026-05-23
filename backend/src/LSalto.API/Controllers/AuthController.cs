using LSalto.Application.Common.Interfaces;
using LSalto.Application.UseCases.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LSalto.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator, IJwtTokenService jwtTokenService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);
        var token = jwtTokenService.GenerateToken(result.Id, result.Nome, result.Role);
        return Ok(new { token, result.Nome, result.Role });
    }
}
