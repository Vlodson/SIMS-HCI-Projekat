using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class GoToCreateGuestFormCommand : CommandBase
    {
        private EmergencyViewModel _emergencyViewModel;
        private EmergencyGeneralViewModel _emergencyGeneralViewModel;

        public GoToCreateGuestFormCommand(EmergencyViewModel emergencyViewModel, EmergencyGeneralViewModel emergencyGeneralViewModel)
        {
            _emergencyViewModel = emergencyViewModel;
            _emergencyGeneralViewModel = emergencyGeneralViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter.ToString() == "CreateGuest")
            {
                _emergencyGeneralViewModel.CurrentEmergencyView = new CreateGuestViewModel(_emergencyGeneralViewModel);
            }
        }
    }
}
