using Firestorage.Noti.Enums;
using System.Threading.Tasks;
using System.Windows;

namespace Firestorage.Noti
{
    public partial class App : Application
    {
        public static void ShowNoti(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Success:
                    var noti = new Views.SuccessView();
                    QueueNoti(noti);
                    break;
                case NotificationType.Warn:
                    break;
                case NotificationType.Error:
                    break;
            }
        }

        private async static void QueueNoti(Window view)
        {
            view.Show();
            await Task.Delay(2000);
            view.Hide();
        }
    }
}
