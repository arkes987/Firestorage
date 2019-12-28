using System.Windows;

namespace Firestorage.Modules.Main
{
    public partial class MainWindow : Window
    {
        public MainWindow(string userId)
        {
            InitializeComponent();
            DataContext = new MainViewModel(userId);
        }
    }
}
