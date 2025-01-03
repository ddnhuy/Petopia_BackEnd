using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Web.Api.Authorization;

public sealed class AuthorizationMiddleware(
    IConfiguration configuration,
    RequestDelegate next,
    ILogger<AuthorizationMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        string? authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.CurrentCulture))
        {
            string token = authorizationHeader.Substring("Bearer ".Length);
            string userId = ValidateJwtToken(token);

            if (!string.IsNullOrEmpty(userId))
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, userId)
                    };

                var identity = new ClaimsIdentity(claims, "jwt");
                context.User = new ClaimsPrincipal(identity);
            }
        }

        await next(context);
    }

    private string? ValidateJwtToken(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        string secretKey = configuration["Jwt:Secret"]!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var handler = new JwtSecurityTokenHandler();
        try
        {
            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken? validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            string? userId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "nameid")?.Value;

            // return user's id from JWT token if validation successful
            if (!string.IsNullOrEmpty(userId))
            {
                return userId;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error validating JWT token!");
        }
        return null;
    }
}
