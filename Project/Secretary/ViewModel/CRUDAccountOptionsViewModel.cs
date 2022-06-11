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
using System.ComponentModel;
using System.Windows.Data;

namespace Secretary.ViewModel
{
    public class CRUDAccountOptionsViewModel : ViewModelBase
    {

        private readonly PatientController _patientController;
        private readonly MedicalRecordController _medicalRecordController;
        private readonly ExamController _examController;
        private ObservableCollection<PatientViewModel> _patientList;
        private ICollectionView _dataGridCollection;
        private String _filter;

        public ICollectionView DataGridCollection
        {
            get { return _dataGridCollection; }
            set { _dataGridCollection = value; OnPropertyChanged(nameof(DataGridCollection)); }
        }

        public String Filter
        {
            get { return _filter; }
            set { _filter = value; OnPropertyChanged(nameof(Filter)); DataGridCollection.Filter = FilterByNameSurnameOrID; }
        }

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

        private bool FilterByNameSurnameOrID(object pat)
        {
            if (!string.IsNullOrEmpty(Filter))
            {
                var data = pat as PatientViewModel;
                return data != null && (data.Name.ToLower().Contains(Filter.ToLower()) || data.Surname.ToLower().Contains(Filter.ToLower()) || data.ID.Contains(Filter));
            }
            return true;
        }

        public CRUDAccountOptionsViewModel(AccountsViewModel accountsViewModel)
        {
            
            var app = System.Windows.Application.Current as App;
            _patientController = app.PatientController;
            _examController = app.ExamController;
            _medicalRecordController = app.MedicalRecordController;

            _patientList = new ObservableCollection<PatientViewModel>();
            DataGridCollection = CollectionViewSource.GetDefaultView(PatientList);

            AddAccountCommand = new GoToAddAccountCommand(this, accountsViewModel);
            DeleteAccountCommand = new DeleteAccountCommand(this, _patientController, _medicalRecordController, _examController);
            EditAccountCommand = new GoToEditAccountCommand(this, accountsViewModel);

            ObservableCollection<Patient> patientsFromBase = _patientController.ReadAllPatients();
            foreach(Patient patient in patientsFromBase)
            {
                _patientList.Add(new PatientViewModel(patient));
            }
        }

    }
}
