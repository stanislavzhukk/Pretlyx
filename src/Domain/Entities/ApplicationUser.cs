namespace Domain.Entities;

using Microsoft.AspNetCore.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? RefreshToken { get; set; }
    public DateTimeOffset? RefreshTokenExpiryTime { get; set; }

    public OwnerProfile? ownerProfile { get; set; } = default!;
    //public ICollection<OwnerProfile> OwnerProfiles { get; set; } = new List<OwnerProfile>();
}
