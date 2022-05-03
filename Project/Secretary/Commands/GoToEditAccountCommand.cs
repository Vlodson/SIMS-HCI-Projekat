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

        public GoToEditAccountCommand(CRUDAccountOptionsViewModel cRUDAccountOptionsViewModel)
        {
            _cRUDAccountOptionsViewModel = cRUDAccountOptionsViewModel;

            _cRUDAccountOptionsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_cRUDAccountOptionsViewModel.PatientViewModel == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            EditAccount editAccount = new EditAccount();
            editAccount.DataContext = new EditAccountViewModel(_cRUDAccountOptionsViewModel, editAccount);
            editAccount.ShowDialog();
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
