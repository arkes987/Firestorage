using Firestorage.Database;
using Firestorage.Database.Structure;
using Firestorage.Libs;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using Firestorage.Modules.Main;
using System;
using Firestorage.Crypto;
using System.Windows.Controls;

namespace Firestorage.Modules.Entry.Components
{
    public class EntryViewModel : NotifyObject
    {

        private FirebaseConnector _fireBaseConnector;
        private Query _query;

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
            Task.Factory.StartNew(() =>
            {
                var user = _query.GetByEmail<User>(Email).Result;

                if (user.Count > 0)
                {
                    var myUser = user.ElementAt(0);
                    var encryptedPass = Encrypt.EncryptSHA512(((PasswordBox)param).Password);
                    if (myUser.Object.MasterPass == encryptedPass)
                    {
                        Application.Current.Dispatcher.Invoke(delegate
                        {
                            var mainWindow = new MainWindow(myUser.Key);
                            mainWindow.Show();
                            IsVisible = false;
                        });
                    }
                    else
                        MessageBox.Show("Wrong password.");
                }
                else
                    MessageBox.Show("User not found.");

            });
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
            //var usr = new User()
            //{
            //    Email = "arkkin@icloud.com",
            //    LastLogin = DateTime.Now,
            //    MasterPass = Encrypt.EncryptInput(""),
            //    PasswordTryCount = 0,
            //    SaveDate = DateTime.Now
            //};

            //_query.Add(usr);
        }

        bool RegisterCommandCanExecute(object param)
        {
            return true;
        }

        #endregion
    }
}
