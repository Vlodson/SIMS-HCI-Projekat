using Commands;
using Controller;
using Doctor;
using Doctor.View;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ViewModel
{
    public class ExcusesViewModel: ViewModelBase
    {
        private string patientBind;
        private DateTime dateBind;
        private string selectedFrom;
        private string selectedTo;
        private Examination selectedExam;
        private readonly PatientController _patientController;
        public MyICommand ExecuteCommand { get; set; }
        public ExcusesViewModel(Examination exam)
        {
            var app = System.Windows.Application.Current as App;
            _patientController = app.patientController;

            selectedExam = exam;
            patientBind = _patientController.ReadPatient(exam.PatientId).NameSurname;
            DateBind = exam.Date;
            ExecuteCommand = new MyICommand(OnExecute, CanExecute);
        }

        public string PatientBind { get => patientBind; set => patientBind = value; }
        public DateTime DateBind { get => dateBind; set => dateBind = value; }
        public string SelectedFrom
        {
            get { return selectedFrom; }
            set
            {
                if (selectedFrom != value)
                {
                    selectedFrom = value;
                    OnPropertyChanged("SelectedFrom");
                    ExecuteCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public string SelectedTo
        {
            get { return selectedTo; }
            set
            {
                if (selectedTo != value)
                {
                    selectedTo = value;
                    OnPropertyChanged("SelectedTo");
                    ExecuteCommand.RaiseCanExecuteChanged();
                }
            }
        }
        public bool CanExecute()
        {
            if (selectedFrom != null && selectedTo != null)
                return DateTime.Parse(selectedFrom) < DateTime.Parse(selectedTo)
                    && DateTime.Parse(selectedTo) > DateTime.Now
                    && DateTime.Parse(selectedFrom) > DateTime.Now;
            else
                return false;
        }
        public void OnExecute()
        {
            Excuses excuses = new Excuses(selectedExam.Id, DateTime.Parse(selectedFrom) , DateTime.Parse(selectedTo));
            ReportPage reportPage = new ReportPage(selectedExam);
            DoctorNavBar.navigation.Navigate(reportPage);
        }


    }
}
