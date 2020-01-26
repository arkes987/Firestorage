using Firestorage.Noti.Enums;
using System.Threading.Tasks;
using System.Windows;

namespace Firestorage.Noti
{
    public partial class App : Application
    {
        public static void ShowNoti(string text, NotificationType type)
        {
            Window noti = null;
            switch (type)
            {
                case NotificationType.Success:
                    noti = new Views.SuccessView(text);
                    break;
                case NotificationType.Warn:
                    noti = new Views.WarnView(text);
                    break;
                case NotificationType.Error:
                    noti = new Views.ErrorView(text);
                    break;
            }
            QueueNoti(noti);
        }

        private async static void QueueNoti(Window view)
        {
            if (view != null)
            {
                view.Show();
                await Task.Delay(2000);
                view.Hide();
            }
        }
    }
}
