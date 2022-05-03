using Controller;
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
    public class DeleteMedicalRecordCommand : CommandBase
    {
        private readonly MedicalRecordController _medicalRecordController;
        private readonly CRUDMedicalRecordViewModel _cRUDMedicalRecordViewModel;

        public DeleteMedicalRecordCommand(CRUDMedicalRecordViewModel cRUDMedicalRecordViewModel, MedicalRecordController medicalRecordController)
        {
            _medicalRecordController = medicalRecordController;
            _cRUDMedicalRecordViewModel = cRUDMedicalRecordViewModel;

            _cRUDMedicalRecordViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_cRUDMedicalRecordViewModel.MedicalRecordViewModel == null) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _medicalRecordController.DeleteMedicalRecord(_cRUDMedicalRecordViewModel.MedicalRecordViewModel.ID);
            UpdateMedicalRecords();
        }

        private void UpdateMedicalRecords()
        {
            _cRUDMedicalRecordViewModel.MedicalRecords.Clear();
            ObservableCollection<MedicalRecord> medicalRecordsFromBase = _medicalRecordController.GetAllMedicalRecords();

            foreach (MedicalRecord medicalRecord in medicalRecordsFromBase)
            {
                _cRUDMedicalRecordViewModel.MedicalRecords.Add(new MedicalRecordViewModel(medicalRecord));
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CRUDMedicalRecordViewModel.MedicalRecordViewModel))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
