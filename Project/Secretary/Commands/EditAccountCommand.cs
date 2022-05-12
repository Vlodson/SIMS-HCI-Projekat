using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller;
using Secretary.ViewModel;
using System.Collections.ObjectModel;
using Model;
using System.ComponentModel;
using System.Windows;
using HospitalMain.Enums;

namespace Secretary.Commands
{
    public class EditAccountCommand : CommandBase
    {
        private readonly PatientController _patientController;
        private readonly CRUDAccountOptionsViewModel _cruDAccountOptionsViewModel;
        private readonly EditAccountViewModel _editAccountViewModel;
        private readonly AccountsViewModel _accountsViewModel;

        public EditAccountCommand(EditAccountViewModel editAccountViewModel, CRUDAccountOptionsViewModel cRUDAccountOptionsViewModel, PatientController patientController, AccountsViewModel accountsViewModel)
        {
            _cruDAccountOptionsViewModel = cRUDAccountOptionsViewModel;
            _patientController = patientController;
            _editAccountViewModel = editAccountViewModel;
            _accountsViewModel = accountsViewModel;

            _editAccountViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_editAccountViewModel.Name) && !string.IsNullOrEmpty(_editAccountViewModel.Surname) && !string.IsNullOrEmpty(_editAccountViewModel.UCIN) && !string.IsNullOrEmpty(_editAccountViewModel.Adress) && !string.IsNullOrEmpty(_editAccountViewModel.Mail) && !string.IsNullOrEmpty(_editAccountViewModel.Gender.ToString()) && !string.IsNullOrEmpty(_editAccountViewModel.PhoneNumber) && !string.IsNullOrEmpty(_editAccountViewModel.MedicalRecordID) && !string.IsNullOrEmpty(_editAccountViewModel.DateOfBirth.ToString()) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            //izmena pacijenta
            _patientController.EditPatient(_editAccountViewModel.ID, _editAccountViewModel.UCIN, _editAccountViewModel.Name, _editAccountViewModel.Surname, _editAccountViewModel.PhoneNumber, _editAccountViewModel.Mail, _editAccountViewModel.Adress, _editAccountViewModel.Gender, Convert.ToDateTime(_editAccountViewModel.DateOfBirth), _editAccountViewModel.MedicalRecordID);

            //update liste pacijenata
            UpdatePatients();
        
            if(parameter.ToString() == "Edit")
            {
                _accountsViewModel.CurrentCRUDAccView = new CRUDAccountOptionsViewModel(_accountsViewModel);
            }

            //zatvaranje prozora
            //_editAccount.Close();
        }

        private void UpdatePatients()
        {
            _cruDAccountOptionsViewModel.PatientList.Clear();
            ObservableCollection<Patient> patientsFromBase = _patientController.ReadAllPatients();

            foreach (Patient patient in patientsFromBase)
            {
                _cruDAccountOptionsViewModel.PatientList.Add(new PatientViewModel(patient));
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditAccountViewModel.Name) || e.PropertyName == nameof(EditAccountViewModel.Surname) || e.PropertyName == nameof(EditAccountViewModel.UCIN) || e.PropertyName == nameof(EditAccountViewModel.Adress) || e.PropertyName == nameof(EditAccountViewModel.Mail) || e.PropertyName == nameof(EditAccountViewModel.PhoneNumber) || e.PropertyName == nameof(EditAccountViewModel.Gender) || e.PropertyName == nameof(EditAccountViewModel.DateOfBirth) || e.PropertyName == nameof(EditAccountViewModel.MedicalRecordID))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
