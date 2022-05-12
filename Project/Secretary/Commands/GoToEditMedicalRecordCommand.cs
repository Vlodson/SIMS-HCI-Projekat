using Secretary.View;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class GoToEditMedicalRecordCommand : CommandBase
    {
        private readonly CRUDMedicalRecordViewModel _cRUDMedicalRecordViewModel;
        private readonly MedicalRecordsViewModel _medicalRecordsViewModel;

        public GoToEditMedicalRecordCommand(CRUDMedicalRecordViewModel cRUDMedicalRecordViewModel, MedicalRecordsViewModel medicalRecordsViewModel)
        {
            _cRUDMedicalRecordViewModel = cRUDMedicalRecordViewModel;
            _medicalRecordsViewModel = medicalRecordsViewModel;

            _cRUDMedicalRecordViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "EditMedRecord")
            {
                _medicalRecordsViewModel.CurrentCRUDMedRecView = new EditMedicalRecordViewModel(_cRUDMedicalRecordViewModel, _medicalRecordsViewModel);
            }

            //EditMedicalRecord editMedicalRecord = new EditMedicalRecord();
            //editMedicalRecord.DataContext = new EditMedicalRecordViewModel(_cRUDMedicalRecordViewModel, editMedicalRecord);
            //editMedicalRecord.ShowDialog();
        }

        public override bool CanExecute(object? parameter)
        {
            return !(_cRUDMedicalRecordViewModel.MedicalRecordViewModel == null) && base.CanExecute(parameter);
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
