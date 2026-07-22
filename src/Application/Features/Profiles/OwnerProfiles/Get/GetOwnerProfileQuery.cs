using Application.Abstractions.Messaging;
using Domain.Common;

namespace Application.Features.Profiles.OwnerProfiles.Get;

public sealed record GetOwnerProfileQuery() : IQuery<Result<OwnerProfileResponse>>;
public sealed record OwnerProfileResponse(Guid Id, DateTimeOffset CreatedAt, string? Email, string? PhoneNumber);
