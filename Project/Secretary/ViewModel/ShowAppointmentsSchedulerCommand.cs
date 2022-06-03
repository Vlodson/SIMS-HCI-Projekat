using Secretary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class ShowAppointmentsSchedulerCommand : CommandBase
    {
        private HomeViewModel _homeViewModel;

        public ShowAppointmentsSchedulerCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "Appointments")
            {
                _homeViewModel.CurrentHomeView = new HomePageViewModel(_homeViewModel);
            }
        }
    }
}
