using HospitalMain.Controller;
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

        private PersonalNotificationController _personalNotificationsController;

        private ObservableCollection<HospitalMain.Model.PersonalNotification> personalNotifications;

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
        public MyICommand AddPersonalNotificationCommand { get; set; }
        public MyICommand RemovePersonalNotificationCommand { get; set; }
        public AlarmsViewModel()
        {
            App app = Application.Current as App;
            _personalNotificationsController = app.personalNotificationController;

            AddPersonalNotificationCommand = new MyICommand(OnAddPersonalNotification);
            RemovePersonalNotificationCommand = new MyICommand(OnRemoveCommand, CanRemove);

            PersonalNotifications = new ObservableCollection<HospitalMain.Model.PersonalNotification>();
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
