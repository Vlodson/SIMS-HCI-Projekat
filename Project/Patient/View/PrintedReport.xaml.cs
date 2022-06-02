using Controller;
using Model;
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

namespace Patient.View
{
    /// <summary>
    /// Interaction logic for PrintedReport.xaml
    /// </summary>
    public partial class PrintedReport : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String start;
        private String end;
        private List<Examination> examinations;

        private ExamController _examinationController;

        public String Start
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

        public String End
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

        public List<Examination> Examinations
        {
            get
            {
                return examinations;
            }
            set
            {
                examinations = value;
                OnPropertyChanged("Examinations");
            }
        }
        public PrintedReport(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            App app = Application.Current as App;
            _examinationController = app.ExamController;

            Examinations = new List<Examination>();
            List<Examination> allExmainations = _examinationController.ReadPatientExams(Login.loggedId).ToList();
            
            foreach (Examination exam in allExmainations)
            {
                if (exam.Date < endDate && exam.Date > startDate)
                {
                    Examinations.Add(exam);
                }
            }

            Examinations.Sort((x, y) => DateTime.Compare(x.Date, y.Date));

            Start = startDate.ToString("dd.mm.yyyy.");
            End = endDate.ToString("dd.mm.yyyy.");

            stratLabel.Content = Start;
            endLabel.Content = End;
            examinationsTable.ItemsSource = Examinations;
        }
    }
}
