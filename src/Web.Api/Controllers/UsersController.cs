using System.Security.Claims;
using Application.Auth.Account;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
[ApiController]
public class UsersController(
    ISender sender) : ControllerBase
{
    [Authorize]
    [HttpPost("refresh-token/revoke")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] string UserId, CancellationToken cancellationToken)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        if (UserId != userId)
        {
            throw new InvalidOperationException("You are not authorized to revoke this refresh token.");
        }

        var command = new RevokeRefreshTokensCommand(UserId);

        await sender.Send(command, cancellationToken);

        return NoContent();
    }
}
