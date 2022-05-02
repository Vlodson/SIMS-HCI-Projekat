using System.Windows;
using Controller;
using Model;
using Utility;
using System.Collections.ObjectModel;
using Repository;
using Service;
using Secretary.Stores;
using Secretary.ViewModel;

namespace Secretary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
            
        public PatientController PatientController { get; set; }
        public PatientRepo PatientRepo { get; set; }

        public MedicalRecordController MedicalRecordController { get; set; }

        public MedicalRecordRepo MedicalRecordRepo { get; set; }

        public DoctorRepo DoctorRepo { get; set; }

        public DoctorController DoctorController { get; set; }

        public ExaminationRepo ExaminationRepo { get; set; }

        public ExamController ExamController { get; set; }

        public RoomRepo RoomRepo { get; set; }

        //treba odraditi navigaciju kako valja
        private readonly NavigationStore _navigationStore;

        public RoomRepo RoomRepo { get; set; }
        public App()
        {
            //_navigationStore = new NavigationStore();
            
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
            PatientRepo = new PatientRepo(GlobalPaths.PatientsDBPath);
            PatientAccountService patientAccService = new PatientAccountService(PatientRepo);
            PatientController = new PatientController(patientAccService);

            DoctorRepo = new DoctorRepo(GlobalPaths.DoctorsDBPath);
            DoctorService doctorService = new DoctorService(DoctorRepo, ExaminationRepo, RoomRepo);
            DoctorController = new DoctorController(doctorService);

            ObservableCollection<MedicalRecord> medicicalRecords = new ObservableCollection<MedicalRecord>();
            MedicalRecordRepo = new MedicalRecordRepo(GlobalPaths.MedicalRecordDBPath);
            MedicalRecordService medicalRecordService = new MedicalRecordService(MedicalRecordRepo);
            MedicalRecordController = new MedicalRecordController(medicalRecordService);

            ExaminationRepo = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            PatientService patientService = new PatientService(PatientRepo, ExaminationRepo, DoctorRepo, RoomRepo);
            ExamController = new ExamController(patientService, doctorService);

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
