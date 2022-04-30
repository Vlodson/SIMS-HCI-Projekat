using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using Model;
using System.Collections.ObjectModel;
using Repository;
using System.IO;
using HospitalMain.Enums;
using System.Windows.Input;
using Secretary.Commands;

namespace Secretary.ViewModel
{
    public class CRUDAccountOptionsViewModel : ViewModelBase
    {

        private readonly PatientController _patientController;
        private ObservableCollection<PatientViewModel> _patientList;

        public ObservableCollection<PatientViewModel> PatientList => _patientList;

        private PatientViewModel _patientViewModel;
        public PatientViewModel PatientViewModel
        {
            get { return _patientViewModel; }
            set { _patientViewModel = value; OnPropertyChanged(nameof(PatientViewModel));  }
        }

        public ICommand AddAccountCommand { get; }
        public ICommand DeleteAccountCommand { get; }
        public ICommand EditAccountCommand { get; }

        public CRUDAccountOptionsViewModel()
        {
            
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;

            _patientList = new ObservableCollection<PatientViewModel>();

            AddAccountCommand = new GoToAddAccountCommand(this);
            DeleteAccountCommand = new DeleteAccountCommand(this, _patientController);
            EditAccountCommand = new GoToEditAccountCommand(this);

            ObservableCollection<Patient> patientsFromBase = _patientController.ReadAllPatients();
            foreach(Patient patient in patientsFromBase)
            {
                _patientList.Add(new PatientViewModel(patient));
            }
        }

    }
}
