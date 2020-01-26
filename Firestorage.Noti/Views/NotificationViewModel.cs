using Firestorage.Core;

namespace Firestorage.Noti.Views
{
    public class NotificationViewModel : NotifyObject
    {
		public NotificationViewModel(string text)
		{
			_notificationText = text;
		}

		private string _notificationText;
		public string NotificationText
		{
			get => _notificationText;
			set
			{
				if (_notificationText == value) return;
				_notificationText = value;
				OnPropertyChanged(() => NotificationText);
			}
		}

	}
}
