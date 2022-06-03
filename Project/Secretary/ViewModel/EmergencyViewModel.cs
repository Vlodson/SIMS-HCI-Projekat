using Controller;
using HospitalMain.Enums;
using Model;
using Secretary.ComboBoxTemplate;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class EmergencyViewModel : ViewModelBase
    {
        private readonly PatientController _patientController;
        private readonly DoctorController _doctorController;
        private readonly ExamController _examController;

        public ICommand ShowSuggestedAppointmentsCommand { get; }
        public ICommand AddEmergencyCommand { get; }
        public ICommand GoToCreateGuestFormCommand { get; }

        //broj sobe
        private String roomID;
        public String RoomID
        {
            get { return roomID; }
            set { roomID = value; OnPropertyChanged(nameof(RoomID)); }
        }

        //datum i vreme hitnog slucaja
        private DateTime dateTime = DateTime.Now;
        public DateTime DateTime
        {
            get { return dateTime; }
            set { dateTime = value; OnPropertyChanged(nameof(DateTime)); }
        }

        //vrste pregleda
        private List<ComboBoxData<ExaminationTypeEnum>> examTypeComboBox = new List<ComboBoxData<ExaminationTypeEnum>>();
        public List<ComboBoxData<ExaminationTypeEnum>> ExamTypeComboBox
        {
            get { return examTypeComboBox; }
            set { examTypeComboBox = value; OnPropertyChanged(nameof(ExamTypeComboBox)); }
        }

        private ExaminationTypeEnum selectedExamType;
        public ExaminationTypeEnum SelectedExamType
        {
            get { return selectedExamType; }
            set { selectedExamType = value; OnPropertyChanged(nameof(SelectedExamType)); }
        }

        private void FillExamTypeComboBoxData()
        {
            foreach(ExaminationTypeEnum examType in Enum.GetValues<ExaminationTypeEnum>())
            {
                examTypeComboBox.Add(new ComboBoxData<ExaminationTypeEnum> { Name = examType.ToString(), Value = examType });
            }
        }

        //pacijenti
        private List<ComboBoxData<Patient>> patientComboBox = new List<ComboBoxData<Patient>>();
        public List<ComboBoxData<Patient>> PatientComboBox
        {
            get { return patientComboBox; }
            set { patientComboBox = value; OnPropertyChanged(nameof(PatientComboBox)); }
        }

        private Patient selectedPatient;
        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set { selectedPatient = value; OnPropertyChanged(nameof(SelectedPatient)); }
        }

        private void FillPatientsComboBoxData()
        {
            ObservableCollection<Patient> patientsFromBase = _patientController.ReadAllPatients();
            foreach(Patient patient in patientsFromBase)
            {
                patientComboBox.Add(new ComboBoxData<Patient> { Name = patient.ID + " " + patient.Name + " " + patient.Surname, Value = patient } );
            }
        }

        //tip doktora (specijalizacija)
        private List<ComboBoxData<DoctorType>> doctorTypeComboBox = new List<ComboBoxData<DoctorType>>();
        public List<ComboBoxData<DoctorType>> DoctorTypeComboBox
        {
            get { return doctorTypeComboBox; }
            set { doctorTypeComboBox = value; OnPropertyChanged(nameof(DoctorTypeComboBox)); }
        }

        private DoctorType doctorType;
        public DoctorType DoctorType
        {
            get { return doctorType; }
            set { doctorType = value; OnPropertyChanged(nameof(DoctorType)); }
        }

        private void FillDoctorTypeComboBoxData()
        {
            foreach (DoctorType doctorType in Enum.GetValues<DoctorType>())
            {
                doctorTypeComboBox.Add(new ComboBoxData<DoctorType> { Name = doctorType.ToString(), Value = doctorType });
            }
        }

        //predlozeni termini pregleda
        private ObservableCollection<ExaminationViewModel> suggestedAppointments = new ObservableCollection<ExaminationViewModel>();
        public ObservableCollection<ExaminationViewModel> SuggestedAppointments
        {
            get { return suggestedAppointments; }
            set { suggestedAppointments = value; OnPropertyChanged(nameof(SuggestedAppointments)); }
        }

        //selektovan odabran termin
        private ExaminationViewModel selectedAppointement;
        public ExaminationViewModel SelectedAppointment
        {
            get { return selectedAppointement; }
            set { selectedAppointement = value; OnPropertyChanged(nameof(SelectedAppointment)); }
        }

        private EmergencyGeneralViewModel _emergencyGeneralViewModel;

        public EmergencyViewModel(EmergencyGeneralViewModel emergencyGeneralViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;
            _emergencyGeneralViewModel = emergencyGeneralViewModel;
            doctorType = DoctorType.Pulmonology;

            FillPatientsComboBoxData();
            FillExamTypeComboBoxData();
            FillDoctorTypeComboBoxData();

            //komande
            ShowSuggestedAppointmentsCommand = new ShowSuggestedEACommand(this, _doctorController);
            AddEmergencyCommand = new AddEmergencyCommand(this, _examController, _doctorController, emergencyGeneralViewModel);
            GoToCreateGuestFormCommand = new GoToCreateGuestFormCommand(this, emergencyGeneralViewModel);
        }
    }
}
