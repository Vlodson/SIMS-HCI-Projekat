using Controller;
using HospitalMain.Enums;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for AddExamination.xaml
    /// </summary>
    public partial class AddExamination : Page, INotifyPropertyChanged
    {
        private PatientController _patientController;
        private RoomController _roomController;
        private ExamController _examController;
        private ExaminationRepo _examRepo;

        public event PropertyChangedEventHandler PropertyChanged;

        private int _duration;

        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        protected virtual void  OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private ExaminationSchedule _examinationSchedule;

        public ObservableCollection<Patient> PatientsObs{ get; set;}
        public ObservableCollection<Room> RoomsObs { get; set;}
        public AddExamination(ExaminationSchedule examinationSchedule)
        {
            InitializeComponent();
            this.DataContext = this;
            _examinationSchedule = examinationSchedule;

            App app = Application.Current as App;
            _examController = app.examController;
            _patientController = app.patientController;
            _roomController = app.roomController;
            _examRepo = app.examRepo;

            TIP.ItemsSource = Enum.GetValues(typeof(ExaminationTypeEnum));
            PatientsObs = _patientController.ReadAllPatients();
            RoomsObs = _roomController.ReadAll();
        }

        private void Zakazi_Click(object sender, RoutedEventArgs e)
        {
            

            if ((Patient)ComboBoxPacijent.SelectedItem == null || (Room)ComboBoxSoba.SelectedItem == null || DUR.Text.Equals("") || timePicker.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja!");
                return;
            } else
            {
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

                    Examination newExam = new Examination(room.Id, dt, (new Random()).Next(10000).ToString(), duration, type, patient.ID, MainWindow._uid);

                    _examController.DoctorCreateExam(newExam);
                    _examRepo.SaveExamination();
                    _examinationSchedule = new ExaminationSchedule();
                    NavigationService.Navigate(_examinationSchedule);
                }
            }
            
            
        }
    }
}
