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
    /// Interaction logic for UpdateExamination.xaml
    /// </summary>
    public partial class UpdateExamination : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DoctorController _doctorController;
        private PatientController _patientController;
        private ExamController _examController;
        private RoomController _roomController;


        protected virtual void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public static ObservableCollection<Patient> PatientsObs
        {
            get;
            set;
        }
        public static ObservableCollection<Room> RoomsObs
        {
            get;
            set;
        }


        public UpdateExamination(Examination selectedItem)
        {
            InitializeComponent();
            this.DataContext = this;

            RoomsObs = new ObservableCollection<Room>();
            PatientsObs = new ObservableCollection<Patient>();

            var app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;
            _patientController = app.PatientController;
            _roomController = app.RoomController;

<<<<<<< HEAD
            ComboBoxPacijent.SelectedItem = selectedItem.Patient;
            ComboBoxSoba.SelectedItem = selectedItem.ExamRoom;
=======
            ComboBoxPacijent.Text = selectedItem.Patient.Name;
            ComboBoxSoba.Text = selectedItem.ExamRoom.Id;
>>>>>>> 378b1b360ffe5ed22aceaadceb99b80dcb631148
            DUR.Text = selectedItem.Duration.ToString();
            TIP.Text = selectedItem.Type;
            datePicker.Text = selectedItem.Date.ToString().Split(" ")[0];
            timePicker.Text = selectedItem.Date.ToString().Split(" ")[1];

            foreach (var pom in _patientController.ReadAllPatients())
            {
                PatientsObs.Add(pom);
            }
            foreach (var pom in _roomController.ReadAll().ToList())
            {
                RoomsObs.Add(pom);
            }


        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {

            Model.Doctor doctor = _doctorController.GetDoctor("222");
            string dateAndTime = datePicker.Text + " " + timePicker.Text;
            DateTime dt = DateTime.Parse(dateAndTime);

            string roomId = ComboBoxSoba.Text;
            Room r = new Room(roomId, 3, 3, true, "tip2");


            string patientName = ComboBoxPacijent.Text;
            Guest guest = new Guest("246");
            Patient p = new Patient(guest.ID,"12345678", patientName, "Nikic", new DateTime(1999, 1, 1, 12, 12, 12), new ObservableCollection<Examination>());

            int duration = Int32.Parse(DUR.Text);

            string type = TIP.Text;

            Examination newExam = new Examination(r, dt, "ID5", duration, type, p, doctor);
            _examController.DoctorEditExam(newExam);

            
            this.Close();
        }

    }
}
