using Controller;
using Model;
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
using System.Windows.Shapes;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for AddExamination.xaml
    /// </summary>
    public partial class AddExamination : Window
    {
        private ExamController _examController;
        private DoctorController _doctorController;
        private PatientController _patientController;

        public ObservableCollection<Doctor> DoctorsObs
        {
            get;
            set;
        }
        public ObservableCollection<DateTime> DateTimes
        {
            get;
            set;
        }

        public AddExamination()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            _examController = app.ExamController;
            _doctorController = app.DoctorController;
            _patientController = app.PatientController;

            DoctorsObs = new ObservableCollection<Doctor>(FindAllDoctors());

        }

        private IList<Doctor> FindAllDoctors()
        {
            return _doctorController.GetAll()
                .ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DoctorCombo.SelectedIndex != -1)
            {
                this.DataContext = this;
                Doctor doctor = (Doctor)DoctorCombo.SelectedItem;
                ExamsAvailable.ItemsSource = _doctorController.GetFreeGetFreeExaminations(doctor);
            }
            
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            if(ExamsAvailable.SelectedIndex != -1)
            {
                Model.Patient patient = _patientController.ReadPatient("idPatient1");
                Doctor doctor = (Doctor)DoctorCombo.SelectedItem;
                DateTime dt = (DateTime)ExamsAvailable.SelectedItem;
                Examination newExam = new Examination(new Room("name", 1, 1, false, "type"), dt, "idExam", 2,"pregled", patient, doctor);
                _examController.PatientCreateExam(newExam);
                //_examController.ReadPatientExams("idPatient1").Add(newExam);
                //MainWindow.Examinations.Add(newExam);
                this.Close();
            }

        }
    }
}
