namespace Petlyx.Application.Features.Identity.Register;

using Petlyx.Application.Abstractions.Messaging;
using Petlyx.Domain.Common;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword) : ICommand;
