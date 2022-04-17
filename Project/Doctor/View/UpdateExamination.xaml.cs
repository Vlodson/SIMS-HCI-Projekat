using Controller;
using Model;
using Repository;
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
using HospitalMain.Enums;

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
        private ExaminationRepo _examRepo;


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
            _examRepo = app.ExaminationRepo;

            DUR.Text = selectedItem.Duration.ToString();
            TIP.Text = selectedItem.EType.ToString();
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
            string dateAndTime = datePicker.Text + " " + timePicker.Text;
            DateTime dt = DateTime.Parse(dateAndTime);

            string roomId = ComboBoxSoba.Text;

            string patientName = ComboBoxPacijent.Text;

            int duration = Int32.Parse(DUR.Text);

            string type = TIP.Text;

            Examination newExam = new Examination(roomId, dt, "ID5", duration, ExaminationTypeEnum.Surgery, patientName, "IDDOC");
            _examController.DoctorEditExam(ExaminationSchedule.SelectedItem.Id, newExam);
            _examRepo.SaveExamination();
            ExaminationSchedule examinationSchedule = new ExaminationSchedule();
            examinationSchedule.Show();
            this.Close();
        }

    }
}
