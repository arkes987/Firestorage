using Firebase.Database;
using Firestorage.Crypto;
using Firestorage.Database;
using Firestorage.Database.Structure;
using Firestorage.Libs;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Firestorage.Modules.Main.Windows
{
    public class ModifyViewModel : NotifyObject
    {
        private Query _query;
        private readonly string _key;
        private readonly string _userId;
        private ProtectDataEngine _protectDataEngine;
        public SimpleAccount Account { get; set; }
        public ModifyViewModel(Query query, FirebaseObject<SimpleAccount> account, string userId, ProtectDataEngine protectDataEngine)
        {
            if (account != null)
            {
                Account = account.Object;
                _key = account.Key;
            }
            else
            {
                _key = null;
                Account = new SimpleAccount();
            }
            _userId = userId;
            _query = query;
            _protectDataEngine = protectDataEngine;
        }

        #region Save

        RelayCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute));

        void SaveCommandExecute(object param)
        {
            Account.Password = _protectDataEngine.Encrypt(((PasswordBox)param).Password);
            if (!string.IsNullOrEmpty(_key))
            {
                Account.ModifyDate = DateTime.Now;
                _query.Update(_key, Account);
            }
            else
            {
                Account.Type = 0;
                Account.ModifyDate = DateTime.Now;
                Account.SaveDate = DateTime.Now;
                Account.OwnerUserId = _userId;
                _query.Add(Account);
            }
        }

        bool SaveCommandCanExecute(object param)
        {
            return true;
        }

        #endregion
    }
}
