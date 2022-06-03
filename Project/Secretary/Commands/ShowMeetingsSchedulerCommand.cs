using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class ShowMeetingsSchedulerCommand : CommandBase
    {
        private HomeViewModel _homeViewModel;

        public ShowMeetingsSchedulerCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "Meetings")
            {
                _homeViewModel.CurrentHomeView = new HomePageMeetingsViewModel(_homeViewModel);
            }
        }
    }
}
