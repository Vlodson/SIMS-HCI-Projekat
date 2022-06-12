using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class NotificationDTO : ViewModelBase
    {
        private Notification _notification;

        public String Content
        {
            get { return _notification.Content; }
            set { _notification.Content = value; OnPropertyChanged(nameof(Content)); }
        }
        public DateTime Date => _notification.DateTimeNotification;

        private String _topic;
        public String Topic
        {
            get => _topic;
            set { _topic = value; OnPropertyChanged(nameof(Topic)); }
        }

        public NotificationDTO(Notification notification, String topic)
        {
            _notification = notification;
            _topic = topic;
        }
    }
}
