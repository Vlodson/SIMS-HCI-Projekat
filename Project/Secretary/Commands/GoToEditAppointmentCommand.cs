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
        private readonly HomePageViewModel _homePageViewModel;
        private readonly HomeViewModel _homeViewModel;

        public GoToEditAppointmentCommand(HomePageViewModel homePageViewModel, HomeViewModel homeViewModel)
        {
            _homePageViewModel = homePageViewModel;
            _homeViewModel = homeViewModel;

            _homePageViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            //!(_homePageViewModel.SelectedExamination == null) &&
            return  base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "EditAppointment")
            {
                _homeViewModel.CurrentHomeView = new EditAppointmentViewModel(_homeViewModel, _homePageViewModel);
            }

            //EditAppointment editAppointment = new EditAppointment();
            //editAppointment.DataContext = new EditAppointmentViewModel(_crudAppointmentsViewModel, editAppointment);
            //editAppointment.ShowDialog();
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(HomePageViewModel.SelectedExamination))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
