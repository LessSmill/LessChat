using System;
using System.Collections.Generic;

namespace ServerAPI;

public partial class ContatsUser
{
    public int? IdUser { get; set; }

    public int? IdFriend { get; set; }

    public int Id { get; set; }

    public virtual ICollection<ContactMessage> ContactMessages { get; set; } = new List<ContactMessage>();

    public virtual UserDatum? IdUserNavigation { get; set; }
}
