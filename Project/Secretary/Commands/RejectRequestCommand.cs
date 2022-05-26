using Controller;
using Enums;
using Model;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class RejectRequestCommand : CommandBase
    {
        private FreeDaysRequestViewModel _freeDaysRequestViewModel;
        private FreeDaysRequestController _freeDaysRequestController;
        private MainViewModel _mainViewModel;

        public RejectRequestCommand(FreeDaysRequestViewModel freeDaysRequestViewModel, FreeDaysRequestController freeDaysRequestController, MainViewModel mainViewModel)
        {
            _freeDaysRequestViewModel = freeDaysRequestViewModel;
            _freeDaysRequestController = freeDaysRequestController;
            _mainViewModel = mainViewModel;

            _freeDaysRequestViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_freeDaysRequestViewModel.RejectionReason) && (_freeDaysRequestViewModel.FreeDaysRequest != null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            FreeDaysRequest request = new FreeDaysRequest(_freeDaysRequestViewModel.FreeDaysRequest.ID, StatusEnum.Rejected, _freeDaysRequestViewModel.FreeDaysRequest.DoctorID, _freeDaysRequestViewModel.FreeDaysRequest.StartDate, _freeDaysRequestViewModel.FreeDaysRequest.EndDate, _freeDaysRequestViewModel.FreeDaysRequest.Reason, _freeDaysRequestViewModel.RejectionReason);
            _freeDaysRequestController.EditRequestStatus(request);

            if (parameter.ToString() == "Reject")
            {
                _mainViewModel.CurrentViewModel = new RequestsViewModel(_mainViewModel);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FreeDaysRequestViewModel.FreeDaysRequest) || e.PropertyName == nameof(FreeDaysRequestViewModel.RejectionReason))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
