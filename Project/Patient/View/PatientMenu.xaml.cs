using Controller;
using HospitalMain.Model;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for PatientMenu.xaml
    /// </summary>
    public partial class PatientMenu : Page
    {
        private PatientController _patientController;
        private MedicalRecordController _medicalRecordController;
        private DoctorRepo _doctorRepo;
        public PatientMenu()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _medicalRecordController = app.MedicalRecordController;
            _doctorRepo = app.DoctorRepo;
            
            _doctorRepo.SaveDoctor();
            Timer t = new Timer(TimerCallback, null, 0, 60000);
        }

        private static void TimerCallback(Object o)
        {
            //ovde isto kao za obavestenja
            String patientId = Login.loggedId;
            App app = Application.Current as App;
            PatientController patientController = app.PatientController;
            MedicalRecordController medicalRecordController = app.MedicalRecordController;

            Model.Patient patient = patientController.ReadPatient(patientId);
            MedicalRecord patientMedicalRecord = medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);
            
            foreach(Notification notification in medicalRecordController.GetNotificationTimes(patientMedicalRecord))
            {
                if(notification.DateTimeNotification.AddMinutes(10).Minute == DateTime.Now.Minute)
                {
                    //MessageBox.Show(notification.Content);
                }
            }
            
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Window.GetWindow(this).Content = new ExaminationsList();
            Menu.Content = new ExaminationsList();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new Login();
        }

        private void Notifications_Click(object sender, RoutedEventArgs e)
        {
            Notifications notifications = new Notifications();
            notifications.ShowDialog();
        }
    }
}
