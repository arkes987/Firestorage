using System.Windows;

namespace Firestorage.Noti.Views
{
    public partial class WarnView : Window
    {
        public WarnView(string text)
        {
            InitializeComponent();
            DataContext = new NotificationViewModel(text);
        }
    }
}
