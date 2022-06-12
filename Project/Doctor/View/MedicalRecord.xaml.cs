using Controller;
using iTextSharp.text.pdf;
using Model;
using Repository;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Doctor.View
{
    /// <summary>
    /// Interaction logic for MedicalRecord.xaml
    /// </summary>
    public partial class MedicalRecord : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private ReportRepo _reportRepo;
        private MedicalRecordRepo _medicalRecordRepo;
        private ReportController _reportController;
        private MedicalRecordController _medicalRecordController;
        private ExamController _examController;
        public static ObservableCollection<Report> Reports { get; set; }
        public static DateTime startDate;
        public static DateTime endDate;
        private Patient patient;
        private PrintDialog _printDialog = new PrintDialog();
        public Model.MedicalRecord _selectedMedicalRecord { get; set; }
        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
                Reports = new ObservableCollection<Report>();
                ObservableCollection<Report> allReports = _reportController.findByPatientId(_selectedMedicalRecord.ID);
                foreach (Report report in allReports)
                {
                    if (_examController.GetExamination(report.ExaminationId).Date < endDate && _examController.GetExamination(report.ExaminationId).Date > startDate)
                    {
                        Reports.Add(report);
                    }
                }
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
                OnPropertyChanged("EndDate");
                Reports = new ObservableCollection<Report>();
                ObservableCollection<Report> allReports = _reportController.findByPatientId(_selectedMedicalRecord.ID);
                foreach (Report report in allReports)
                {
                    if (_examController.GetExamination(report.ExaminationId).Date < endDate && _examController.GetExamination(report.ExaminationId).Date > startDate)
                    {
                        Reports.Add(report);
                    }
                }
            }
        }
        public MedicalRecord(Patient patient)
        {
            InitializeComponent();
            this.DataContext = this;
            this.patient = patient;

            App app = Application.Current as App;
            _reportRepo = app.reportRepo;
            _reportController = app.reportController;
            _medicalRecordController = app.medicalRecordController;
            _medicalRecordRepo = app.medicalRecordRepo;
            _examController = app.examController;

            if (File.Exists(_reportRepo.DBPath))
                _reportRepo.LoadReport();
            if(File.Exists(_medicalRecordRepo.DBPath))
                _medicalRecordRepo.LoadMedicalRecord();

            _selectedMedicalRecord = _medicalRecordController.GetMedicalRecord(patient.MedicalRecordID);
            Reports = _reportController.findByPatientId(patient.ID);
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

        private void Generate_Click(object sender, RoutedEventArgs e)
        {
            GenerateReport generateReport = new GenerateReport(Patient, StartDate, EndDate);
            _printDialog.PrintVisual(generateReport, "Izvestaj");
        }
    }
}
