using HospitalMain.Model;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class NotificationViewModel : ViewModelBase
    {
        private ObservableCollection<NotificationDTO> _notifications;
        public ObservableCollection<NotificationDTO> Notifications
        {
            get { return _notifications; }
            set { _notifications = value; OnPropertyChanged(nameof(Notifications)); }
        }

        private NotificationDTO _selectedNotification;
        public NotificationDTO SelectedNotification
        {
            get { return _selectedNotification; }
            set { _selectedNotification = value; OnPropertyChanged(nameof(SelectedNotification)); }
        }

        private String _topic;
        public String Topic
        {
            get { return _topic; }
            set { _topic = value; OnPropertyChanged(nameof(Topic)); }
        }

        public ICommand ReadNotificationCommand { get; }

        public NotificationViewModel(MainViewModel mainViewModel)
        {

            Notifications = new ObservableCollection<NotificationDTO>();

            Notification not1 = new Notification("Content1", false, new DateTime(2022, 6, 7, 12, 30, 00));
            Notification not2 = new Notification("Content2", false, new DateTime(2022, 6, 7, 14, 30, 00));
            Notification not3 = new Notification("Content3", false, new DateTime(2022, 6, 8, 8, 00, 00));
            Notification not4 = new Notification("Content4", false, new DateTime(2022, 6, 9, 11, 30, 00));

            Notifications.Add(new NotificationDTO(not1, "Topic1"));
            Notifications.Add(new NotificationDTO(not2, "Topic2"));
            Notifications.Add(new NotificationDTO(not3, "Topic3"));
            Notifications.Add(new NotificationDTO(not4, "Topic4"));

            ReadNotificationCommand = new ReadNotificationCommand(mainViewModel, this);
        }
    }
}
