using FinanceControl.Application.Abstractions;
using FinanceControl.Application.UseCases.Auth.Commands.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IDispatcher dispatcher) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await dispatcher.SendAsync(command, cancellationToken);
        
        return result.IsSuccess 
            ? Ok(result.Value)
            : Unauthorized(result.Error);
    }
}
