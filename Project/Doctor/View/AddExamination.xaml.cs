using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for AddExamination.xaml
    /// </summary>
    public partial class AddExamination : Window
    {
        private DoctorController _doctorController;
        private PatientController _patientController;
        private ExamController _examController;
        private RoomController _roomController;

        public ObservableCollection<Patient> PatientsObs
        {
            get;
            set;
        }
        public static ObservableCollection<Room> RoomsObs
        {
            get;
            set;
        }

        public AddExamination()
        {
            InitializeComponent();
            this.DataContext = this;

            PatientsObs = new ObservableCollection<Patient>();

            var app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;
            _roomController = app.RoomController;
            _patientController = app.PatientController;

            /*
            foreach (var pom in _patientController.GetAll().ToList())
            {
                PatientsObs.Add(pom);
            }
            */
            foreach (var pom in _patientController.ReadAllPatients())
            {
                PatientsObs.Add(pom);
            }
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            Model.Doctor doctor = _doctorController.GetDoctor("111");

            string dateAndTime = datePicker.Text + " " + timePicker.Text;
            DateTime dt = DateTime.Parse(dateAndTime);

            string roomId = ComboBoxSoba.SelectedItem.ToString();
            //Room r = _roomController.ReadRoom(roomId);
            Room room = new Room(roomId, 5, 1, true, "tip");

            string patientName = ComboBoxPacijent.SelectedItem.ToString();
            Guest guest = new Guest("246");
            Patient p = new Patient(guest.ID, "010199992222", patientName, "Nikic", new DateTime(1999, 1, 1, 12, 12, 12), new ObservableCollection<Examination>());

            int duration = Int32.Parse(DUR.Text);

            string type = TIP.Text;

            Examination newExam = new Examination(room, dt, "ID5", duration, type, p, doctor);
            _examController.DoctorCreateExam(newExam);

            this.Close();
        }

<<<<<<< HEAD
        public void convertEntityToView()
        {
            ExaminationSchedule.Examinations.Clear();
            string idDoc = "IDDOC";
            List<Examination> exams = this._examController.ReadAll(idDoc);
            foreach (Examination exam in exams)
                ExaminationSchedule.Examinations.Add(exam);
        }
=======
>>>>>>> 378b1b360ffe5ed22aceaadceb99b80dcb631148
    }
}
