using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for AddMedicineToTherapy.xaml
    /// </summary>
    public partial class AddMedicineToTherapy : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string patientBind;
        private DateTime dateBind;
        private Medicine selectedMedicine;
        private Model.MedicalRecord medicalRecord;
        public ObservableCollection<Medicine> MedicinesBind { get; set; }
        private Examination selectedExam;
        private int dur;
        private int per;

        private TherapyController _therapyController;
        private ReportPage _reportPage;
        private PatientController _patientController;
        private MedicineController _medicineController;
        private MedicalRecordController _medicalRecordController;

        public string PatientBind { get => patientBind; set => patientBind = value; }
        public DateTime DateBind { get => dateBind; set => dateBind = value; }
        public Examination SelectedExam { get => selectedExam; set => selectedExam = value; }
        public Model.MedicalRecord MedicalRecord { get => medicalRecord; set => medicalRecord = value; }

        public Medicine SelectedMedicine
        {
            get 
            { 
                return selectedMedicine;
            }
            set 
            { 
                selectedMedicine = value;
                if (_medicineController.CheckAllergens(selectedMedicine, medicalRecord))
                {
                    MessageBox.Show("Alergican je");
                }
            }
        }
        public int Dur
        {
            get
            {
                return dur;
            }
            set
            {
                dur = value;
                OnPropertyChanged("Dur");
            }
        }
        public int Per
        {
            get
            {
                return per;
            }
            set
            {
                per = value;
                OnPropertyChanged("Per");
            }
        }



        public AddMedicineToTherapy(ReportPage reportPage, Examination exam)
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            _therapyController = app.therapyController;
            _patientController = app.patientController;
            _medicineController = app.medicineController;
            _medicalRecordController = app.medicalRecordController;

            PatientBind = _patientController.ReadPatient(exam.PatientId).NameSurname;
            MedicalRecord = _medicalRecordController.GetMedicalRecord(_patientController.ReadPatient(exam.PatientId).ID);
            MedicinesBind = _medicineController.ReadAllApproved();
            DateBind = exam.Date;
            selectedExam = exam;
            _reportPage = reportPage;
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(!comboBoxMedicine.Text.Equals("") && !textBoxDuration.Text.Equals("") && !textBoxPerDay.Text.Equals("")){
                Medicine medicine = (Medicine)comboBoxMedicine.SelectedItem;
                string med = medicine.Name;
                int duration = Int32.Parse(textBoxDuration.Text);
                int perDay = Int32.Parse(textBoxPerDay.Text);
                bool prescription = (bool)checkBoxPrescription.IsChecked;

                Therapy therapy = new Therapy(selectedExam.Id, med, duration, perDay, prescription);
                _therapyController.NewTherapy(therapy);
                _reportPage = new ReportPage(selectedExam);
                NavigationService.Navigate(_reportPage);
            }
            
        }
    }
}
