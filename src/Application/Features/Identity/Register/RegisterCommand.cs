namespace Application.Features.Identity.Register;

using Application.Abstractions.Messaging;
using Domain.Common;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword) : ICommand;
