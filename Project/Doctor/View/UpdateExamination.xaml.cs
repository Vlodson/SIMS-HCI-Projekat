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

        public static ObservableCollection<Patient> PatientsObs
        {
            get;
            set;
        }
        public UpdateExamination(Examination selectedItem, ExaminationSchedule examinationSchedule)
        {
            InitializeComponent();
            this.DataContext = this;
            _examinationSchedule = examinationSchedule;
            _selectedExam = selectedItem;
            PatientsObs = new ObservableCollection<Patient>();

            var app = Application.Current as App;
            _doctorController = app.doctorController;
            _examController = app.examController;
            _patientController = app.patientController;
            _roomController = app.roomController;
            _examRepo = app.examRepo;

            ComboBoxPacijent.Text = selectedItem.PatientId.ToString();
            DUR.Text = selectedItem.Duration.ToString();
            TIP.Text = selectedItem.EType.ToString();
            datePicker.Text = selectedItem.Date.ToString().Split(" ")[0];
            timePicker.Text = selectedItem.Date.ToString().Split(" ")[1];

            foreach (var pom in _patientController.ReadAllPatients())
            {
                PatientsObs.Add(pom);
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string dateAndTime = datePicker.Text + " " + timePicker.Text;
            DateTime dt = DateTime.Parse(dateAndTime);

            string roomId = ComboBoxSoba.Text;

            string patientName = ComboBoxPacijent.Text;

            int duration = Int32.Parse(DUR.Text);

            string type = TIP.Text;

            Examination newExam = new Examination(roomId, dt, _selectedExam.Id , duration, ExaminationTypeEnum.Surgery, patientName, "d1");
            _examController.DoctorEditExam(ExaminationSchedule.SelectedItem.Id, newExam);
            _examRepo.SaveExamination();
            _examinationSchedule = new ExaminationSchedule();
            NavigationService.Navigate(_examinationSchedule);
            
        }
    }
}
