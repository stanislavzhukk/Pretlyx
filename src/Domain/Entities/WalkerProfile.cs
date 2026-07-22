using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;

namespace Domain.Entities;

public class WalkerProfile : AuditableEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;

    public decimal? PricePerHour { get; set; }
    public int? ExperienceYears { get; set;  }
    public float? Rating { get; set; }
    public string? Location { get; set; }
    //potentially postgis
    public string? Email { get; set; } = default!;
    public string? PhoneNumber { get; set; } = default!;
    public ApplicationUser User { get; set; }
}
