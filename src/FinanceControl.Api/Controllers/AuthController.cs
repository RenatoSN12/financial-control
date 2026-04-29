using FinanceControl.Application.Abstractions;
using FinanceControl.Application.UseCases.Auth.Commands.Login;
using FinanceControl.Application.UseCases.Auth.Commands.RefreshToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IDispatcher dispatcher) : ControllerBase
{
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await dispatcher.SendAsync(command, cancellationToken);
        return result.ToHttpResponse();
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IResult> RefreshToken(
        [FromBody] RefreshTokenCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await dispatcher.SendAsync(command, cancellationToken);
        return result.ToHttpResponse();
    }


}
