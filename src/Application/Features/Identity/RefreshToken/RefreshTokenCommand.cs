namespace Petlyx.Application.Features.Identity.RefreshToken;

using Petlyx.Application.Abstractions.Identity;
using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Domain.Common;

public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand<Result<TokenResponse>>;
