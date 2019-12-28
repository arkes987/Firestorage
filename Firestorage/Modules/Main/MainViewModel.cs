using Firebase.Database;
using Firebase.Database.Streaming;
using Firestorage.Database;
using Firestorage.Database.Structure;
using Firestorage.Libs;
using Firestorage.Modules.Main.Windows;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using Firestorage.Crypto;

namespace Firestorage.Modules.Main
{
    public class MainViewModel : NotifyObject
    {
        private FirebaseConnector _fireBaseConnector;
        private ProtectDataEngine _protectDataEngine;
        private Query _query;
        private string _userId;

        private ObservableCollection<FirebaseObject<SimpleAccount>> _accounts;
        public ObservableCollection<FirebaseObject<SimpleAccount>> Accounts
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
            Accounts = new ObservableCollection<FirebaseObject<SimpleAccount>>();
            _fireBaseConnector = new FirebaseConnector();
            _query = new Query(_fireBaseConnector);
            _userId = userId;
            _protectDataEngine = new ProtectDataEngine(_userId);
            _query.ObserveCollection<SimpleAccount>(FetchAccountFromServer, _userId);
        }

        public void CreateSampleAcc()
        {
            Task.Factory.StartNew(() =>
            {
                //var acc = new SimpleAccount()
                //{
                //    OwnerUserId = _userId,
                //    Login = "arkadiusz.kina@smarterpod.eu",
                //    Password = "asFFGEE!$24",
                //    ModifyDate = DateTime.Now,
                //    SaveDate = DateTime.Now,
                //    Type = 0
                //};
                //_query.Add<Account>(acc);
            });
        }

        public void FetchAccountFromServer(FirebaseEvent<SimpleAccount> recivedEvent)
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
        }

        #region Add

        RelayCommand _addCommand;
        public ICommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(AddCommandExecute, AddCommandCanExecute));

        void AddCommandExecute(object param)
        {
            var modifyWindow = new ModifyWindow(_query, null, _userId, _protectDataEngine);
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
            Task.Factory.StartNew(() =>
            {
                var key = (string)param;
                _query.DeleteByKey<SimpleAccount>(key);
            });
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
            var obj = (FirebaseObject<SimpleAccount>)param;
            var modifyWindow = new ModifyWindow(_query, obj, _userId, _protectDataEngine);
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
            var obj = (SimpleAccount)param;
            Clipboard.SetText(_protectDataEngine.Decrypt(obj.Password));
        }

        bool CopyCommandCanExecute(object param)
        {
            return true;
        }

        #endregion
    }
}
