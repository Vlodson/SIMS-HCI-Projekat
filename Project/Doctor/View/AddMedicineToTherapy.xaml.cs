using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
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
        private Examination selectedExam;
        private int dur;
        private int perDay;

        private TherapyController _therapyController;
        private TherapyRepo _therapyRepo;
        private ReportPage _reportPage;

        public string PatientBind { get => patientBind; set => patientBind = value; }
        public DateTime DateBind { get => dateBind; set => dateBind = value; }
        public Examination SelectedExam { get => selectedExam; set => selectedExam = value; }

        public int PerDay
        {
            get
            {
                return perDay;
            }
            set
            {
                perDay = value;
                OnPropertyChanged("PerDay");
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

        public AddMedicineToTherapy(ReportPage reportPage, Examination exam)
        {
            InitializeComponent();
            this.DataContext = this;
            this.PatientBind = exam.PatientId;
            this.DateBind = exam.Date;
            this.selectedExam = exam;
            _reportPage = reportPage;

            App app = Application.Current as App;
            _therapyController = app.therapyController;
            _therapyRepo = app.therapyRepo;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string medicine = comboBoxMedicine.Text;
            int duration = Int32.Parse(textBoxDuration.Text);
            int perDay = Int32.Parse(textBoxPerDay.Text);
            bool prescription = (bool)checkBoxPrescription.IsChecked;

            Therapy therapy = new Therapy(selectedExam.Id, medicine, duration, perDay, prescription);
            _therapyController.NewTherapy(therapy);
            _therapyRepo.SaveTherapy();
            _reportPage = new ReportPage(selectedExam);
            NavigationService.Navigate(_reportPage);
        }
    }
}
