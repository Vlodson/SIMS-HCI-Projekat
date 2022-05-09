using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secretary.View;
using Secretary.ViewModel;

namespace Secretary.Commands
{
    public class GoToEditAccountCommand : CommandBase
    {
        private readonly CRUDAccountOptionsViewModel _cRUDAccountOptionsViewModel;
        private readonly AccountsViewModel _accountsViewModel;

        public GoToEditAccountCommand(CRUDAccountOptionsViewModel cRUDAccountOptionsViewModel, AccountsViewModel accountsViewModel)
        {
            _cRUDAccountOptionsViewModel = cRUDAccountOptionsViewModel;
            _accountsViewModel = accountsViewModel;

            _cRUDAccountOptionsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_cRUDAccountOptionsViewModel.PatientViewModel == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "EditPatientUser")
            {
                _accountsViewModel.CurrentCRUDAccView = new EditAccountViewModel(_cRUDAccountOptionsViewModel, _accountsViewModel);
            }

            //EditAccount editAccount = new EditAccount();
            //editAccount.DataContext = new EditAccountViewModel(_cRUDAccountOptionsViewModel, editAccount);
            //editAccount.ShowDialog();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CRUDAccountOptionsViewModel.PatientViewModel))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
