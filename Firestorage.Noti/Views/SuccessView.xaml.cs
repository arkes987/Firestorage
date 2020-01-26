using System.Windows;

namespace Firestorage.Noti.Views
{
    public partial class SuccessView : Window
    {
        public SuccessView(string text)
        {
            InitializeComponent();
            DataContext = new NotificationViewModel(text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            Left = desktopWorkingArea.Right - (Width + 10);
            Top = desktopWorkingArea.Bottom - (Height + 10);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
