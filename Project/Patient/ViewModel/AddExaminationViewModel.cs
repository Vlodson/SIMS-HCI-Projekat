using Controller;
using Model;
using Patient.View;
using Patient.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patient.ViewModel
{
    public class AddExaminationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CheckedChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private DoctorController _doctorController;
        private PatientController _patientController;
        private ExamController _examController;
        private List<DoctorType> doctorTypes;
        private List<String> doctorTypesString;
        private DoctorType selectedType;
        private string selectedTypeString;
        private List<Doctor> doctors;
        private Doctor selectedDoctor;
        public static DateTime startDate;
        private DateTime endDate;
        private List<Examination> availableExaminations;
        private bool priority;
        private Examination selectedExamination;
        private Window thisWindow;

        public MyICommand ShowExaminationsCommand { get; set; }
        public MyICommand DoctorPriorityCommand { get; set; }
        public MyICommand DatePriorityCommand { get; set; }
        public MyICommand AddExaminationCommand { get; set; }
        public MyICommand TypeChangedCommand    { get; set; }
        

        
        public List<DoctorType> DoctorTypes
        {
            get
            {
                return doctorTypes;
            }
            set
            {
                this.doctorTypes = value;
            }
        }

        public List<String> DoctorTypesString
        {
            get
            {
                return doctorTypesString;
            }
            set
            {
                doctorTypesString = value;
            }
        }

        public DoctorType SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
                doctors = new List<Doctor>();
                foreach (Doctor doctor in _doctorController.GetAll().ToList())
                {
                    if (doctor.Type == SelectedType)
                    {
                        doctors.Add(doctor);
                    }
                }
                OnPropertyChanged("Doctors");
            }
        }

        public String SelectedTypeString
        {
            get
            {
                return selectedTypeString;
            }
            set
            {
                selectedTypeString = value;
                OnPropertyChanged("SelectedTypeString");
                OnPropertyChanged("SelectedType");
                switch (selectedTypeString)
                {
                    case "Pulmologija":
                        SelectedType = DoctorType.Pulmonology;
                        break;
                    case "Kardiologija":
                        SelectedType = DoctorType.Cardiology;
                        break;
                    //case "Neurologija":
                    //    SelectedType = DoctorType.Neurology;
                    //    break;
                    //case "Dermatologija":
                    //    SelectedType = DoctorType.Dermatology;
                    //    break;
                    default:
                        SelectedType = DoctorType.General;
                        break;
                }
                doctors = new List<Doctor>();
                foreach (Doctor doctor in _doctorController.GetAll().ToList())
                {
                    if (doctor.Type == SelectedType)
                    {
                        doctors.Add(doctor);
                    }
                }
                OnPropertyChanged("Doctors");
            }
        }

        public List<Doctor> Doctors
        {
            get
            {
                return doctors;
            }
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }

        public Doctor SelectedDoctor
        {
            get
            {
                return selectedDoctor;
            }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }

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

        public List<Examination> AvailableExaminations
        {
            get
            {
                return availableExaminations;
            }
            set
            {
                availableExaminations = value;
                OnPropertyChanged("AvailableExaminations");
            }
        }

        public bool Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
                OnPropertyChanged("Priority");
            }
        }

        public Examination SelectedExamination
        {
            get
            {
                return selectedExamination;
            }
            set
            {
                selectedExamination = value;
                OnPropertyChanged("SelectedExamination");
                AddExaminationCommand.RaiseCanExecuteChanged();
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

        public AddExaminationViewModel(DateTime sDate, Window window)
        {
            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            _patientController = app.PatientController;
            _examController = app.ExamController;

            doctorTypes = new List<DoctorType>();
            doctorTypes.Add(DoctorType.Pulmonology);
            doctorTypes.Add(DoctorType.Cardiology);
            doctorTypes.Add(DoctorType.Dermatology);
            doctorTypes.Add(DoctorType.Neurology);
            //doctorTypes.Add(DoctorType.specialistCheckup);

            doctorTypesString = new List<String>();
            doctorTypesString.Add("Pulmologija");
            doctorTypesString.Add("Kardiologija");
            //doctorTypesString.Add("Dermatologija");
            //doctorTypesString.Add("Neurologija");
            doctorTypesString.Add("Opšta praksa");

            SelectedType = DoctorType.Pulmonology;
            SelectedTypeString = "Pulmologija";
            doctors = new List<Doctor>();
            foreach(Doctor doctor in _doctorController.GetAll().ToList())
            {
                if(doctor.Type == SelectedType)
                {
                    doctors.Add(doctor);
                }
            }
            StartDate = sDate;
            EndDate = sDate.AddDays(7);

           
            ShowExaminationsCommand = new MyICommand(OnShowExaminations);
            priority = false;
            DoctorPriorityCommand = new MyICommand(OnDoctorPriority);
            DatePriorityCommand = new MyICommand(OnDatePriority);
            AddExaminationCommand = new MyICommand(OnAddExamination, CanAddExamination);
            TypeChangedCommand = new MyICommand(OnTypeChanged, CanTypeChanged);
            thisWindow = window;
        }

        private void OnShowExaminations()
        {
            if(SelectedDoctor == null)
            {
                List<Doctor> doctors = _doctorController.GetAll().ToList();
                List<Examination> examinations = new List<Examination>();
                switch (selectedTypeString)
                {
                    case "Pulmologija":
                        SelectedType = DoctorType.Pulmonology;
                        break;
                    case "Kardiologija":
                        SelectedType = DoctorType.Cardiology;
                        break;
                    //case "Neurologija":
                    //    SelectedType= DoctorType.Neurology;
                    //    break;
                    //case "Dermatologija":
                    //    SelectedType = DoctorType.Dermatology;
                    //    break;
                    default:
                        SelectedType = DoctorType.General;
                        break;
                }
                priority = false;
                foreach(Doctor doctor in doctors)
                {
                    if(doctor.Type == SelectedType)
                    {
                        List<Examination> listExaminationsWithRooms = _doctorController.GetFreeGetFreeExaminations(doctor, startDate, endDate, priority);
                        foreach (Examination exam in listExaminationsWithRooms)
                        {
                            exam.DoctorNameSurname = _doctorController.GetDoctor(doctor.Id).NameSurname;
                        }
                        examinations.AddRange(listExaminationsWithRooms);
                    }
                }
                AvailableExaminations = examinations;
            }
            else
            {
                List<Examination> examinations = _doctorController.GetFreeGetFreeExaminations(SelectedDoctor, startDate, endDate, priority);
                foreach (Examination exam in examinations)
                {
                    exam.DoctorNameSurname = _doctorController.GetDoctor(SelectedDoctor.Id).NameSurname;
                }
                AvailableExaminations = examinations;
            }
        }

        private void OnDoctorPriority()
        {
            priority = true;
        }
        private void OnDatePriority()
        {
            priority = false;
        }

        private bool CanAddExamination()
        {
            return SelectedExamination != null;
        }

        private void OnAddExamination()
        {
            Model.Patient patient = _patientController.ReadPatient(Login.loggedId);
            Examination newExamination = new Examination(null, SelectedExamination.Date, RandomString(6), 2, HospitalMain.Enums.ExaminationTypeEnum.OrdinaryExamination, patient.ID, selectedExamination.DoctorId);
            _examController.PatientCreateExam(newExamination, SelectedExamination.Date);
            _examController.SaveExaminationRepo();
            ObservableCollection<Examination> examinations = _examController.ReadPatientExams(Login.loggedId);
            foreach (Examination exam in examinations)
            {
                exam.DoctorNameSurname = _doctorController.GetDoctor(exam.DoctorId).NameSurname;
            }
            thisWindow.Close();
        }

        private bool CanTypeChanged()
        {
            return SelectedDoctor != null;
        }
        private void OnTypeChanged()
        {
            doctors = new List<Doctor>();
            foreach (Doctor doctor in _doctorController.GetAll().ToList())
            {
                if (doctor.Type == SelectedType)
                {
                    doctors.Add(doctor);
                }
            }
        }
        
    }
}
