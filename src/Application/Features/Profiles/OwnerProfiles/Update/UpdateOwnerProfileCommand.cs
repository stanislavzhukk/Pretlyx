using Application.Abstractions.Messaging;
using Domain.Common;

namespace Application.Features.Profiles.OwnerProfiles.Update;

public sealed record UpdateOwnerProfileCommand(string? Name, string? PhoneNumber) : ICommand<Result<UpdateOwnerProfileResponse>>;

public sealed record UpdateOwnerProfileResponse(Guid Id, string Name, string? PhoneNumber);
