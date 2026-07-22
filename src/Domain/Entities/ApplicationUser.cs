namespace Domain.Entities;

using Microsoft.AspNetCore.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }

    public OwnerProfile? OwnerProfile { get; set; } = default!;
    public WalkerProfile? WalkerProfile { get; set; } = default!;
}
