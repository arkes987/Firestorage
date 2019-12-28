using System;
using System.Collections.Generic;
using System.Text;

namespace Firestorage.Database.Structure
{
    public class User
    {
        public string Email { get; set; }
        public string MasterPass { get; set; }
        public int PasswordTryCount { get; set; }
        public DateTime LastLogin { get; set; }
        public DateTime SaveDate { get; set; }
    }
}
