using Controller;
using Model;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class ShowSuggestedEACommand : CommandBase
    {
        private readonly EmergencyViewModel _emergencyViewModel;
        private readonly DoctorController _doctorController;

        public ShowSuggestedEACommand(EmergencyViewModel emergencyViewModel, DoctorController doctorController)
        {
            _emergencyViewModel = emergencyViewModel;
            _doctorController = doctorController;
        }

        public override void Execute(object? parameter)
        {
            _emergencyViewModel.SuggestedAppointments.Clear();

            ObservableCollection<Examination> suggestedExams = _doctorController.GetFreeExaminations(_emergencyViewModel.DateTime, _emergencyViewModel.DateTime.AddHours(3), _emergencyViewModel.DoctorType);

            foreach (Examination exam in suggestedExams)
            {
                _emergencyViewModel.SuggestedAppointments.Add(new ExaminationViewModel(exam));
            }

        }

    }
}
