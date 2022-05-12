using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secretary.ViewModel;

namespace Secretary.Commands
{
    public class CancelAccountCommand : CommandBase
    {
        private readonly AccountsViewModel _accountsViewModel;

        public CancelAccountCommand(AccountsViewModel accountsViewModel)
        {
            _accountsViewModel = accountsViewModel;
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "Cancel")
            {
                _accountsViewModel.CurrentCRUDAccView = new CRUDAccountOptionsViewModel(_accountsViewModel);
            }
        }
    }
}
