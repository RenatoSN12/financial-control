using System.Security.Claims;
using FinanceControl.Application.UseCases.Abstractions;

namespace FinanceControl.Api.Services;

public class CurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    public Guid UserId =>
        Guid.Parse(accessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
