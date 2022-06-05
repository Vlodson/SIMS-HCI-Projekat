using HospitalMain.Controller;
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
    public class EditMeetingCommand : CommandBase
    {
        private MeetingController _meetingController;
        private EditMeetingViewModel _editMeetingViewModel;
        private HomeViewModel _homeViewModel;
        private HomePageMeetingsViewModel _homePageMeetingsViewModel;

        public EditMeetingCommand(MeetingController meetingController, EditMeetingViewModel editMeetingViewModel, HomeViewModel homeViewModel, HomePageMeetingsViewModel homePageMeetingsViewModel)
        {
            _meetingController = meetingController;
            _editMeetingViewModel = editMeetingViewModel;
            _homeViewModel = homeViewModel;
            _homePageMeetingsViewModel = homePageMeetingsViewModel;

            _editMeetingViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            //vrv treba neki uslov dodatno
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            foreach (SelectableItemWrapper<Doctor> doctor in _editMeetingViewModel.DoctorListBox)
            {
                if (doctor.IsSelected)
                {
                    _editMeetingViewModel.Doctors.Add(doctor.Item);
                }
            }

            //logika iz beka

            if (parameter.ToString() == "Edit")
            {
                _homeViewModel.CurrentHomeView = new HomePageMeetingsViewModel(_homeViewModel);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditMeetingViewModel.MeetingTopic))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
