using Controller;
using HospitalMain.Enums;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UpdateExamination.xaml
    /// </summary>
    public partial class UpdateExamination : Page
    {
        private DoctorController _doctorController;
        private PatientController _patientController;
        private ExamController _examController;
        private RoomController _roomController;
        private ExaminationRepo _examRepo;

        private ExaminationSchedule _examinationSchedule;

        private Examination _selectedExam { get; set; }

        public ObservableCollection<Patient> PatientsObs { get; set; }
        public ObservableCollection<Room> RoomsObs { get; set; }
        public UpdateExamination()
        {
            InitializeComponent();
            this.DataContext = this;

            var app = Application.Current as App;
            _doctorController = app.doctorController;
            _examController = app.examController;
            _patientController = app.patientController;
            _roomController = app.roomController;
            _examRepo = app.examRepo;

            TIP.ItemsSource = Enum.GetValues(typeof(ExaminationTypeEnum));
            PatientsObs = _patientController.ReadAllPatients();
            RoomsObs = _roomController.ReadAll();
        }
        public UpdateExamination(Examination selectedItem, ExaminationSchedule examinationSchedule)
        {
            InitializeComponent();
            this.DataContext = this;
            _examinationSchedule = examinationSchedule;
            _selectedExam = selectedItem;

            var app = Application.Current as App;
            _doctorController = app.doctorController;
            _examController = app.examController;
            _patientController = app.patientController;
            _roomController = app.roomController;
            _examRepo = app.examRepo;

            ComboBoxPacijent.Text = selectedItem.PatientId;
            DUR.Text = selectedItem.Duration.ToString();
            TIP.SelectedItem = selectedItem.EType;
            datePicker.Text = selectedItem.Date.ToString().Split(" ")[0];
            timePicker.SelectedValue = selectedItem.Date.ToString().Split(" ")[1];

            TIP.ItemsSource = Enum.GetValues(typeof(ExaminationTypeEnum));
            PatientsObs = _patientController.ReadAllPatients();
            RoomsObs = _roomController.ReadAll();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

            if ((Patient)ComboBoxPacijent.SelectedItem == null || (Room)ComboBoxSoba.SelectedItem == null || DUR.Text.Equals("") || timePicker.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return;
            }
            else
            {
            }
            string dateAndTime = datePicker.Text + " " + timePicker.Text;
            DateTime dt = DateTime.Parse(dateAndTime);
            int res = DateTime.Compare(dt, DateTime.Now);
            bool occupiedDate = _examController.occupiedDate(dt);

            
            if (res < 0)
            {
                ErrorLabel.Content = "Mozete izabrati samo buduce datume!";
                return;
            }
            else if (occupiedDate)
            {
                ErrorLabel.Content = "Odabrani termin nije dostupan!";
                return;
            }
            else
            {
                Room room = (Room)ComboBoxSoba.SelectedItem;

                Patient patient = (Patient)ComboBoxPacijent.SelectedItem;

                int duration = Int32.Parse(DUR.Text);

                ExaminationTypeEnum type = (ExaminationTypeEnum)this.TIP.SelectedItem;

                //Examination newExam = new Examination(room.Id, dt, _selectedExam.Id, duration, type, patient.ID, MainWindow._uid);
                //_examController.DoctorEditExam(ExaminationSchedule.SelectedItem.Id, newExam);
                //_examRepo.SaveExamination();
                _examinationSchedule = new ExaminationSchedule();
                NavigationService.Navigate(_examinationSchedule);
            }

             
            
        }
        private void DUR_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
