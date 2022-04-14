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
            
        public PatientController patientController { get; set; }
        public PatientRepo patientRepo { get; set; }

        public App()
        {
            
            ObservableCollection<Patient> patients = new ObservableCollection<Patient>();
            patientRepo = new PatientRepo(GlobalPaths.PatientsDBPath);
            PatientAccountService patientAccService = new PatientAccountService(patientRepo);
            patientController = new PatientController(patientAccService);

        }
         

    }
}
