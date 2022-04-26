using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HospitalMain.Enums;
using Model;
using Secretary.ComboBoxTemplate;
using Controller;
using Secretary.Commands;
using Secretary.View;
using System.Windows;

namespace Secretary.ViewModel
{
    public class AddAppointmentViewModel : ViewModelBase
    {
        private ExamController examController;
        private DoctorController doctorController;
        private PatientController patientController; 

        //id pacijenta
        private String patientID;
        public String PatientID
        {
            get { return patientID; }
            set { patientID = value; OnPropertyChanged(nameof(PatientID)); }
        }

        //id sobe
        private String roomID;

        public String RoomID
        {
            get { return roomID; }
            set { roomID = value; OnPropertyChanged(nameof(RoomID)); }
        }

        private DateTime date = new DateTime(2022, 1, 1);

        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        public ICommand AddCommand { get; }

        //Doktor
        private List<ComboBoxData<Doctor>> doctorComboBox = new List<ComboBoxData<Doctor>>();

        public List<ComboBoxData<Doctor>> DoctorComboBox
        {
            get { return doctorComboBox; }
            set { doctorComboBox = value; OnPropertyChanged(nameof(DoctorComboBox)); }
        }

        private Doctor doctor;
        public Doctor Doctor
        {
            get { return doctor; }
            set { doctor = value; OnPropertyChanged(nameof(Doctor)); }
        }

        private void FillDoctorComboBoxData()
        {
            foreach(Doctor doctor in doctorController.GetAll())
            {
                doctorComboBox.Add(new ComboBoxData<Doctor> { Name = doctor.Name + " " + doctor.Surname, Value = doctor });
            }
        }

        //Tip doktora
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
            foreach(DoctorType doctorType in Enum.GetValues<DoctorType>())
            {
                doctorTypeComboBox.Add(new ComboBoxData<DoctorType> { Name = doctorType.ToString(), Value = doctorType });
            }
        }

        //Tip pregleda
        private List<ComboBoxData<ExaminationTypeEnum>> examTypeComboBox = new List<ComboBoxData<ExaminationTypeEnum>>();
        public List<ComboBoxData<ExaminationTypeEnum>> ExamTypeComboBox
        {
            get { return examTypeComboBox; }
            set { examTypeComboBox = value; OnPropertyChanged(nameof(ExamTypeComboBox)); }
        }

        
        private ExaminationTypeEnum examinationTypeEnum;
        public ExaminationTypeEnum ExaminationTypeEnum { get { return examinationTypeEnum; } set { examinationTypeEnum = value; OnPropertyChanged(nameof(ExaminationTypeEnum)); } }

        private void FillExamTypeComboBoxData()
        {
            foreach (ExaminationTypeEnum examType in Enum.GetValues<ExaminationTypeEnum>())
            {
                examTypeComboBox.Add(new ComboBoxData<ExaminationTypeEnum> { Name = examType.ToString(), Value = examType });
            }
        }

        private Window _addApointment;

        public AddAppointmentViewModel(CRUDAppointmentsViewModel cRUDAppointmentsViewModel, Window addAppointment)
        {

            var app = System.Windows.Application.Current as App;
            doctorController = app.DoctorController;
            examController = app.ExamController;
            patientController = app.PatientController;

            _addApointment = addAppointment;

            FillDoctorTypeComboBoxData();
            FillDoctorComboBoxData();
            FillExamTypeComboBoxData();

            //this treba kao param
            AddCommand = new AddAppointmentCommand(this, cRUDAppointmentsViewModel, examController, _addApointment);
        }
    }
}
