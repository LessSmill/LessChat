using System;
using System.Collections.Generic;

namespace ServerAPI;

public partial class Message
{
    public int Id { get; set; }

    public string? Text { get; set; }

    public int? IdSender { get; set; }

    public int? IdContacts { get; set; }

    public virtual ICollection<ContactMessage> ContactMessages { get; set; } = new List<ContactMessage>();
}
