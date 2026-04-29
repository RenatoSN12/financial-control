using FinanceControl.Application.Abstractions;
using FinanceControl.Application.UseCases.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController(IDispatcher dispatcher) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IResult> CreateUser(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await dispatcher.SendAsync(command, cancellationToken);
        return result.ToCreatedResponse("quando tiver get user");
    }
}