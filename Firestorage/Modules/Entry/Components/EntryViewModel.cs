using Firestorage.Core;
using Firestorage.Database;
using Firestorage.Enums;
using Firestorage.Modules.Main;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Firestorage.Modules.Entry.Components
{
    public class EntryViewModel : NotifyObject
    {
        private Query _query;
        private readonly FirebaseConnector _fireBaseConnector;

        private EntryViewType _entryViewType = EntryViewType.Login;
        public EntryViewType EntryViewType
        {
            get => _entryViewType;
            set
            {
                if (_entryViewType == value) return;
                _entryViewType = value;
                OnPropertyChanged(() => EntryViewType);
            }
        }


        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value) return;
                _isVisible = value;
                OnPropertyChanged(() => IsVisible);
            }
        }

        public string Name { get; set; }

        public string Email { get; set; } = "arkkin@icloud.com";



        public EntryViewModel()
        {
            _fireBaseConnector = new FirebaseConnector();
        }

        #region Login

        RelayCommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ??= new RelayCommand(LoginCommandExecute, LoginCommandCanExecute);

        async void LoginCommandExecute(object param)
        {
            await TryAuthenticateUser(((PasswordBox)param).Password);
        }

        bool LoginCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region Register

        RelayCommand _registerCommand;
        public ICommand RegisterCommand => _registerCommand ??= new RelayCommand(RegisterCommandExecute, RegisterCommandCanExecute);

        void RegisterCommandExecute(object param)
        {
            EntryViewType = EntryViewType.Register;
        }

        bool RegisterCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region RegisterConfirm

        RelayCommand _registerConfirmCommand;
        public ICommand RegisterConfirmCommand => _registerConfirmCommand ??= new RelayCommand(RegisterConfirmCommandExecute, RegisterConfirmCommandCanExecute);

        async void RegisterConfirmCommandExecute(object param)
        {
            if (string.IsNullOrEmpty(((PasswordBox)param).Password))
            {
                MessageBox.Show("Plase type correct password.");
                return;
            }

            await TryRegisterUser(((PasswordBox)param).Password);

        }

        bool RegisterConfirmCommandCanExecute(object param)
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Name);
        }

        #endregion

        private async Task TryAuthenticateUser(string password)
        {
            if (_fireBaseConnector != null)
            {
                var authResult = await _fireBaseConnector?.Authenticate(Email, password);

                if (authResult != null)
                {
                    _query = new Query(_fireBaseConnector);

                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        var mainWindow = new MainWindow(authResult, _fireBaseConnector);
                        mainWindow.Show();
                        IsVisible = false;
                        EntryViewType = EntryViewType.Login;
                    });
                }
                else
                {
                    MessageBox.Show("Wrong password.");
                }
            }
        }

        private async Task TryRegisterUser(string password)
        {
            if (_fireBaseConnector != null)
            {
                var authResult = await _fireBaseConnector?.Register(Email, password);

                if (authResult != null)
                {
                    _query = new Query(_fireBaseConnector);

                    Application.Current.Dispatcher.Invoke(delegate
                    {
                        var mainWindow = new MainWindow(authResult, _fireBaseConnector);
                        mainWindow.Show();
                        IsVisible = false;
                        EntryViewType = EntryViewType.Login;
                    });
                }
                else
                {
                    MessageBox.Show("Wrong password.");
                }
            }
        }
    }
}
