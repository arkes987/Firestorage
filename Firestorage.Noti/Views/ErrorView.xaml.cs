using System.Windows;

namespace Firestorage.Noti.Views
{
    public partial class ErrorView : Window
    {
        public ErrorView(string text)
        {
            InitializeComponent();
            DataContext = new NotificationViewModel(text);
        }
    }
}
