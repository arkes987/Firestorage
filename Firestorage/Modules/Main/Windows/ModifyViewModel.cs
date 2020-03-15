using Firestorage.Core;
using Firestorage.Crypto;
using Firestorage.Database;
using Firestorage.Database.Structure;
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
        public Account Account { get; set; }
        public ModifyViewModel(Query query, string key, Account account, string userId, ProtectDataEngine protectDataEngine)
        {
            if (account != null)
            {
                Account = account;
                _key = key;
            }
            else
            {
                _key = null;
                Account = new Account();
            }
            _userId = userId;
            _query = query;
            _protectDataEngine = protectDataEngine;
        }

        #region Save

        RelayCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new RelayCommand(SaveCommandExecute, SaveCommandCanExecute);

        void SaveCommandExecute(object param)
        {
            var acc = Account;
            acc.Password = _protectDataEngine.Encrypt(((PasswordBox)param).Password);
            if (!string.IsNullOrEmpty(_key))
            {
                acc.ModifyDate = DateTime.Now;
                _query.Update(_key, acc);
            }
            else
            {
                acc.ModifyDate = DateTime.Now;
                acc.SaveDate = DateTime.Now;
                acc.OwnerUserId = _userId;
                _query.Add(acc);
            }
        }

        bool SaveCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region Generate

        RelayCommand _generateCommand;
        public ICommand GenerateCommand => _generateCommand ??= new RelayCommand(GenerateCommandExecute, GenerateCommandCanExecute);

        void GenerateCommandExecute(object param)
        {
            string pswd = string.Empty;
            var passwordGenerator = new PasswordGenerator(ref pswd);
            passwordGenerator.Show();
        }

        bool GenerateCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

    }
}
