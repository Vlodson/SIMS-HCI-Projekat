using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using Utility;

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for AddExamination.xaml
    /// </summary>
    public partial class AddExamination : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private ExamController _examController;
        private DoctorController _doctorController;
        private PatientController _patientController;
        private RoomController _roomController;
        private ExaminationRepo _examinationRepo;

        //public int id = 0;
        
        private List<String> doctorTypes;

        
        

        public ObservableCollection<Doctor> DoctorsObs
        {
            get;
            set;
        }

        public static DateTime startDate;
        private DateTime endDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public List<String> DoctorTypes
        {
            get
            {
                return doctorTypes;
            }
            set
            {
                doctorTypes = value;
                OnPropertyChanged("DoctorTypes");
            }
        }
        
        

        private static Random random = new Random((int)DateTime.Now.Ticks);

        

        private string RandomString(int Size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < Size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
        public AddExamination()
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            _examController = app.ExamController;
            _doctorController = app.DoctorController;
            _patientController = app.PatientController;
            _roomController = app.RoomController;
            _examinationRepo = app.ExaminationRepo;

            
            DoctorsObs = new ObservableCollection<Doctor>();
            doctorTypes = new List<string>();
            StartDate = DateTime.Now.AddDays(1);
            EndDate = DateTime.Now.AddDays(7);
            List<Doctor> doctors = _doctorController.GetAll().ToList();

            DoctorTypeSelected.SelectedIndex = 0;
            foreach (Doctor doctor in doctors)
            {
                if(doctor.Type == Model.DoctorType.Pulmonology)
                {
                    DoctorsObs.Add(doctor);
                }
            }

            doctorTypes.Add("Pulmologija");
            doctorTypes.Add("Opšta praksa");
            doctorTypes.Add("Kardiologija");
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
                
                StartDate = (DateTime)Start.SelectedDate;
                
                EndDate = (DateTime)End.SelectedDate;

                bool priority = true; //ako je doktor onda je true, termin false
                if(doctorPriority.IsChecked == true)
                {
                    priority = true;
                }
                else
                {
                    priority = false;
                }
                
                List<Examination> listExaminations = _doctorController.GetFreeGetFreeExaminations(doctor, startDate, endDate, priority);
                foreach(Examination exam in listExaminations)
                {
                    exam.DoctorNameSurname = _doctorController.GetDoctor(doctor.Id).NameSurname;
                }
                
                ExamsAvailable.ItemsSource = listExaminations;
            }
            
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            
            if (ExamsAvailable.SelectedIndex != -1)
            {
                Model.Patient patient = _patientController.ReadPatient("2");
                Doctor doctor = (Doctor)DoctorCombo.SelectedItem;
                Examination selectedExamination = (Examination)ExamsAvailable.SelectedItem;
                DateTime dt = selectedExamination.Date;
                
                
                Room getRoom = new Room();
                
                if (File.Exists(GlobalPaths.RoomsDBPath))
                    _roomController.LoadRoom();
                

                foreach (Room room in _roomController.ReadAll())
                {
                    //Console.WriteLine("nesto radi");
                    if (room.Occupancy == false)
                    {
                        getRoom = room;
                        break;
                    }
                }

                //Room r1 = new Room("name", 1, 1, false, HospitalMain.Enums.RoomTypeEnum.Patient_Room);
                int id = 0;
                for(int i = 0; i < _examController.GetExaminations().Count; ++i)
                {
                    ++id;
                }
                ++id;
                Examination newExam = new Examination(getRoom.Id, dt, id.ToString(), 2,HospitalMain.Enums.ExaminationTypeEnum.OrdinaryExamination, patient.ID, doctor.Id);

                _examController.PatientCreateExam(newExam);
                _examinationRepo.SaveExamination();
                ObservableCollection<Examination> examinations = _examController.ReadPatientExams("2");
                foreach (Examination exam in examinations)
                {
                    exam.DoctorNameSurname = _doctorController.GetDoctor(exam.DoctorId).NameSurname;
                }
                ExaminationsList.Examinations = examinations;
                this.Close();
            }

        }

        private void ChangeType(object sender, SelectionChangedEventArgs e)
        {
            DoctorType selectedType = DoctorType.Pulmonology;
            switch (DoctorTypeSelected.SelectedIndex)
            {
                case 0:
                    selectedType = DoctorType.Pulmonology;
                    DoctorTypeSelected.SelectedIndex = 0;
                    break;
                case 1:
                    selectedType = DoctorType.specialistCheckup;
                    DoctorTypeSelected.SelectedIndex = 1;
                    break;
                case 2:
                    selectedType = DoctorType.Cardiology;
                    DoctorTypeSelected.SelectedIndex = 2;
                    break;
            }

            
            List<Doctor> doctors = _doctorController.GetAll().ToList();
            DoctorsObs = new ObservableCollection<Doctor>();
            foreach (Doctor doctor in doctors)
            {
                if(doctor.Type == selectedType)
                {
                    DoctorsObs.Add(doctor);
                }
            }
            DoctorCombo.ItemsSource = DoctorsObs;
        }
    }
}
