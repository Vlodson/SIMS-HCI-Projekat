using Controller;
using HospitalMain.Controller;
using HospitalMain.Model;
using Model;
using Patient.View;
using Patient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patient.ViewModel
{
    public class AlarmsViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private HospitalMain.Model.PersonalNotification selectedNotification;
        private bool isChecked;

        private PersonalNotificationController _personalNotificationsController;
        private MedicalRecordController _medicalRecordController;

        private ObservableCollection<HospitalMain.Model.PersonalNotification> personalNotifications;
        private ObservableCollection<Notification> notifications;

        public ObservableCollection<HospitalMain.Model.PersonalNotification> PersonalNotifications
        {
            get
            {
                return personalNotifications;
            }
            set
            {
                personalNotifications = value;
                OnPropertyChanged("PersonalNotifications");
            }
        }

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
        public HospitalMain.Model.PersonalNotification SelectedNotification
        {
            get
            {
                return selectedNotification;
            }
            set
            {
                selectedNotification = value;
                OnPropertyChanged("SelectedNotification");
                RemovePersonalNotificationCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
                _personalNotificationsController.ChangeNotificationStatus(SelectedNotification);
                
            }
        }

        public MyICommand AddPersonalNotificationCommand { get; set; }
        public MyICommand RemovePersonalNotificationCommand { get; set; }
        public AlarmsViewModel()
        {
            App app = Application.Current as App;
            _personalNotificationsController = app.personalNotificationController;
            _medicalRecordController = app.MedicalRecordController;

            AddPersonalNotificationCommand = new MyICommand(OnAddPersonalNotification);
            RemovePersonalNotificationCommand = new MyICommand(OnRemoveCommand, CanRemove);

            PersonalNotifications = new ObservableCollection<HospitalMain.Model.PersonalNotification>();
            MedicalRecord medicalRecord = _medicalRecordController.GetMedicalRecord(Login.loggedId);
            Notifications = new ObservableCollection<Notification>(_medicalRecordController.GetNotificationTimes(medicalRecord));
            foreach(Notification notification in Notifications)
            {
                if(notification.DateTimeNotification < DateTime.Now)
                {
                    notification.ContentTable = notification.Content.Split("u")[0];
                    notification.DateTimeNotificationTable = notification.DateTimeNotification.ToString("dd.MM.yyyy. HH:mm");
                }
                else
                {
                    Notifications.Remove(notification);
                }
            }
            foreach(HospitalMain.Model.PersonalNotification personalNotification in _personalNotificationsController.GetPatientPersonalNotifications(Login.loggedId))
            {
                String days = "";
                foreach(int day in personalNotification.Days)
                {
                    switch (day)
                    {
                        case 1:
                            days += "pon ";
                            break;
                        case 2:
                            days += "uto ";
                            break;
                        case 3:
                            days += "sre ";
                            break;
                        case 4:
                            days += "čet ";
                            break;
                        case 5:
                            days += "pet ";
                            break;
                        case 6:
                            days += "sub ";
                            break;
                        case 0:
                            days += "ned ";
                            break;
                    }
                }
                personalNotification.DaysString = days;
                personalNotification.TimeString = personalNotification.Time.ToString("HH:mm");
                PersonalNotifications.Add(personalNotification);
            }
        }

        private void OnAddPersonalNotification()
        {
            NEwAlarm newAlarm = new NEwAlarm();
            newAlarm.ShowDialog();
            PersonalNotifications = new ObservableCollection<HospitalMain.Model.PersonalNotification>();
            foreach (HospitalMain.Model.PersonalNotification personalNotification in _personalNotificationsController.GetPatientPersonalNotifications(Login.loggedId))
            {
                String days = "";
                foreach (int day in personalNotification.Days)
                {
                    switch (day)
                    {
                        case 1:
                            days += "pon ";
                            break;
                        case 2:
                            days += "uto ";
                            break;
                        case 3:
                            days += "sre ";
                            break;
                        case 4:
                            days += "čet ";
                            break;
                        case 5:
                            days += "pet ";
                            break;
                        case 6:
                            days += "sub ";
                            break;
                        case 7:
                            days += "ned ";
                            break;
                    }
                }
                personalNotification.DaysString = days;
                PersonalNotifications.Add(personalNotification);
            }
        }

        public bool CanRemove()
        {
            return SelectedNotification != null;
        }

        private void OnRemoveCommand()
        {
            _personalNotificationsController.DeletePersonalNotification(SelectedNotification);
            PersonalNotifications = new ObservableCollection<HospitalMain.Model.PersonalNotification>();
            foreach (HospitalMain.Model.PersonalNotification personalNotification in _personalNotificationsController.GetPatientPersonalNotifications(Login.loggedId))
            {
                String days = "";
                foreach (int day in personalNotification.Days)
                {
                    switch (day)
                    {
                        case 1:
                            days += "pon ";
                            break;
                        case 2:
                            days += "uto ";
                            break;
                        case 3:
                            days += "sre ";
                            break;
                        case 4:
                            days += "čet ";
                            break;
                        case 5:
                            days += "pet ";
                            break;
                        case 6:
                            days += "sub ";
                            break;
                        case 7:
                            days += "ned ";
                            break;
                    }
                }
                personalNotification.DaysString = days;
                PersonalNotifications.Add(personalNotification);
            }
        }

    }
}
