using System;
using System.Windows;
using Controller;
using Model;
using System.Collections.ObjectModel;
using Repository;

namespace Secretary
{
    /// <summary>
    /// Interaction logic for CRUDAccountOptions.xaml
    /// </summary>
    public partial class CRUDAccountOptions : Window
    {
        private PatientController patientController;
        private PatientRepo patientRepo;

        public static ObservableCollection<Patient> patients
        {
            get;
            set;
        }

        public CRUDAccountOptions()
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            patientController = app.patientController;
            patientRepo = app.patientRepo;


            patientController.CreatePatient("1", "Pera", "Peric", new DateTime(1994, 05, 06), new ObservableCollection<Examination>());
            patientController.CreatePatient("2", "Ivan", "Ivic", new DateTime(1985, 08, 08), new ObservableCollection<Examination>());
            patientController.CreatePatient("3", "Zika", "Zikic", new DateTime(2001, 11, 11), new ObservableCollection<Examination>());
            patientController.CreatePatient("4", "Zika", "Zikic", new DateTime(2001, 11, 11), new ObservableCollection<Examination>());

            patientRepo.LoadPatient();
            patients = patientController.ReadAllPatients();
        }

        private void addAccBtn_Click(object sender, RoutedEventArgs e)
        {
            
            string name = textName.Text;
            string surname = textSurname.Text;
            string UCIN = textUCIN.Text;
            DateTime dob = Convert.ToDateTime(textDoB.Text);

            patientController.CreatePatient(UCIN, name, surname, dob, new ObservableCollection<Examination>());    

        }

        private void editAccBtn_Click(object sender, RoutedEventArgs e)
        {

            Patient patient = (Patient)dataGridPatients.SelectedItem;

            if (patient != null)
            {

                patient.Name = textName.Text;
                patient.Surname = textSurname.Text;
                patient.ID = textUCIN.Text;
                patient.DoB = Convert.ToDateTime(textDoB.Text);

                patientController.EditPatient(patient.ID, patient.Name, patient.Surname, patient.DoB, patient.examinations);
            }

            dataGridPatients.UnselectAll();
        }

        private void removeAccBtn_Click(object sender, RoutedEventArgs e)
        {
            Patient selectedPatient = (Patient)dataGridPatients.SelectedItem;
            if(selectedPatient != null)
            {
                patientController.RemovePatient(selectedPatient.ID);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            patientRepo.SavePatient();
        }
    }
}
