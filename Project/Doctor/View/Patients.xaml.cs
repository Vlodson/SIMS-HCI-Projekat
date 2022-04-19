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

        private PatientController patientController;
        private PatientRepo patientRepo;

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
            patientController = app.patientController;
            patientRepo = app.patientRepo;

            if (File.Exists(patientRepo.dbPath))
                patientRepo.LoadPatient();
            patients = patientController.ReadAllPatients();
        }

        private void Choose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
