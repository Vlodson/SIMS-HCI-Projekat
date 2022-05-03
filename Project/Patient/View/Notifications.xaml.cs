using Controller;
using HospitalMain.Model;
using Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : Window
    {
        private PatientController _patientController;
        private MedicalRecordController _medicalRecordController;

        private List<String> notificationsList;
        private List<DateTime> notificationsTimeList;
        private List<String> showingNotifications;


        

        public List<String> ShowingNotifications
        {
            get
            {
                return showingNotifications;
            }
            set
            {
                showingNotifications = value;
            }
        }

        public Notifications()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _medicalRecordController = app.MedicalRecordController;

            String patientId = "2";
            notificationsTimeList = new List<DateTime>();
            notificationsList = new List<String>();

            Model.Patient patient = _patientController.ReadPatient(patientId);
            MedicalRecord patientMedicalRecord = _medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);

            List<String> notifications = new List<String>();
            foreach(Notification notification in _medicalRecordController.GetNotificationTimes(patientMedicalRecord))
            {
                notifications.Add(notification.Content);
            }
            NotificationList.ItemsSource = notifications;
            
        }

        private void MarkAsRead(object sender, RoutedEventArgs e)
        {
            List<Notification> markNotifications = new List<Notification>();
            String patientId = "2";
            Model.Patient patient = _patientController.ReadPatient(patientId);
            MedicalRecord patientMedicalRecord = _medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);

            List<int> selectedItemIndexes = (from object o in NotificationList.SelectedItems select NotificationList.Items.IndexOf(o)).ToList();
            foreach (int index in selectedItemIndexes)
            {

                Notification notification = _medicalRecordController.GetNotificationTimes(patientMedicalRecord)[index];
                _medicalRecordController.EditReadNotification(patientMedicalRecord, notification);
            }
            this.Close();
        }
    }
}
