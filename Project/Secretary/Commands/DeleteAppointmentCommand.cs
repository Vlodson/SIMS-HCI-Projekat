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
        private readonly HomePageViewModel _homePageViewModel;
        private readonly ExamController _examController;

        public DeleteAppointmentCommand(HomePageViewModel homePageViewModel, ExamController examController)
        {
            _homePageViewModel = homePageViewModel;
            _examController = examController;

            _homePageViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_homePageViewModel.SelectedExamination == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            Examination exam = _examController.getExamination(_homePageViewModel.SelectedExamination.ID);

            //Examination exam = _examController.GetExamByTime(_homePageViewModel.SelectedDate);
            
           _examController.RemoveExam(exam);
            UpdateExaminations();
        }

        private void UpdateExaminations()
        {
            _homePageViewModel.ExaminationList.Clear();
            ObservableCollection<Examination> examinationsFromBase = _examController.getAllExaminations();

            foreach(Examination examination in examinationsFromBase)
            {
                _homePageViewModel.ExaminationList.Add(new ExaminationViewModel(examination));
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
