using HospitalMain.Controller;
using HospitalMain.Model;
using Model;
using Secretary.ViewModel;
using Secretary.ViewUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class AddMeetingCommand : CommandBase
    {
        private MeetingController _meetingController;
        private AddMeetingViewModel _addMeetingViewModel;
        private MainViewModel _mainViewModel;

        public AddMeetingCommand(MeetingController meetingController, AddMeetingViewModel addMeetingViewModel, MainViewModel mainViewModel)
        {
            _meetingController = meetingController;
            _addMeetingViewModel = addMeetingViewModel;
            _mainViewModel = mainViewModel;

            _addMeetingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_addMeetingViewModel.MeetingTopic) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            int newMeetingID = _meetingController.generateID();

            foreach(SelectableItemWrapper<Doctor> doctor in _addMeetingViewModel.DoctorListBox)
            {
                if (doctor.IsSelected)
                {
                    _addMeetingViewModel.Doctors.Add(doctor.Item);
                }
            }

            Meeting newMeeting = new Meeting(newMeetingID.ToString(), _addMeetingViewModel.MeetingTopic, _addMeetingViewModel.Room.Id, _addMeetingViewModel.DateTime, _addMeetingViewModel.Doctors);
            _meetingController.BookNewMeeting(newMeeting);

            if(parameter.ToString() == "Add")
            {
                _mainViewModel.CurrentViewModel = new BookViewModel(_mainViewModel);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddMeetingViewModel.MeetingTopic))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
