using Firebase.Database;
using Firebase.Database.Streaming;
using Firestorage.Database;
using Firestorage.Database.Structure;
using Firestorage.Modules.Main.Windows;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using Firestorage.Crypto;
using System.Threading;
using Firestorage.Enums;
using Firestorage.Core;

namespace Firestorage.Modules.Main
{
    public class MainViewModel : NotifyObject
    {
        private FirebaseConnector _fireBaseConnector;
        private ProtectDataEngine _protectDataEngine;
        private Query _query;
        private string _userId;

        public AccountTypeView AccountTypeView { get; set; } = AccountTypeView.All;

        private int _formWidth = 800;
        public int FormWidth
        {
            get => _formWidth;
            set
            {
                if (_formWidth == value) return;
                _formWidth = value;
                OnPropertyChanged(() => FormWidth);
            }
        }

        private int _formHeight = 450;
        public int FormHeight
        {
            get => _formHeight;
            set
            {
                if (_formHeight == value) return;
                _formHeight = value;
                OnPropertyChanged(() => FormHeight);
            }
        }

        private ObservableCollection<FirebaseObject<Account>> _accounts;
        public ObservableCollection<FirebaseObject<Account>> Accounts
        {
            get => _accounts;
            set
            {
                if (_accounts == value) return;
                _accounts = value;
                OnPropertyChanged(() => Accounts);
            }
        }

        public MainViewModel(string userId)
        {
            Accounts = new ObservableCollection<FirebaseObject<Account>>();
            _fireBaseConnector = new FirebaseConnector();
            _query = new Query(_fireBaseConnector);
            _userId = userId;
            _protectDataEngine = new ProtectDataEngine(_userId);
            _query.ObserveCollection<Account>(callback => FetchAccountFromServer(callback), _userId);
        }

        public void FetchAccountFromServer(FirebaseEvent<Account> recivedEvent)
        {
            if (recivedEvent.Object == null)
                return;

            if (recivedEvent.EventType == FirebaseEventType.Delete)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (var account in Accounts)
                    {
                        if (account.Key == recivedEvent.Key)
                        {
                            Accounts.Remove(account);
                            break;
                        }
                    }
                });
            }
            else if (recivedEvent.EventType == FirebaseEventType.InsertOrUpdate)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    recivedEvent.Object.DeleteCommand = DeleteCommand;
                    recivedEvent.Object.ModifyCommand = ModifyCommand;
                    recivedEvent.Object.CopyCommand = CopyCommand;

                    var exItem = Accounts.FirstOrDefault(x => x.Key == recivedEvent.Key);

                    if (exItem != null)
                        exItem = recivedEvent;
                    else
                        Accounts.Add(recivedEvent);
                });
            }

            Accounts = new ObservableCollection<FirebaseObject<Account>>(Accounts.OrderBy(acc => acc.Object.Type));
        }

        #region Add

        RelayCommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(AddCommandExecute, AddCommandCanExecute));

        void AddCommandExecute(object param)
        {
            var modifyWindow = new ModifyWindow(_query, null, null, _userId, _protectDataEngine);
            modifyWindow.Show();
        }

        bool AddCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region Delete

        RelayCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteCommandExecute, DeleteCommandCanExecute));

        void DeleteCommandExecute(object param)
        {
            if (MessageBox.Show("Sure to delete?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Task.Factory.StartNew(() =>
                {
                    var key = (string)param;
                    _query.DeleteByKey<Account>(key);
                });
            }
        }

        bool DeleteCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region Modify

        RelayCommand _modifyCommand;
        public ICommand ModifyCommand => _modifyCommand ?? (_modifyCommand = new RelayCommand(ModifyCommandExecute, ModifyCommandCanExecute));

        void ModifyCommandExecute(object param)
        {
            var obj = (FirebaseObject<Account>)param;
            var modifyWindow = new ModifyWindow(_query, obj.Key, obj.Object, _userId, _protectDataEngine);
            modifyWindow.Show();
        }

        bool ModifyCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region Copy

        RelayCommand _copyCommand;
        public ICommand CopyCommand => _copyCommand ?? (_copyCommand = new RelayCommand(CopyCommandExecute, CopyCommandCanExecute));

        void CopyCommandExecute(object param)
        {            
            var acc = (Account)param;

            switch (acc.Type)
            {
                case AccountType.Simple:
                    Clipboard.SetText(acc.Login);
                    Thread.Sleep(1000);
                    Clipboard.SetText(_protectDataEngine.Decrypt(acc.Password));
                    break;
                case AccountType.Password:
                    Clipboard.SetText(_protectDataEngine.Decrypt(acc.Password));
                    break;
                case AccountType.Config:
                    Clipboard.SetText(acc.Content);
                    break;
                case AccountType.Note:
                    Clipboard.SetText(acc.Content);
                    break;
            }
            Noti.App.ShowNoti("Successfully copied item", Noti.Enums.NotificationType.Success);
        }

        bool CopyCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region Lock

        RelayCommand _lockCommand;
        public ICommand LockCommand => _lockCommand ?? (_lockCommand = new RelayCommand(LockCommandExecute, LockCommandCanExecute));

        void LockCommandExecute(object param)
        {

        }

        bool LockCommandCanExecute(object param)
        {
            return true;
        }

        #endregion
    }
}
