﻿using Controller;
using Enums;
using Model;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class ApproveRequestCommand : CommandBase
    {
        private FreeDaysRequestViewModel _freeDaysRequestViewModel;
        private FreeDaysRequestController _freeDaysRequestController;
        private MainViewModel _mainViewModel;

        public ApproveRequestCommand(FreeDaysRequestViewModel freeDaysRequestViewModel, FreeDaysRequestController freeDaysRequestController, MainViewModel mainViewModel)
        {
            _freeDaysRequestViewModel = freeDaysRequestViewModel;
            _freeDaysRequestController = freeDaysRequestController;
            _mainViewModel = mainViewModel;

            _freeDaysRequestViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            int days = Convert.ToInt32((_freeDaysRequestViewModel.FreeDaysRequest.EndDate.Date - _freeDaysRequestViewModel.FreeDaysRequest.StartDate.Date).TotalDays);
            return _freeDaysRequestController.CheckIfDoctorHasFreeDays(_freeDaysRequestViewModel.FreeDaysRequest.DoctorID, days) && (_freeDaysRequestViewModel.FreeDaysRequest != null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            //promeniti status zahteva koji je pikovan
            FreeDaysRequest request = new FreeDaysRequest(_freeDaysRequestViewModel.FreeDaysRequest.ID, StatusEnum.Approved, _freeDaysRequestViewModel.FreeDaysRequest.DoctorID, _freeDaysRequestViewModel.FreeDaysRequest.StartDate, _freeDaysRequestViewModel.FreeDaysRequest.EndDate, _freeDaysRequestViewModel.FreeDaysRequest.Reason, _freeDaysRequestViewModel.RejectionReason);
            _freeDaysRequestController.EditRequestStatus(request);

            //update tabele
            if(parameter.ToString() == "Approve")
            {
                _mainViewModel.CurrentViewModel = new RequestsViewModel(_mainViewModel);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(FreeDaysRequestViewModel.FreeDaysRequest))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
