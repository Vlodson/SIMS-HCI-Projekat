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
        public ObservableCollection<Medicine> MedicinesBind { get; set; }
        private Examination selectedExam;
        private int dur;

        private TherapyController _therapyController;
        private ReportPage _reportPage;
        private PatientController _patientController;
        private MedicineController _medicineController;

        public string PatientBind { get => patientBind; set => patientBind = value; }
        public DateTime DateBind { get => dateBind; set => dateBind = value; }
        public Examination SelectedExam { get => selectedExam; set => selectedExam = value; }

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

        public AddMedicineToTherapy(ReportPage reportPage, Examination exam)
        {
            InitializeComponent();
            this.DataContext = this;

            App app = Application.Current as App;
            _therapyController = app.therapyController;
            _patientController = app.patientController;
            _medicineController = app.medicineController;

            PatientBind = _patientController.ReadPatient(exam.PatientId).NameSurname;
            MedicinesBind = _medicineController.ReadAll();
            DateBind = exam.Date;
            selectedExam = exam;
            _reportPage = reportPage;
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            if(!comboBoxMedicine.Text.Equals("") && !textBoxDuration.Text.Equals("") && !textBoxPerDay.Text.Equals("")){
                string medicine = comboBoxMedicine.Text;
                int duration = Int32.Parse(textBoxDuration.Text);
                int perDay = Int32.Parse(textBoxPerDay.Text);
                bool prescription = (bool)checkBoxPrescription.IsChecked;

                Therapy therapy = new Therapy(selectedExam.Id, medicine, duration, perDay, prescription);
                _therapyController.NewTherapy(therapy);
                _reportPage = new ReportPage(selectedExam);
                NavigationService.Navigate(_reportPage);
            }
            
        }
    }
}
