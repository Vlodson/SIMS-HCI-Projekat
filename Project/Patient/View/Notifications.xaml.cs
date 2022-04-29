using Controller;
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
            /*
            foreach (Report report in patientMedicalRecord.Reports)
            {
                foreach(Therapy therapy in report.Therapy)
                {
                    //ovde dodje do terapije
                    //DateTime start = report.CreateDate;
                    DateTime start = new DateTime(report.CreateDate.Year, report.CreateDate.Month, report.CreateDate.Day, 0, 0, 0);
                    DateTime end = report.CreateDate.AddDays(therapy.Duration);
                    int addingHours = 24 / therapy.PerDay; //ovoliko da se dodaje

                    //popuniti liste
                    for(int i = 0; i < therapy.Duration; ++i)
                    {
                       for(int j = 0; j < therapy.PerDay; ++j)
                       {
                            notificationsTimeList.Add(start.AddDays(i).AddHours(j*addingHours));
                            notificationsList.Add("Popiti lek " + therapy.Medicine);
                       }
                    }
                }
            }

            showingNotifications = new List<string>();
            DateTime today = DateTime.Now;
            for(int i = 0; i < notificationsTimeList.Count; ++i)
            {
                if(today.Date == notificationsTimeList[i].Date)
                {
                    ShowingNotifications.Add(notificationsList[i] + " u " + notificationsTimeList[i].ToString("HH:mm"));
                }
            }
            
            NotificationList.ItemsSource = ShowingNotifications;
            */
            NotificationList.ItemsSource = _medicalRecordController.GetNotificationTimes(patientMedicalRecord);
            
        }
    }
}
