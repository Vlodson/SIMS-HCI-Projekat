using HospitalMain.Controller;
using HospitalMain.Model;
using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Secretary.ViewModel
{
    public class HomePageMeetingsViewModel : ViewModelBase
    {
        private MeetingController _meetingsController;
        private ObservableCollection<MeetingViewModel> _meetings;

        public ObservableCollection<MeetingViewModel> Meetings
        {
            get { return _meetings; }
            set { _meetings = value; OnPropertyChanged(nameof(Meetings)); }
        }

        private MeetingViewModel _selectedMeeting;
        public MeetingViewModel SelectedMeeting
        {
            get { return _selectedMeeting; }
            set { _selectedMeeting = value; OnPropertyChanged(nameof(SelectedMeeting)); }
        }

        public ICommand EditMeetingCommand { get; }
        public ICommand DeleteMeetingCommand { get; }

        public ICommand ShowMeetingsCommand { get; }
        public ICommand ShowAppointmentsCommand { get; }

        public HomePageMeetingsViewModel(HomeViewModel homeViewModel)
        {
            var app = System.Windows.Application.Current as App;
            _meetingsController = app.MeetingController;

            _meetings = new ObservableCollection<MeetingViewModel>();

            EditMeetingCommand = new GoToEditMeetingCommand(this, homeViewModel);
            //DeleteMeetingCommand = DeleteMeetingCommand(this, _meetingsController);
            ShowAppointmentsCommand = new ShowAppointmentsSchedulerCommand(homeViewModel);
            ShowMeetingsCommand = new ShowMeetingsSchedulerCommand(homeViewModel);

            ObservableCollection<Meeting> meetingsFromBase = _meetingsController.GetAllMeetings();
            foreach (Meeting meeting in meetingsFromBase)
            {
                _meetings.Add(new MeetingViewModel(meeting));
            }
        }
    }
}
