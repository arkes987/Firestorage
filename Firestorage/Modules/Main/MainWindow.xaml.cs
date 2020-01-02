using System;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
