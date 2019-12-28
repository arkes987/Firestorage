using Firebase.Database;
using Firestorage.Database;
using Firestorage.Database.Structure;
using System.Windows;

namespace Firestorage.Modules.Main.Windows
{
    public partial class ModifyWindow : Window
    {
        public ModifyWindow(Query query, FirebaseObject<SimpleAccount> account, string userId)
        {
            InitializeComponent();
            DataContext = new ModifyViewModel(query, account, userId);
        }
    }
}
