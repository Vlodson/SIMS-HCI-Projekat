using Controller;
using Model;
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
    /// Interaction logic for GenerateReport.xaml
    /// </summary>
    public partial class GenerateReport : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string start;
        private string end;
        private string patient;
        private string dateTimeNow;
        private ObservableCollection<Report> reports;
        private ReportController _reportController;
        private ExamController _examController;
        public string DateTimeNow
        {
            get
            {
                return dateTimeNow;
            }
            set
            {
                dateTimeNow = value;
                OnPropertyChanged("DateTimeNow");
            }
        }
        public string Start
        {
            get
            {
                return start;
            }
            set
            {
                start = value;
                OnPropertyChanged("Start");
            }
        }
        public string Patient
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

        public string End
        {
            get
            {
                return end;
            }
            set
            {
                end = value;
                OnPropertyChanged("End");
            }
        }

        public ObservableCollection<Report> Reports
        {
            get
            {
                return reports;
            }
            set
            {
                reports = value;
                OnPropertyChanged("Reports");
            }
        }
        public GenerateReport(Patient patient, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            this.DataContext = this;
            App app = Application.Current as App;
            _reportController = app.reportController;
            _examController = app.examController;

            Reports = new ObservableCollection<Report>();
            ObservableCollection<Report> allReports = _reportController.findByPatientId(patient.ID);

            foreach (Report report in allReports)
            {
                if (_examController.GetExamination(report.ExaminationId).Date < endDate && _examController.GetExamination(report.ExaminationId).Date > startDate)
                {
                    Reports.Add(report);
                }
            }

            //Reports.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            Start = startDate.ToString();
            End = endDate.ToString();

            dateTimeNow = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
            patientLabel.Content = patient.NameSurname;
            startLabel.Content = Start;
            endLabel.Content = End;
            reportsTable.ItemsSource = Reports;
        }
    }
}
