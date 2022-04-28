using Secretary.View;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class GoToAddApointmentCommand : CommandBase
    {
        private readonly CRUDAppointmentsViewModel _crudAppointmentsViewModel;

        public GoToAddApointmentCommand(CRUDAppointmentsViewModel cRUDAppointmentsViewModel)
        {
            _crudAppointmentsViewModel = cRUDAppointmentsViewModel;
        }

        public override void Execute(object? parameter)
        {
            //unselect selected row
            _crudAppointmentsViewModel.ExaminationViewModel = null;
            
            AddApointment addApointment = new AddApointment();
            addApointment.DataContext = new AddAppointmentViewModel(_crudAppointmentsViewModel, addApointment);
            addApointment.ShowDialog();

        }
    }
}
