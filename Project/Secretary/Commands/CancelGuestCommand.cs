using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class CancelGuestCommand : CommandBase
    {
        //private readonly CreateGuestViewModel _createGuestViewModel;
        private readonly EmergencyGeneralViewModel _emergencyGeneralViewModel;

        public CancelGuestCommand(EmergencyGeneralViewModel emergencyGeneralViewModel)
        {
            //_createGuestViewModel = createGuestViewModel;
            _emergencyGeneralViewModel = emergencyGeneralViewModel;
        }

        public override void Execute(object? parameter)
        {
            if (parameter.ToString() == "Cancel")
            {
                _emergencyGeneralViewModel.CurrentEmergencyView = new EmergencyViewModel(_emergencyGeneralViewModel);
            }
        }
    }
}
