using System.Windows;
using Controller;
using Model;
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
            string path = "C:\\Users\\One\\Desktop\\F\\3. godina\\2. semestar\\SIMS & HCI Projekat\\SIMS-HCI-Projekat\\Project\\Secretary\\Repository\\Patients.json";
            patientRepo = new PatientRepo(path);
            PatientAccountService patientAccService = new PatientAccountService(patientRepo);
            patientController = new PatientController(patientAccService);

        }
         

    }
}
