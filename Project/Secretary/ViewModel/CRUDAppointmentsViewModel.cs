using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using HospitalMain.Model;
using Model;
using System.Windows.Input;
using Controller;
using Secretary.Commands;

namespace Secretary.ViewModel
{
    public class CRUDAppointmentsViewModel : ViewModelBase
    {

        private ObservableCollection<ExaminationViewModel> examinationList;
        private readonly ExamController examController;

        public ObservableCollection<ExaminationViewModel> ExaminationList => examinationList;

        private ExaminationViewModel examinationViewModel;
        public ExaminationViewModel ExaminationViewModel
        {
            get { return examinationViewModel; }
            set { examinationViewModel = value; OnPropertyChanged(nameof(ExaminationViewModel)); }

        }

        public ICommand AddAppointmentCommand { get; }
        public ICommand EditAppointmentCommand { get; }
        public ICommand DeleteAppointmentCommand { get; }
        public ICommand BackCommand { get; }

        public CRUDAppointmentsViewModel()
        {
            var app = System.Windows.Application.Current as App;
            examController = app.ExamController;

            examinationList = new ObservableCollection<ExaminationViewModel>();

            //AddAppointmentCommand = new GoToAddApointmentCommand(this);
            //DeleteAppointmentCommand = new DeleteAppointmentCommand(this, examController);
            //EditAppointmentCommand = new GoToEditAppointmentCommand(this);

            ObservableCollection<Examination> examinationsFromBase = examController.getAllExaminations();
            foreach(Examination examination in examinationsFromBase)
            {
                examinationList.Add(new ExaminationViewModel(examination));
            }

        }



    }
}
