using HospitalMain.Controller;
using HospitalMain.Model;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class DeleteMeetingCommand : CommandBase
    {
        private readonly HomePageMeetingsViewModel _homePageMeetingsViewModel;
        private readonly MeetingController _meetingsController;

        public DeleteMeetingCommand(HomePageMeetingsViewModel homePageMeetingsViewModel, MeetingController meetingController)
        {
            _homePageMeetingsViewModel = homePageMeetingsViewModel;
            _meetingsController = meetingController;

            _homePageMeetingsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_homePageMeetingsViewModel.SelectedMeeting == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _meetingsController.DeleteMeeting(_homePageMeetingsViewModel.SelectedMeeting.ID);
            UpdateMeetings();
        }

        private void UpdateMeetings()
        {
            _homePageMeetingsViewModel.Meetings.Clear();
            ObservableCollection<Meeting> meetingsFromBase = _meetingsController.GetAllMeetings();
            foreach(Meeting meeting in meetingsFromBase)
            {
                _homePageMeetingsViewModel.Meetings.Add(new MeetingViewModel(meeting));
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(HomePageMeetingsViewModel.SelectedMeeting))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
