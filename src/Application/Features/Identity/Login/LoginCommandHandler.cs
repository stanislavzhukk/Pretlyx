namespace Petlyx.Application.Features.Identity.Login;

using Petlyx.Application.Abstractions.Identity;
using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Domain.Common;
using Petlyx.Domain.Entities;
using Microsoft.AspNetCore.Identity;

public sealed class LoginCommandHandler(
    UserManager<ApplicationUser> userManager,
    ITokenService tokenService) : ICommandHandler<LoginCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> HandleAsync(LoginCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user is null)
            return Result.Failure<TokenResponse>(Error.Validation("Auth.InvalidCredentials", "Invalid email or password."));

        var isValidPassword = await userManager.CheckPasswordAsync(user, command.Password);
        if (!isValidPassword)
            return Result.Failure<TokenResponse>(Error.Validation("Auth.InvalidCredentials", "Invalid email or password."));

        var token = await tokenService.GenerateTokenAsync(user, cancellationToken);
        return Result.Success(token);
    }
}
