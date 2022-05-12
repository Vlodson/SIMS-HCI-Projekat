using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Secretary.ViewModel;

namespace Secretary.Commands
{
    public class CancelMedRecordCommand : CommandBase
    {
        private readonly MedicalRecordsViewModel _medicalRecordsViewModel;
        
        public CancelMedRecordCommand(MedicalRecordsViewModel medicalRecordsViewModel)
        {
            _medicalRecordsViewModel = medicalRecordsViewModel;
        }

        public override void Execute(object? parameter)
        {
            if(parameter.ToString() == "Cancel")
            {
                _medicalRecordsViewModel.CurrentCRUDMedRecView = new CRUDMedicalRecordViewModel(_medicalRecordsViewModel);
            }
        }
    }
}
