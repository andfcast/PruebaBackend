using System;
using System.Collections.Generic;

namespace WebApiEntities.Models;

public partial class User
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string? Avatar { get; set; }
}
