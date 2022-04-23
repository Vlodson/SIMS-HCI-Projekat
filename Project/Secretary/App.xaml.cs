using System.Windows;
using Controller;
using Model;
using Utility;
using System.Collections.ObjectModel;
using Repository;
using Service;

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

        public App()
        {
            
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
            PatientRepo = new PatientRepo(GlobalPaths.PatientsDBPath);
            PatientAccountService patientAccService = new PatientAccountService(PatientRepo);
            PatientController = new PatientController(patientAccService);

            ObservableCollection<MedicalRecord> medicicalRecords = new ObservableCollection<MedicalRecord>();
            MedicalRecordRepo = new MedicalRecordRepo(GlobalPaths.MedicalRecordDBPath);
            MedicalRecordService medicalRecordService = new MedicalRecordService(MedicalRecordRepo);
            MedicalRecordController = new MedicalRecordController(medicalRecordService);

        }
         

    }
}
