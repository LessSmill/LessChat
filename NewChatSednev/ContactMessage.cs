//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewChatSednev
{
    using System;
    using System.Collections.Generic;
    
    public partial class ContactMessage
    {
        public int Id { get; set; }
        public Nullable<int> IdContatsUser { get; set; }
        public Nullable<int> IdMessage { get; set; }
    
        public virtual ContatsUser ContatsUser { get; set; }
        public virtual Message Message { get; set; }
    }
}
