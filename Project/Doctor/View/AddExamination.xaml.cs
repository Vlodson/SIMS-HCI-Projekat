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
        private RoomController _roomController;
        private ExamController _examController;
        private ExaminationRepo _examRepo;

        private ExaminationSchedule _examinationSchedule;

        public ObservableCollection<Patient> PatientsObs{ get; set;}
        public ObservableCollection<Room> RoomsObs { get; set;}
        public AddExamination(ExaminationSchedule examinationSchedule)
        {
            InitializeComponent();
            this.DataContext = this;
            _examinationSchedule = examinationSchedule;

            //PatientsObs = new ObservableCollection<Patient>();

            App app = Application.Current as App;
            _examController = app.examController;
            _patientController = app.patientController;
            _roomController = app.roomController;
            _examRepo = app.examRepo;

            PatientsObs = _patientController.ReadAllPatients();
            RoomsObs = _roomController.ReadAll();
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            if((Patient)ComboBoxPacijent.SelectedItem == null || (Room)ComboBoxSoba.SelectedItem == null || DUR.Text.Equals(""))
            {
                MessageBox.Show("Molimo popunite sva polja!");
            }
            else
            {
                string dateAndTime = datePicker.Text + " " + timePicker.Text;
                DateTime dt = DateTime.Parse(dateAndTime);

                Room room = (Room)ComboBoxSoba.SelectedItem;

                Patient patient = (Patient)ComboBoxPacijent.SelectedItem;

                int duration = Int32.Parse(DUR.Text);

                string type = TIP.Text;

                Examination newExam = new Examination(room.Id, dt, (new Random()).Next(10000).ToString(), duration, ExaminationTypeEnum.OrdinaryExamination, patient.ID, "d1");

                _examController.DoctorCreateExam(newExam);
                _examRepo.SaveExamination();
                _examinationSchedule = new ExaminationSchedule();
                NavigationService.Navigate(_examinationSchedule);
            }
            
        }
    }
}
