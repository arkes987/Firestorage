using Firestorage.Enums;
using System;
using System.Windows.Input;

namespace Firestorage.Database.Structure
{
    public class Account
    {
        public string Name { get; set; }
        public string OwnerUserId { get; set; }
        public AccountType Type { get; set; }
        public DateTime SaveDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ModifyCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Content { get; set; }
    }
}
