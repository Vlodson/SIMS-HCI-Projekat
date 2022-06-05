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
using HospitalMain.Enums;
using System.Windows;
using HospitalMain.Model;

namespace Secretary.Commands
{
    public class AddAccountCommand : CommandBase
    {
        private readonly AddAccountViewModel _addAccountViewModel;
        private readonly PatientController _patientController;
        private readonly CRUDAccountOptionsViewModel _cruDAccountOptionsViewModel;
        private readonly AccountsViewModel _accountsViewModel;

        public AddAccountCommand(AddAccountViewModel addAccountViewModel , CRUDAccountOptionsViewModel cRUDAccountOptionsViewModel , PatientController patientController, AccountsViewModel accountsViewModel)
        {
            _addAccountViewModel = addAccountViewModel;
            _cruDAccountOptionsViewModel = cRUDAccountOptionsViewModel;
            _patientController = patientController;
            _accountsViewModel = accountsViewModel;

            _addAccountViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_addAccountViewModel.UCIN) && !string.IsNullOrEmpty(_addAccountViewModel.Name) && !string.IsNullOrEmpty(_addAccountViewModel.Surname) && !string.IsNullOrEmpty(_addAccountViewModel.Adress) && !string.IsNullOrEmpty(_addAccountViewModel.Mail) && !string.IsNullOrEmpty(_addAccountViewModel.DateOfBirth.ToString()) && !string.IsNullOrEmpty(_addAccountViewModel.Gender.ToString()) && !string.IsNullOrEmpty(_addAccountViewModel.PhoneNumber) && !string.IsNullOrEmpty(_addAccountViewModel.MedicalRecordID) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            int newPatientID = _patientController.generateID();

            //pravljenje novog pacijenta
            Patient newPatient = new Patient(newPatientID.ToString(), _addAccountViewModel.UCIN, _addAccountViewModel.Name, _addAccountViewModel.Surname, _addAccountViewModel.PhoneNumber, _addAccountViewModel.Mail, _addAccountViewModel.Adress, _addAccountViewModel.Gender, Convert.ToDateTime(_addAccountViewModel.DateOfBirth), _addAccountViewModel.MedicalRecordID, false, new List<Answer>(), DateTime.Now.ToString("MM"), 0, 0);
            _patientController.CreatePatient(newPatient);

            //nepotrebno update jer se instancira novi viewModel CRUDAcc
            //update liste pacijenata
            UpdatePatients();

            if(parameter.ToString() == "Add")
            {
                _accountsViewModel.CurrentCRUDAccView = new CRUDAccountOptionsViewModel(_accountsViewModel);
            }
            //zatvaranje prozora
            //_addAccount.Close();
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
            if (e.PropertyName == nameof(AddAccountViewModel.UCIN) || e.PropertyName == nameof(AddAccountViewModel.Name) || e.PropertyName == nameof(AddAccountViewModel.Surname) || e.PropertyName == nameof(AddAccountViewModel.Adress) || e.PropertyName == nameof(AddAccountViewModel.Mail) || e.PropertyName == nameof(AddAccountViewModel.Gender) || e.PropertyName == nameof(AddAccountViewModel.PhoneNumber) || e.PropertyName == nameof(AddAccountViewModel.MedicalRecordID) || e.PropertyName == nameof(AddAccountViewModel.DateOfBirth))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
