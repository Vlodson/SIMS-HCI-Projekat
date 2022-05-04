using Repository;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for DoctorNavBar.xaml
    /// </summary>
    public partial class DoctorNavBar : Window
    {
        private ReportRepo _reportRepo;
        private TherapyRepo _therapyRepo;
        private ExaminationRepo _examRepo;
        private PatientRepo _patientRepo;
        private RoomRepo _roomRepo;
        private MedicalRecordRepo _medicalRecordRepo;
        private UserAccountRepo _userAccountRepo;

        public DoctorNavBar()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            _examRepo = app.examRepo;
            _reportRepo = app.reportRepo;
            _therapyRepo = app.therapyRepo;
            _patientRepo = app.patientRepo;
            _roomRepo = app.roomRepo;
            _medicalRecordRepo = app.medicalRecordRepo;
            _userAccountRepo = app.userAccountRepo;

            if (File.Exists(_patientRepo.DBPath))
                _patientRepo.LoadPatient();
            if (File.Exists(_roomRepo.dbPath))
                _roomRepo.LoadRoom();
            if (File.Exists(_examRepo.dbPath))
                _examRepo.LoadExamination();

        }

        private void ButtonSchedule(object sender, RoutedEventArgs e)
        {
            Main.Content = new ExaminationSchedule();
        }

        private void ButtonExaminations(object sender, RoutedEventArgs e)
        {
            Main.Content = new EndedExaminations();
        }

        private void ButtonRequests(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonPatients(object sender, RoutedEventArgs e)
        {
            Main.Content = new Patients();
        }

        private void ButtonNotifications(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonLogOut(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _examRepo.SaveExamination();
            _therapyRepo.SaveTherapy();
            _reportRepo.SaveReport();
            _patientRepo.SavePatient();
            _medicalRecordRepo.SaveMedicalRecord();
            _userAccountRepo.SaveUserAccounts();
        }
    }
}
