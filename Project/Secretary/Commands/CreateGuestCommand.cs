using Controller;
using HospitalMain.Model;
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
    public class CreateGuestCommand : CommandBase
    {
        private readonly CreateGuestViewModel _createGuestViewModel;
        private readonly PatientController _patientController;
        private readonly EmergencyGeneralViewModel _emergencyGeneralViewModel;

        public CreateGuestCommand(CreateGuestViewModel createGuestViewModel, PatientController patientController, EmergencyGeneralViewModel emergencyGeneralViewModel)
        {
            _createGuestViewModel = createGuestViewModel;
            _patientController = patientController;
            _emergencyGeneralViewModel = emergencyGeneralViewModel;

            _createGuestViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_createGuestViewModel.Name) && !string.IsNullOrEmpty(_createGuestViewModel.Surname) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            int newGuestID = _patientController.generateID();

            //pravljenje guest-a
            Patient patient = new Patient(newGuestID.ToString(), "", _createGuestViewModel.Name, _createGuestViewModel.Surname, "", "", "", _createGuestViewModel.Gender, new DateTime(), "", true, new List<Answer>(), DateTime.Now.ToString("MM"), 0, 0);
            _patientController.CreateGuest(patient);

            if(parameter.ToString() == "CreateGuest")
            {
                _emergencyGeneralViewModel.CurrentEmergencyView = new EmergencyViewModel(_emergencyGeneralViewModel);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreateGuestViewModel.Name) || e.PropertyName == nameof(CreateGuestViewModel.Surname))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
