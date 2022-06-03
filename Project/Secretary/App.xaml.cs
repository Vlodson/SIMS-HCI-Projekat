using System.Windows;
using Controller;
using Model;
using Utility;
using System.Collections.ObjectModel;
using Repository;
using Service;
using Secretary.Stores;
using Secretary.ViewModel;
using HospitalMain.Repository;
using HospitalMain.Service;
using HospitalMain.Controller;
using HospitalMain.Enums;

namespace Secretary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
            
        public PatientController PatientController { get; set; }
        public PatientRepo PatientRepo { get; set; }
        public EmergencyService EmergencyService { get; set; }

        public FreeDaysRequestRepo FreeDaysRequestRepo { get; set; }
        public FreeDaysRequestService FreeDaysRequestService { get; set; }
        public FreeDaysRequestController FreeDaysController { get; set; }

        public DynamicEquipmentRepo DynamicEquipmentRepo { get; set; }
        public DynamicEquipmentService DynamicEquipmentService { get; set; }
        public DynamicEquipmentController DynamicEquipmentController { get; set; }

        public MedicalRecordController MedicalRecordController { get; set; }

        public MedicalRecordRepo MedicalRecordRepo { get; set; }

        public DoctorRepo DoctorRepo { get; set; }

        public DoctorController DoctorController { get; set; }

        public ExaminationRepo ExaminationRepo { get; set; }

        public ExamController ExamController { get; set; }

        public UserAccountRepo UserAccountRepo { get; set; }

        public QuestionnaireRepo QuestionnaireRepo { get; set; }

        public UserAccountController UserAccountController { get; set; }
        
        public RoomRepo RoomRepo { get; set; }
        public RoomController RoomController { get; set; }
        public EquipmentRepo EquipmentRepo { get; set; }

        public MeetingsRepo MeetingsRepo { get; set; }
        public MeetingsService MeetingsService { get; set; }
        public MeetingController MeetingController { get; set; }
        public RenovationRepo RenovationRepo { get; set; }
        public EquipmentTransferRepo EquipmentTransferRepo { get; set; }

        //treba odraditi navigaciju kako valja
        private readonly NavigationStore _navigationStore;

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjQ5MjM0QDMyMzAyZTMxMmUzMFNoQ1huVGdPUHA3RUU1ZXljUzl2SjNqeGxaZCtmMXlZWE1RYWh6bVhRcXM9");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("sr-Latn-RS");
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sr-Latn-RS");

            ExaminationRepo = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            EquipmentRepo = new EquipmentRepo(GlobalPaths.EquipmentDBPath);
            RoomRepo = new RoomRepo(GlobalPaths.RoomsDBPath, EquipmentRepo);
            RoomService roomService = new RoomService(RoomRepo);
            RoomController = new RoomController(roomService);
            QuestionnaireRepo = new QuestionnaireRepo(GlobalPaths.QuestionnaireDBPath);
            DynamicEquipmentRepo = new DynamicEquipmentRepo(GlobalPaths.DynamicEquipmentDBPath);
            DynamicEquipmentService = new DynamicEquipmentService(DynamicEquipmentRepo, EquipmentRepo, RoomRepo);
            DynamicEquipmentController = new DynamicEquipmentController(DynamicEquipmentService);

            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
            PatientRepo = new PatientRepo(GlobalPaths.PatientsDBPath);
            PatientAccountService patientAccService = new PatientAccountService(PatientRepo);
            PatientController = new PatientController(patientAccService);

            DoctorRepo = new DoctorRepo(GlobalPaths.DoctorsDBPath);
            EmergencyService = new(ExaminationRepo, DoctorRepo);
            DoctorService doctorService = new DoctorService(DoctorRepo, ExaminationRepo, RoomRepo, PatientRepo);
            DoctorController = new DoctorController(doctorService, EmergencyService);

            ObservableCollection<MedicalRecord> medicicalRecords = new ObservableCollection<MedicalRecord>();
            MedicalRecordRepo = new MedicalRecordRepo(GlobalPaths.MedicalRecordDBPath);
            MedicalRecordService medicalRecordService = new MedicalRecordService(MedicalRecordRepo);
            MedicalRecordController = new MedicalRecordController(medicalRecordService);

            PatientService patientService = new PatientService(PatientRepo, ExaminationRepo, DoctorRepo, RoomRepo, QuestionnaireRepo);
            ExamController = new ExamController(patientService, doctorService);

            UserAccountRepo = new UserAccountRepo(GlobalPaths.UserDBPath);
            UserAccountService userAccountService = new UserAccountService(UserAccountRepo);
            UserAccountController = new UserAccountController(userAccountService);

            FreeDaysRequestRepo = new FreeDaysRequestRepo(GlobalPaths.RequestDBPath);
            FreeDaysRequestService = new FreeDaysRequestService(FreeDaysRequestRepo, DoctorRepo);
            FreeDaysController = new FreeDaysRequestController(FreeDaysRequestService);

            RenovationRepo = new RenovationRepo(GlobalPaths.RenovationDBPath, RoomRepo);
            EquipmentTransferRepo = new EquipmentTransferRepo(GlobalPaths.EquipmentTransfersDBPath, RoomRepo, EquipmentRepo);

            MeetingsRepo = new MeetingsRepo(GlobalPaths.MeetingsDBPath);
            MeetingsService = new MeetingsService(MeetingsRepo, DoctorRepo, RoomRepo, doctorService, RenovationRepo, EquipmentTransferRepo, FreeDaysRequestService);
            MeetingController = new MeetingController(MeetingsService);

            for(int i = 1; i <= 15; i++)
            {
                if(i % 3 == 0)
                {
                    Room room = new Room(i.ToString(), 1, i, false, RoomTypeEnum.Meeting_Room, RoomTypeEnum.Meeting_Room);
                    RoomController.CreateRoom(room);
                } 
                else if(i % 3 == 1)
                {
                    Room room = new Room(i.ToString(), 1, i, false, RoomTypeEnum.Patient_Room, RoomTypeEnum.Patient_Room);
                    RoomController.CreateRoom(room);
                } 
                else if(i % 3 == 2)
                {
                    Room room = new Room(i.ToString(), 1, i, false, RoomTypeEnum.Operation_Room, RoomTypeEnum.Operation_Room);
                    RoomController.CreateRoom(room);
                }
            }
        }

        //protected override void OnStartup(StartupEventArgs e)
        //{

        //    //Treba ovo proveriti
        //    //_navigationStore.CurrentViewModel = new MainViewModel(_navigationStore);

        //    MainWindow = new MainWindow()
        //    {
        //        DataContext = new MainViewModel(_navigationStore)
        //    };
        //    //baca ovde excaption
        //    //MainWindow.Show();

        //    base.OnStartup(e);
        //}
    }
}
