using Firebase.Database;
using Firestorage.Database;
using Firestorage.Database.Structure;
using Firestorage.Libs;
using System.Windows.Input;

namespace Firestorage.Modules.Main.Windows
{
    public class ModifyViewModel : NotifyObject
    {
        private Query _query;
        private readonly string _key;
        private readonly string _userId;
        public SimpleAccount Account { get; set; }
        public ModifyViewModel(Query query, FirebaseObject<SimpleAccount> account, string userId)
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

        }

        #region Save

        RelayCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(SaveCommandExecute, SaveCommandCanExecute));

        void SaveCommandExecute(object param)
        {

            if (!string.IsNullOrEmpty(_key))
            {
                _query.Update(_key, Account);
            }
            else
            {
                Account.Type = 0;
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
