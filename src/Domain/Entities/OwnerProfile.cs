using System;
using System.Collections.Generic;
using System.Text;
using Domain.Common;
using Domain.Entities;

namespace Domain.Entities;

public class OwnerProfile : AuditableEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public ApplicationUser User { get; set; }
}
