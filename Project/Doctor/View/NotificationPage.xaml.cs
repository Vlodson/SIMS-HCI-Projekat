using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for NotificationPage.xaml
    /// </summary>
    public partial class NotificationPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private ObservableCollection<Notification> notifications;
        public ObservableCollection<Notification> Notifications
        {
            get
            {
                return notifications;
            }
            set
            {
                notifications = value;
                OnPropertyChanged("Notifications");
            }
        }
        public NotificationPage()
        {
            InitializeComponent();
            this.DataContext = this;
            notifications = new ObservableCollection<Notification>();
            Notification notification1 = new Notification("Renoviranje prostorije 4 ce trajati od 22.6. do 13.07.2022. godine. U tom periodu prostorija nece biti u funkciji.", false, DateTime.Now);
            Notification notification2 = new Notification("Danas je rodjendan nasem kolegi Miki Mikicu. Podsecamo te da mu danas pozelis srecan rodjendan!", false, DateTime.Now);
            Notification notification3 = new Notification("Danas u 12:00 u bolnicu su pristigli novi lekovi.", false, DateTime.Now);
            Notifications.Add(notification2);
            Notifications.Add(notification1);
            Notifications.Add(notification3);
        }
    }
}
