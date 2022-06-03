using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class GoToEditMeetingCommand : CommandBase
    {
        private readonly HomePageMeetingsViewModel _homePageMeetingsViewModel;
        private readonly HomeViewModel _homeViewModel;

        public GoToEditMeetingCommand(HomePageMeetingsViewModel homePageMeetingsViewModel, HomeViewModel homeViewModel)
        {
            _homePageMeetingsViewModel = homePageMeetingsViewModel;
            _homeViewModel = homeViewModel;

            _homePageMeetingsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            //!(_homePageMeetingsViewModel.SelectedMeeting == null) &&
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "EditMeeting")
            {
                _homeViewModel.CurrentHomeView = new EditMeetingViewModel(_homeViewModel, _homePageMeetingsViewModel);
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
