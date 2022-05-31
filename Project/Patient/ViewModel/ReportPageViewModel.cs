using Controller;
using Model;
using Patient.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patient.ViewModel
{
    internal class ReportPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public static DateTime startDate;
        public static DateTime endDate;
        private List<Examination> examinations;

        private ExamController _examinationController;

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
                Examinations = new List<Examination>();
                List<Examination> allExmainations = _examinationController.ReadPatientExams(Login.loggedId).ToList();
                foreach (Examination exam in allExmainations)
                {
                    if (exam.Date < endDate && exam.Date > startDate)
                    {
                        Examinations.Add(exam);
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
                Examinations = new List<Examination>();
                List<Examination> allExmainations = _examinationController.ReadPatientExams(Login.loggedId).ToList();
                foreach (Examination exam in allExmainations)
                {
                    if (exam.Date < endDate && exam.Date > startDate)
                    {
                        Examinations.Add(exam);
                    }
                }
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

        public ReportPageViewModel()
        {
            App app = Application.Current as App;
            _examinationController = app.ExamController;

            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(5);

            Examinations = new List<Examination>();
            List<Examination> allExmainations = _examinationController.ReadPatientExams(Login.loggedId).ToList();
            foreach(Examination exam in allExmainations)
            {
                if(exam.Date < endDate && exam.Date > startDate)
                {
                    Examinations.Add(exam);
                }
            }
        }
    }
}
