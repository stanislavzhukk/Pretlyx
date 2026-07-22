namespace Application.Features.Identity.RefreshToken;

using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Common;

public sealed record RefreshTokenCommand(string AccessToken, string RefreshToken) : ICommand<Result<TokenResponse>>;
