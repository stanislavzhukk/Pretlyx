namespace Application.Features.Identity.Login;

using Application.Abstractions.Identity;
using Application.Abstractions.Messaging;
using Domain.Common;

public sealed record LoginCommand(string Email, string Password) : ICommand<Result<TokenResponse>>;
