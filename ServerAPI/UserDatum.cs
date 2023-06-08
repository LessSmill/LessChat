using System;
using System.Collections.Generic;

namespace ServerAPI;

public partial class UserDatum
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<ContatsUser> ContatsUsers { get; set; } = new List<ContatsUser>();
}
