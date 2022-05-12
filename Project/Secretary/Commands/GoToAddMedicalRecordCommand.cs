using Secretary.View;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class GoToAddMedicalRecordCommand : CommandBase
    {
        private readonly CRUDMedicalRecordViewModel _cRUDMedicalRecordViewModel;
        private readonly MedicalRecordsViewModel _medicalRecordsViewModel;

        public GoToAddMedicalRecordCommand(CRUDMedicalRecordViewModel cRUDMedicalRecordViewModel, MedicalRecordsViewModel medicalRecordsViewModel)
        {
            _cRUDMedicalRecordViewModel = cRUDMedicalRecordViewModel;
            _medicalRecordsViewModel = medicalRecordsViewModel;
        }

        public override void Execute(object? parameter)
        {
            //unselect tabele
            _cRUDMedicalRecordViewModel.MedicalRecordViewModel = null;
        
            if(parameter.ToString() == "AddMedRecord")
            {
                _medicalRecordsViewModel.CurrentCRUDMedRecView = new AddMedicalRecordViewModel(_cRUDMedicalRecordViewModel, _medicalRecordsViewModel);
            }

            //AddMedicalRecord addMedicalRecord = new AddMedicalRecord();
            //addMedicalRecord.DataContext = new AddMedicalRecordViewModel(_cRUDMedicalRecordViewModel, addMedicalRecord);
            //addMedicalRecord.ShowDialog();
        }
    }
}
