using Controller;
using HospitalMain.Enums;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddExamination.xaml
    /// </summary>
    public partial class AddExamination : Page
    {
        private PatientController _patientController;
        private ExamController _examController;
        private ExaminationRepo _examRepo;

        private ReportPage _reportPage;
        public ObservableCollection<Patient> PatientsObs
        {
            get;
            set;
        }
        public AddExamination(ReportPage reportPage)
        {
            InitializeComponent();
            this.DataContext = this;
            _reportPage = reportPage;

            PatientsObs = new ObservableCollection<Patient>();

            App app = Application.Current as App;
            _examController = app.examController;
            _patientController = app.patientController;
            _examRepo = app.examRepo;

            foreach (var pom in _patientController.ReadAllPatients())
            {
                PatientsObs.Add(pom);
            }
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            ////Model.Doctor doctor = _doctorController.GetDoctor("IDDOC");
            Model.Doctor doctor = new Model.Doctor("IDDOC", "pera", "Peric", new DateTime(1980, 11, 11), DoctorType.specialistCheckup, new List<Examination>());

            string dateAndTime = datePicker.Text + " " + timePicker.Text;
            DateTime dt = DateTime.Parse(dateAndTime);

            string roomId = ComboBoxSoba.SelectedItem.ToString();

            string patientName = ComboBoxPacijent.SelectedItem.ToString();

            int duration = Int32.Parse(DUR.Text);

            string type = TIP.Text;

            Examination newExam = new Examination(roomId, dt, (new Random()).Next(10000).ToString(), duration, ExaminationTypeEnum.OrdinaryExamination, patientName, "IDDOC");

            _examController.DoctorCreateExam(newExam);
            _examRepo.SaveExamination();

            NavigationService.Navigate(_reportPage);
        }
    }
}
