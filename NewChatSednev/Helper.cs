using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewChatSednev
{
    static class Helper
    {
        public static int id_userr { get; set; }

        public static int id_friend { get; set; }

        public static class UserDataTransfer
        { 
            public static UserData SelectedUser { get; set; }
        }
    }
}
