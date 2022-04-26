using Secretary.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secretary.ViewModel;
using System.ComponentModel;

namespace Secretary.Commands
{
    public class GoToEditAppointmentCommand : CommandBase
    {
        private readonly CRUDAppointmentsViewModel _crudAppointmentsViewModel;
        public GoToEditAppointmentCommand(CRUDAppointmentsViewModel cRUDAppointmentsViewModel)
        {
            _crudAppointmentsViewModel = cRUDAppointmentsViewModel;

            _crudAppointmentsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_crudAppointmentsViewModel.ExaminationViewModel == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            EditAppointment editAppointment = new EditAppointment();
            editAppointment.DataContext = new EditAppointmentViewModel(_crudAppointmentsViewModel, editAppointment);
            editAppointment.ShowDialog();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CRUDAppointmentsViewModel.ExaminationViewModel))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
