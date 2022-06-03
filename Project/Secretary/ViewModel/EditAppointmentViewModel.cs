using Controller;
using HospitalMain.Enums;
using Model;
using Secretary.ComboBoxTemplate;
using Secretary.Commands;
using Secretary.ViewUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private RoomController roomController;

        public ICommand EditCommand { get; }

        //exam id
        private String examID;
        public String ExamID
        {
            get { return examID; }
            set { examID = value; OnPropertyChanged(nameof(examID)); }
        }

        //id pacijenta
        private ObservableCollection<SelectableItemWrapper<Patient>> patientListBox = new ObservableCollection<SelectableItemWrapper<Patient>>();
        public ObservableCollection<SelectableItemWrapper<Patient>> PatientListBox
        {
            get { return patientListBox; }
            set { patientListBox = value; OnPropertyChanged(nameof(PatientListBox)); }
        }

        private Patient _patient;
        public Patient Patient
        {
            get { return _patient; }
            set { _patient = value; OnPropertyChanged(nameof(Patient)); }
        }

        private void FillPatientListBox()
        {
            patientListBox.Clear();
            foreach (Patient patient in patientController.ReadAllPatients())
            {
                patientListBox.Add(new SelectableItemWrapper<Patient> { IsSelected = false, Item = patient });
            }
        }

        private ObservableCollection<ComboBoxData<Room>> roomComboBox = new ObservableCollection<ComboBoxData<Room>>();
        public ObservableCollection<ComboBoxData<Room>> RoomComboBox
        {
            get { return roomComboBox; }
            set { roomComboBox = value; OnPropertyChanged(nameof(RoomComboBox)); }
        }

        //selected
        private Room _room;
        public Room Room
        {
            get { return _room; }
            set { _room = value; OnPropertyChanged(nameof(Room)); }
        }

        private void FillRoomComboBoxData()
        {
            roomComboBox.Clear();
            ObservableCollection<Room> rooms = roomController.GetAllRoomsByExamType(ExaminationTypeEnum);
            foreach (Room room in rooms)
            {
                roomComboBox.Add(new ComboBoxData<Room> { Name = room.RoomNb.ToString(), Value = room });
            }
            Room = rooms.First();
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(nameof(Date)); }
        }

        //Doktor
        private ObservableCollection<SelectableItemWrapper<Doctor>> doctorListBox = new ObservableCollection<SelectableItemWrapper<Doctor>>();
        public ObservableCollection<SelectableItemWrapper<Doctor>> DoctorListBox
        {
            get { return doctorListBox; }
            set { doctorListBox = value; OnPropertyChanged(nameof(DoctorListBox)); }
        }

        private Doctor doctor;
        public Doctor Doctor
        {
            get { return doctor; }
            set { doctor = value; OnPropertyChanged(nameof(Doctor)); }
        }

        private void FillDoctorListBox()
        {
            doctorListBox.Clear();
            foreach (String doctorID in doctorController.GetDoctorsBySpecialization(DoctorType))
            {
                doctorListBox.Add(new SelectableItemWrapper<Doctor> { IsSelected = false, Item = doctorController.GetDoctor(doctorID) });
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
        public ExaminationTypeEnum ExaminationTypeEnum { get { return examinationTypeEnum; } set { examinationTypeEnum = value; OnPropertyChanged(nameof(ExaminationTypeEnum)); FillRoomComboBoxData(); } }

        private void FillExamTypeComboBoxData()
        {
            foreach (ExaminationTypeEnum examType in Enum.GetValues<ExaminationTypeEnum>())
            {
                examTypeComboBox.Add(new ComboBoxData<ExaminationTypeEnum> { Name = examType.ToString(), Value = examType });
            }
        }

        public EditAppointmentViewModel(HomeViewModel homeViewModel, HomePageViewModel homePageViewModel)
        { 

            var app = System.Windows.Application.Current as App;
            doctorController = app.DoctorController;
            examController = app.ExamController;
            patientController = app.PatientController;
            roomController = app.RoomController;

            //ExamID = cRUDAppointmentsViewModel.ExaminationViewModel.ID;
            //PatientID = cRUDAppointmentsViewModel.ExaminationViewModel.PatientID;
            //RoomID = cRUDAppointmentsViewModel.ExaminationViewModel.ExamRoomID;
            //Date = Convert.ToDateTime(cRUDAppointmentsViewModel.ExaminationViewModel.StartDate);
            ////DoctorType?
            //Doctor = cRUDAppointmentsViewModel.ExaminationViewModel.Doctor;
            //ExaminationTypeEnum = cRUDAppointmentsViewModel.ExaminationViewModel.Type;

            Doctor = doctorController.GetAll().First();
            Patient = patientController.ReadAllPatients().First();
            Room = roomController.ReadAll().First();

            FillDoctorTypeComboBoxData();
            FillDoctorListBox();
            FillExamTypeComboBoxData();

            EditCommand = new EditAppointmentCommand(this, homeViewModel, homePageViewModel, examController, doctorController);
        }
    }
}
