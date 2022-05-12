using Controller;
using HospitalMain.Enums;
using Model;
using Secretary.ComboBoxTemplate;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class EditAppointmentViewModel : ViewModelBase
    {

        private ExamController examController;
        private DoctorController doctorController;
        private PatientController patientController;
        private readonly CRUDAppointmentsViewModel _crudAppointmentsViewModel;

        public ICommand EditCommand { get; }

        //exam id
        private String examID;
        public String ExamID
        {
            get { return examID; }
            set { examID = value; OnPropertyChanged(nameof(examID)); }
        }

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

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

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
            foreach (Doctor doctor in doctorController.GetAll())
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
            foreach (DoctorType doctorType in Enum.GetValues<DoctorType>())
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

        private Window _editApointment;

        public EditAppointmentViewModel(CRUDAppointmentsViewModel cRUDAppointmentsViewModel, Window editApointment)
        {
            _crudAppointmentsViewModel = cRUDAppointmentsViewModel;  

            var app = System.Windows.Application.Current as App;
            doctorController = app.DoctorController;
            examController = app.ExamController;
            patientController = app.PatientController;
            _editApointment = editApointment;

            ExamID = cRUDAppointmentsViewModel.ExaminationViewModel.ID;
            PatientID = cRUDAppointmentsViewModel.ExaminationViewModel.PatientID;
            RoomID = cRUDAppointmentsViewModel.ExaminationViewModel.ExamRoomID;
            Date = Convert.ToDateTime(cRUDAppointmentsViewModel.ExaminationViewModel.Date);
            //DoctorType?
            Doctor = cRUDAppointmentsViewModel.ExaminationViewModel.Doctor;
            ExaminationTypeEnum = cRUDAppointmentsViewModel.ExaminationViewModel.Type;

            FillDoctorTypeComboBoxData();
            FillDoctorComboBoxData();
            FillExamTypeComboBoxData();

            EditCommand = new EditAppointmentCommand(this, _crudAppointmentsViewModel, examController, _editApointment, doctorController);
        }
    }
}
