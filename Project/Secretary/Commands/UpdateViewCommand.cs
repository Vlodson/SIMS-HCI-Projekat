using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class UpdateViewCommand : CommandBase
    {
        private MainViewModel _mainViewModel;

        public UpdateViewCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "Appointments")
            {
                _mainViewModel.CurrentViewModel = new CRUDAppointmentsViewModel();
            } 
            else if(parameter.ToString() == "UserAccounts")
            {
                _mainViewModel.CurrentViewModel = new CRUDAccountOptionsViewModel();
            } 
            else if (parameter.ToString() == "MedicalRecords")
            {
                _mainViewModel.CurrentViewModel = new CRUDMedicalRecordViewModel();
            }
        }
    }
}
