using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secretary.View;
using Secretary.ViewModel;

namespace Secretary.Commands
{
    public class GoToAddAccountCommand : CommandBase
    {
        private readonly CRUDAccountOptionsViewModel _cRUDAccountOptionsViewModel;
        private readonly AccountsViewModel _accountsViewModel;

        public GoToAddAccountCommand(CRUDAccountOptionsViewModel cRUDAccountOptionsViewModel, AccountsViewModel accountsViewModel)
        {
            _cRUDAccountOptionsViewModel = cRUDAccountOptionsViewModel;
            _accountsViewModel = accountsViewModel;
        }

        public override void Execute(object? parameter)
        {
            _cRUDAccountOptionsViewModel.PatientViewModel = null;

            if(parameter.ToString() == "AddPatientUser")
            {
                _accountsViewModel.CurrentCRUDAccView = new AddAccountViewModel(_cRUDAccountOptionsViewModel, _accountsViewModel);
            }

            //AddAccount addAccount = new AddAccount();
            //addAccount.DataContext = new AddAccountViewModel(_cRUDAccountOptionsViewModel);
            //addAccount.ShowDialog();
        }
    }
}
