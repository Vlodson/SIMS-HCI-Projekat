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
    public partial class AddExamination : Window, INotifyPropertyChanged
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

            RoomsObs = new ObservableCollection<Room>();
            PatientsObs = new ObservableCollection<Patient>();

            var app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;
            _patientController = app.PatientController;
            _roomController = app.RoomController;

            foreach (var pom in _patientController.GetAll().ToList())
            {
                PatientsObs.Add(pom);
            }
            foreach (var pom in _roomController.ReadAll().ToList())
            {
                RoomsObs.Add(pom);
            }



        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            Model.Doctor doctor = _doctorController.GetDoctor("111");

            string dateAndTime = datePicker.Text + " " + timePicker.Text;
            DateTime dt = DateTime.Parse(dateAndTime);

            string roomId = ComboBoxSoba.SelectedItem.ToString();
            Room r = _roomController.FindById(roomId);


            string patientName = ComboBoxPacijent.SelectedItem.ToString();
            Patient p = new Patient("246", patientName, "Nikic", new DateTime(1999, 1, 1, 12, 12, 12), new List<Examination>());

            int duration = Int32.Parse(DUR.Text);

            string type = TIP.Text;

            Examination newExam = new Examination(r, dt, "ID5", duration, type, p, doctor);
            _examController.DoctorCreateExam(newExam);

            //ExaminationSchedule.Examinations.Add(newExam);
            convertEntityToView();
            this.Close();
        }

        public void convertEntityToView()
        {
            // stupid slow but i dont really care right now also probably needs a check to see if its null
            ExaminationSchedule.Examinations.Clear();
            string idDoc = "IDDOC";
            List<Examination> exams = this._examController.ReadAll(idDoc);
            foreach (Examination exam in exams)
                ExaminationSchedule.Examinations.Add(exam);
        }
    }
}
