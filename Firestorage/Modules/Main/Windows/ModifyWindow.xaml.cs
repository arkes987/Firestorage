using Firebase.Database;
using Firestorage.Crypto;
using Firestorage.Database;
using Firestorage.Database.Structure;
using System.Windows;

namespace Firestorage.Modules.Main.Windows
{
    public partial class ModifyWindow : Window
    {
        public ModifyWindow(Query query, FirebaseObject<SimpleAccount> account, string userId, ProtectDataEngine protectDataEngine)
        {
            InitializeComponent();
            DataContext = new ModifyViewModel(query, account, userId, protectDataEngine);
        }
    }
}
