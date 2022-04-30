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

        public GoToAddAccountCommand(CRUDAccountOptionsViewModel cRUDAccountOptionsViewModel)
        {
            _cRUDAccountOptionsViewModel = cRUDAccountOptionsViewModel;
        }

        public override void Execute(object? parameter)
        {
            _cRUDAccountOptionsViewModel.PatientViewModel = null;

            AddAccount addAccount = new AddAccount();
            addAccount.DataContext = new AddAccountViewModel(_cRUDAccountOptionsViewModel, addAccount);
            addAccount.ShowDialog();
        }
    }
}
