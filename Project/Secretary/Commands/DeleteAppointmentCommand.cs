using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Secretary.View;
using Secretary.ViewModel;
using Controller;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Secretary.Commands
{

    public class DeleteAppointmentCommand : CommandBase
    {
        private readonly CRUDAppointmentsViewModel _crudAppointmentsViewModel;
        private readonly ExamController _examController;

        public DeleteAppointmentCommand(CRUDAppointmentsViewModel cRUDAppointmentsViewModel, ExamController examController)
        {
            _crudAppointmentsViewModel = cRUDAppointmentsViewModel;
            _examController = examController;

            _crudAppointmentsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_crudAppointmentsViewModel.ExaminationViewModel == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Examination exam = _examController.getExamination(_crudAppointmentsViewModel.ExaminationViewModel.ID);

           _examController.RemoveExam(exam);
            UpdateExaminations();
        }

        private void UpdateExaminations()
        {
            _crudAppointmentsViewModel.ExaminationList.Clear();
            ObservableCollection<Examination> examinationsFromBase = _examController.getAllExaminations();

            foreach(Examination examination in examinationsFromBase)
            {
                _crudAppointmentsViewModel.ExaminationList.Add(new ExaminationViewModel(examination));
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(CRUDAppointmentsViewModel.ExaminationViewModel))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
