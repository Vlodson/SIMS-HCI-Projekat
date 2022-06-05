using Controller;
using Model;
using Secretary.Commands;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class HomePageViewModel : ViewModelBase
    {

        private ExamController _examController;
        private ObservableCollection<ExaminationViewModel> _examinationList;

        public ObservableCollection<ExaminationViewModel> ExaminationList
        {
            get { return _examinationList; }
            set { _examinationList = value; OnPropertyChanged(nameof(ExaminationList)); }
        }

        private ExaminationViewModel _selectedExamination;
        public ExaminationViewModel SelectedExamination
        {
            get { return _selectedExamination; }
            set { _selectedExamination = value; OnPropertyChanged(nameof(SelectedExamination));}
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set { _selectedDate = value; OnPropertyChanged(nameof(SelectedDate)); }
        }

        public ICommand EditAppointmentCommand { get; }
        public ICommand DeleteAppointmentCommand { get; }

        public ICommand ShowMeetingsCommand { get; }
        public ICommand ShowAppointmentsCommand { get; }

        public HomePageViewModel(HomeViewModel homeViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _examController = app.ExamController;

            _examinationList = new ObservableCollection<ExaminationViewModel>();

            EditAppointmentCommand = new GoToEditAppointmentCommand(this, homeViewModel);
            //DeleteAppointmentCommand = new DeleteAppointmentCommand(this, _examController);
            ShowAppointmentsCommand = new ShowAppointmentsSchedulerCommand(homeViewModel);
            ShowMeetingsCommand = new ShowMeetingsSchedulerCommand(homeViewModel);

            //ObservableCollection<Examination> examinationsFromBase = _examController.getAllExaminations();
            //foreach (Examination examination in examinationsFromBase)
            //{
            //    _examinationList.Add(new ExaminationViewModel(examination));
            //}
        }

    }
}
