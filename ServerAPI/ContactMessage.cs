using System;
using System.Collections.Generic;

namespace ServerAPI;

public partial class ContactMessage
{
    public int Id { get; set; }

    public int? IdContatsUser { get; set; }

    public int? IdMessage { get; set; }

    public virtual ContatsUser? IdContatsUserNavigation { get; set; }

    public virtual Message? IdMessageNavigation { get; set; }
}
