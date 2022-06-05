using Controller;
using HospitalMain.Controller;
using HospitalMain.Model;
using Model;
using Patient.View;
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

    public class NotificationsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private PatientController _patientController;
        private MedicalRecordController _medicalRecordController;
        private NotificationController _notificationController;

        private ObservableCollection<Notification> showingNotifications;
        private Notification selectedNotification;

        public MyICommand RemoveNotificationsCommand { get; set; }

        public ObservableCollection<Notification> ShowingNotifications
        {
            get
            {
                return showingNotifications;
            }
            set
            {
                showingNotifications = value;
                OnPropertyChanged("ShowingNotifications");
            }
        }

        public Notification SelectedNotification
        {
            get
            {
                return selectedNotification;
            }
            set
            {
                selectedNotification = value;
                RemoveNotificationsCommand.RaiseCanExecuteChanged();
            }
        }

        public NotificationsViewModel()
        {
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _medicalRecordController = app.MedicalRecordController;
            _notificationController = app.NotificationController;

            RemoveNotificationsCommand = new MyICommand(OnRemoveNotifications, CanRemoveNotification);

            String patientId = Login.loggedId;
            Model.Patient patient = _patientController.ReadPatient(patientId);
            MedicalRecord patientMedicalRecord = _medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);
            showingNotifications = new ObservableCollection<Notification>();
            //foreach(Notification notification in _medicalRecordController.GetNotificationTimes(patientMedicalRecord))
            //{
            //    showingNotifications.Add(notification.Content);
            //}
            //showingNotifications = _medicalRecordController.GetNotificationTimes(patientMedicalRecord);
            showingNotifications = new ObservableCollection<Notification>(_notificationController.GetPatientNotifications(patientMedicalRecord));
        }

        private bool CanRemoveNotification()
        {
            return SelectedNotification != null;
        }
        private void OnRemoveNotifications()
        {
            String patientId = Login.loggedId;
            Model.Patient patient = _patientController.ReadPatient(patientId);
            MedicalRecord patientMedicalRecord = _medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);

            //showingNotifications.Remove(notification.Content);
            _notificationController.EditReadNotification(patientMedicalRecord, SelectedNotification);
            ShowingNotifications.Remove(SelectedNotification);
            
        }
    }
}
