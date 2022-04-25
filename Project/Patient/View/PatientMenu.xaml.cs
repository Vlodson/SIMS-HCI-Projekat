using Controller;
using Model;
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
        public PatientMenu()
        {
            InitializeComponent();
            App app = Application.Current as App;
            _patientController = app.PatientController;
            _medicalRecordController = app.MedicalRecordController;

            Timer t = new Timer(TimerCallback, null, 0, 1000);
        }

        private static void TimerCallback(Object o)
        {
            //ovde isto kao za obavestenja
            String patientId = "2";
            App app = Application.Current as App;
            PatientController patientController = app.PatientController;
            MedicalRecordController medicalRecordController = app.MedicalRecordController;

            Model.Patient patient = patientController.ReadPatient(patientId);
            MedicalRecord patientMedicalRecord = medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);
            foreach (Report report in patientMedicalRecord.Reports)
            {
                foreach (Therapy therapy in report.Therapy)
                {
                    //ovde dodje do terapije
                    DateTime start = new DateTime(report.CreateDate.Year, report.CreateDate.Month, report.CreateDate.Day, 0, 0, 0);
                    DateTime end = report.CreateDate.AddDays(therapy.Duration);
                    int addingHours = 24 / therapy.PerDay; //ovoliko da se dodaje

                    //popuniti liste
                    for (int i = 0; i < therapy.Duration; ++i)
                    {
                        for (int j = 0; j < therapy.PerDay; ++j)
                        {
                            //proveri je l to to vreme
                            if(DateTime.Now.CompareTo(start.AddDays(i).AddHours(j * addingHours)) == 0)
                            {
                                MessageBox.Show("Popiti lek" + therapy.Medicine);
                            }
                        }
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new ExaminationsList();
            //ExaminationsList examinationsList = new ExaminationsList();

            //ListExaminations listExaminations = new ListExaminations();
            //listExaminations.Show();
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
