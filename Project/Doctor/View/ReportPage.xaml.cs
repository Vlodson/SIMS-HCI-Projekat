using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        private TherapyRepo _therapyRepo;
        private TherapyController _therapyController;
        private ReportController _reportController;
        private readonly PatientController _patientController;
        private MedicalRecordController _medicalRecordController;
        private MedicalRecordRepo _medicalRecordRepo;
        private ReportRepo _reportRepo;
        private string patientBind;
        private DateTime dateBind;
        private Examination selectedExam;

        public static ObservableCollection<Therapy> therapyBind
        {
            get;
            set;
        }
        
        public ReportPage(Examination exam)
        {
            InitializeComponent();
            this.DataContext = this;
            
            App app = Application.Current as App;
            _therapyRepo = app.therapyRepo;
            _therapyController = app.therapyController;
            _reportController = app.reportController;
            _medicalRecordController = app.medicalRecordController;
            _reportRepo = app.reportRepo;
            _medicalRecordRepo = app.medicalRecordRepo;
            _patientController= app.patientController;

            this.PatientBind = _patientController.ReadPatient(exam.PatientId).NameSurname;
            this.DateBind = exam.Date;
            this.selectedExam = exam;

            if (File.Exists(_therapyRepo.DBPath))
                _therapyRepo.LoadTherapy();

            therapyBind = _therapyController.findById(exam.Id);
        }

        public string PatientBind { get => patientBind; set => patientBind = value; }
        public DateTime DateBind { get => dateBind; set => dateBind = value; }
        public Examination SelectedExam { get => selectedExam; set => selectedExam = value; }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBox txtBox = sender as TextBox;
            if (txtBox.Text == "Unesi izvestaj sa pregleda...")
                txtBox.Text = string.Empty;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddMedicineToTherapy amtt = new AddMedicineToTherapy(this, selectedExam);
            NavigationService.Navigate(amtt);
        }

        private void SaveReport_Click(object sender, RoutedEventArgs e)
        {
            string description = textBoxDescription.Text;
            ObservableCollection<Therapy> therapies = _therapyController.findById(selectedExam.Id);

            Report report = new Report(selectedExam.Id, description, selectedExam.Date, selectedExam.PatientId, selectedExam.DoctorId, therapies, "");
            _reportController.NewReport(report);
            _medicalRecordController.AddNewReport(selectedExam.PatientId, report);
            _reportRepo.SaveReport();
            _medicalRecordRepo.SaveMedicalRecord();
            EndedExaminations ended = new EndedExaminations();
            EndedExaminations.examinations.Remove(SelectedExam);
            ended.dataGridExams.ItemsSource = EndedExaminations.examinations;
            NavigationService.Navigate(ended);

        }
        private void AddReferral_Click(object sender, RoutedEventArgs e)
        {
            ReferralPage referralPage = new ReferralPage(selectedExam);
            NavigationService.Navigate(referralPage);
        }
        private void AddExcuses_Click(object sender, RoutedEventArgs e)
        {
            ExcusesPage excusesPage = new ExcusesPage(selectedExam);
            NavigationService.Navigate(excusesPage);
        }
    }
}
