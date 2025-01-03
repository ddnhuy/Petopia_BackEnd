using Domain.Users;

namespace Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string GenerateAccessToken(ApplicationUser user);
    string GenerateRefreshToken();
}
