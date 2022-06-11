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

            Notification not1 = new Notification("Od sledeće nedelje, na radno mesto dolazi novi upravnik bolnice, Dr. Kopitović.", false, new DateTime(2022, 6, 7, 12, 30, 00));
            Notification not2 = new Notification("Na današnji dan, proslavljamo 15. rođendan naše bolnice. Povodom ovog događaja, upravnik Lunić je organizovao u svečanoj sali bolnice proslavu. Na prethodno spomenutom događaju, gostovaće nam proslavljena estradna zvezda Đani!", false, new DateTime(2022, 6, 7, 14, 30, 00));
            Notification not3 = new Notification("Danas, u jutarnji časovima, desio se kvar na instalacijama u ostavi na 2. spratu. Molim vas, pozovite nadležne da otklone problem.", false, new DateTime(2022, 6, 8, 8, 00, 00));
            Notification not4 = new Notification("Renoviranje sobe broj 8 će se održati u periodu od 15.8.2022. do 29.8.2022. Molimo sve zaposlene za strpljenje.", false, new DateTime(2022, 6, 9, 11, 30, 00));

            Notifications.Add(new NotificationDTO(not1, "Novi upravnik"));
            Notifications.Add(new NotificationDTO(not2, "Rođendan bolnice"));
            Notifications.Add(new NotificationDTO(not3, "Kvar na instalacijama"));
            Notifications.Add(new NotificationDTO(not4, "Renoviranje sobe 8"));

            ReadNotificationCommand = new ReadNotificationCommand(mainViewModel, this);
        }
    }
}
