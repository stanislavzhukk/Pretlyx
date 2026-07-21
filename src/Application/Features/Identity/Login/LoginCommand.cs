namespace Petlyx.Application.Features.Identity.Login;

using Petlyx.Application.Abstractions.Identity;
using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Domain.Common;

public sealed record LoginCommand(string Email, string Password) : ICommand<Result<TokenResponse>>;
