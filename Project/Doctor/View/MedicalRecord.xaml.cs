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
    /// Interaction logic for MedicalRecord.xaml
    /// </summary>
    public partial class MedicalRecord : Page
    {
        private ReportRepo _reportRepo;
        private MedicalRecordRepo _medicalRecordRepo;
        private ReportController _reportController;
        private MedicalRecordController _medicalRecordController;
        public static ObservableCollection<Report> reports { get; set; }
        public Model.MedicalRecord _selectedMedicalRecord { get; set; }
        public Report _selectedReport { get; set; } 
        public MedicalRecord(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            

            App app = Application.Current as App;
            _reportRepo = app.reportRepo;
            _reportController = app.reportController;
            _medicalRecordController = app.medicalRecordController;
            _medicalRecordRepo = app.medicalRecordRepo;

            if (File.Exists(_reportRepo.DBPath))
                _reportRepo.LoadReport();
            if(File.Exists(_medicalRecordRepo.DBPath))
                _medicalRecordRepo.LoadMedicalRecord();

            _selectedMedicalRecord = _medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);
            reports = _reportController.findByPatientId(patient.ID);
            idLbl.Text = _selectedMedicalRecord.ID;
            ucinLbl.Text = _selectedMedicalRecord.UCIN;
            nameLbl.Text = _selectedMedicalRecord.Name;
            surnameLbl.Text = _selectedMedicalRecord.Surname;
            phoneLbl.Text = _selectedMedicalRecord.PhoneNumber;
            emailLbl.Text = _selectedMedicalRecord.Mail;
            addressLbl.Text = _selectedMedicalRecord.Adress;
            genderLbl.Text = _selectedMedicalRecord.Gender.ToString();
            dobLbl.Text = _selectedMedicalRecord.DoB.ToString();
            bloodLbl.Text = _selectedMedicalRecord.BloodType.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _selectedReport = (Report)dataGridReports.SelectedItem;
            _selectedReport.Description = txtDescription.Text;
            _reportRepo.SaveReport();
            NavigationService.Refresh();
        }
    }
}
