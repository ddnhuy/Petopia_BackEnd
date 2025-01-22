using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Auths;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using SharedKernel;

namespace Application.Auth.Login;

internal sealed class LoginWithThirdPartyCommandHandler(
    IApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    ITokenProvider tokenProvider,
    HttpClient httpClient) : ICommandHandler<LoginWithThirdPartyCommand, LoginResponse>
{
    private sealed record UserInfo(string Email, string FirstName, string LastName, Uri ImageUrl);

    public async Task<Result<LoginResponse>> Handle(LoginWithThirdPartyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            JObject authProfile = request.LoginProvider switch
            {
                LoginProvider.GOOGLE => await SignInWithGoogle(request.Token),
                LoginProvider.FACEBOOK => await SignInWithFacebook(request.Token),
                _ => throw new InvalidDataException("Invalid provider."),
            };

            if (authProfile is null)
            {
                return Result.Failure<LoginResponse>(AuthErrors.InvalidAccessToken);
            }

            UserInfo userInfo = GetUserInfo(authProfile, request.LoginProvider);

            ApplicationUser? userFromDb = await userManager.FindByEmailAsync(userInfo.Email);

            if (userFromDb is null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = userInfo.Email,
                    Email = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    ImageUrl = userInfo.ImageUrl,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(newUser);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, AppRoles.USER);

                    userFromDb = newUser;

                }
                else
                {
                    throw new();
                }
            }
            var loginInfo = new UserLoginInfo(request.LoginProvider, request.Token, request.LoginProvider);
            await userManager.AddLoginAsync(userFromDb, loginInfo);

            bool isLockedOut = await userManager.IsLockedOutAsync(userFromDb);
            if (isLockedOut)
            {
                throw new();
            }

            string accessToken = tokenProvider.GenerateAccessToken(userFromDb);
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userFromDb.Id,
                Token = tokenProvider.GenerateRefreshToken(),
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7),
            };

            await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success<LoginResponse>(new(accessToken, refreshToken.Token));
        }
        catch (InvalidDataException)
        {
            return Result.Failure<LoginResponse>(AuthErrors.InvalidAccessToken);
        }
        catch
        {
            return Result.Failure<LoginResponse>(AuthErrors.ProblemWhenLoginWithThirdParty(request.LoginProvider));
        }
    }

    private async Task<JObject> SignInWithFacebook(string accessToken)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"https://graph.facebook.com/me?fields=id,first_name,last_name,email,picture&access_token={accessToken}");

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidDataException();
        }

        string content = await response.Content.ReadAsStringAsync();
        var result = JObject.Parse(content);
        return result;
    }

    private async Task<JObject> SignInWithGoogle(string accessToken)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v3/userinfo?access_token={accessToken}");

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidDataException();
        }

        string content = await response.Content.ReadAsStringAsync();
        var result = JObject.Parse(content);

        return result;
    }

    private UserInfo GetUserInfo(JObject authProfile, string loginProvider)
    {
        string firstName, lastName, email, imageUrl;
        switch (loginProvider)
        {
            case LoginProvider.GOOGLE:
                firstName = authProfile["given_name"]!.ToString();
                lastName = authProfile["family_name"]!.ToString();
                email = authProfile["email"]!.ToString();
                imageUrl = authProfile["picture"]!.ToString();
                break;
            case LoginProvider.FACEBOOK:
                firstName = authProfile["first_name"]!.ToString();
                lastName = authProfile["last_name"]!.ToString();
                email = authProfile["email"]!.ToString();
                imageUrl = authProfile["picture"]!["data"]!["url"]!.ToString();
                break;
            default:
                throw new InvalidDataException();
        }
        return new UserInfo(email, firstName, lastName, new Uri(imageUrl));
    }
}
