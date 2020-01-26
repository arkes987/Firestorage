using Firestorage.Database;
using Firestorage.Database.Structure;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using Firestorage.Modules.Main;
using System;
using Firestorage.Crypto;
using System.Windows.Controls;
using Firestorage.Enums;
using Firestorage.Core;

namespace Firestorage.Modules.Entry.Components
{
    public class EntryViewModel : NotifyObject
    {

        private FirebaseConnector _fireBaseConnector;
        private Query _query;

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

        public string Password { get; set; }


        public EntryViewModel()
        {
            _fireBaseConnector = new FirebaseConnector();
            _query = new Query(_fireBaseConnector);
        }

        #region Login

        RelayCommand _loginCommand;
        public ICommand LoginCommand => _loginCommand ?? (_loginCommand = new RelayCommand(LoginCommandExecute, LoginCommandCanExecute));

        void LoginCommandExecute(object param)
        {
            var encryptedPass = Encrypt.EncryptSHA512(((PasswordBox)param).Password);
            TryAuthenticateUser(encryptedPass);
        }

        bool LoginCommandCanExecute(object param)
        {
            return true;
        }

        #endregion

        #region Register

        RelayCommand _registerCommand;
        public ICommand RegisterCommand => _registerCommand ?? (_registerCommand = new RelayCommand(RegisterCommandExecute, RegisterCommandCanExecute));

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
        public ICommand RegisterConfirmCommand => _registerConfirmCommand ?? (_registerConfirmCommand = new RelayCommand(RegisterConfirmCommandExecute, RegisterConfirmCommandCanExecute));

        void RegisterConfirmCommandExecute(object param)
        {
            if (string.IsNullOrEmpty(((PasswordBox)param).Password))
            {
                MessageBox.Show("Plase type correct password.");
                return;
            }

            var encryptedPass = Encrypt.EncryptSHA512(((PasswordBox)param).Password);
            var usr = new User()
            {
                Email = Email,
                LastLogin = DateTime.Now,
                MasterPass = encryptedPass,
                PasswordTryCount = 0,
                SaveDate = DateTime.Now
            };

            _query.Add(usr);

            TryAuthenticateUser(encryptedPass);
        }

        bool RegisterConfirmCommandCanExecute(object param)
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Name);
        }

        #endregion

        private void TryAuthenticateUser(string encryptedPswd)
        {
            Task.Factory.StartNew(() =>
            {
                var user = _query.GetByEmail<User>(Email).Result;

                if (user.Count > 0)
                {
                    var myUser = user.ElementAt(0);
                    if (myUser.Object.MasterPass == encryptedPswd)
                    {
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            var mainWindow = new MainWindow(myUser.Key);
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
                else
                {
                    MessageBox.Show("User not found.");
                }
            });
        }
    }
}
