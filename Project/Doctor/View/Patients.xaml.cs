using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for Patients.xaml
    /// </summary>
    public partial class Patients : Page
    {

        private PatientController _patientController;
        private PatientRepo _patientRepo;


        public static ObservableCollection<Patient> patients
        {
            get;
            set;
        }
        public Patients()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            _patientController = app.patientController;
            _patientRepo = app.patientRepo;

            if (File.Exists(_patientRepo.dbPath))
                _patientRepo.LoadPatient();
            patients = _patientController.ReadAllPatients();
        }
        public void Choose_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)dataGridPatients.SelectedItem;
            MedicalRecord medicalRecord = new MedicalRecord(selectedPatient);
            NavigationService.Navigate(medicalRecord);
        }
    }   
}
