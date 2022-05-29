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
            idLbl.Content = _selectedMedicalRecord.ID;
            ucinLbl.Content = _selectedMedicalRecord.UCIN;
            nameLbl.Content = _selectedMedicalRecord.Name;
            surnameLbl.Content = _selectedMedicalRecord.Surname;
            phoneLbl.Content = _selectedMedicalRecord.PhoneNumber;
            emailLbl.Content = _selectedMedicalRecord.Mail;
            addressLbl.Content = _selectedMedicalRecord.Adress;
            GenderLbl.Content = _selectedMedicalRecord.Gender;
            dobLbl.Content = _selectedMedicalRecord.DoB;
            bloodLbl.Content = _selectedMedicalRecord.BloodType;
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
