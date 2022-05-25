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

namespace Secretary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
            
        public PatientController PatientController { get; set; }
        public PatientRepo PatientRepo { get; set; }

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
        public EquipmentRepo EquipmentRepo { get; set; }

        //treba odraditi navigaciju kako valja
        private readonly NavigationStore _navigationStore;

        public App()
        {
            //_navigationStore = new NavigationStore();
            ExaminationRepo = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            EquipmentRepo = new EquipmentRepo(GlobalPaths.EquipmentDBPath);
            RoomRepo = new RoomRepo(GlobalPaths.RoomsDBPath, EquipmentRepo);
            QuestionnaireRepo = new QuestionnaireRepo(GlobalPaths.QuestionnaireDBPath);
            DynamicEquipmentRepo = new DynamicEquipmentRepo(GlobalPaths.DynamicEquipmentDBPath);
            DynamicEquipmentService = new DynamicEquipmentService(DynamicEquipmentRepo, EquipmentRepo, RoomRepo);
            DynamicEquipmentController = new DynamicEquipmentController(DynamicEquipmentService);

            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
            PatientRepo = new PatientRepo(GlobalPaths.PatientsDBPath);
            PatientAccountService patientAccService = new PatientAccountService(PatientRepo);
            PatientController = new PatientController(patientAccService);

            DoctorRepo = new DoctorRepo(GlobalPaths.DoctorsDBPath);
            DoctorService doctorService = new DoctorService(DoctorRepo, ExaminationRepo, RoomRepo, PatientRepo);
            DoctorController = new DoctorController(doctorService);

            ObservableCollection<MedicalRecord> medicicalRecords = new ObservableCollection<MedicalRecord>();
            MedicalRecordRepo = new MedicalRecordRepo(GlobalPaths.MedicalRecordDBPath);
            MedicalRecordService medicalRecordService = new MedicalRecordService(MedicalRecordRepo);
            MedicalRecordController = new MedicalRecordController(medicalRecordService);

            PatientService patientService = new PatientService(PatientRepo, ExaminationRepo, DoctorRepo, RoomRepo, QuestionnaireRepo);
            ExamController = new ExamController(patientService, doctorService);

            UserAccountRepo = new UserAccountRepo(GlobalPaths.UserDBPath);
            UserAccountService userAccountService = new UserAccountService(UserAccountRepo);
            UserAccountController = new UserAccountController(userAccountService);
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
