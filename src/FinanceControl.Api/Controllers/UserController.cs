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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser(
        [FromBody] CreateUserCommand command,
        CancellationToken cancellationToken
    )
    {
        var result = await dispatcher.SendAsync(command, cancellationToken);
        
        return result.IsSuccess 
            ? CreatedAtAction("colocar um nameof GetUser quando tiver", new { id = result.Value!.Id })
            : BadRequest(result.Error);
    }
}