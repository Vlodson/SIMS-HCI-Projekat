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

        public GoToAddMedicalRecordCommand(CRUDMedicalRecordViewModel cRUDMedicalRecordViewModel)
        {
            _cRUDMedicalRecordViewModel = cRUDMedicalRecordViewModel;
        }

        public override void Execute(object? parameter)
        {
            //unselect tabele
            _cRUDMedicalRecordViewModel.MedicalRecordViewModel = null;
        
            AddMedicalRecord addMedicalRecord = new AddMedicalRecord();
            addMedicalRecord.DataContext = new AddMedicalRecordViewModel(_cRUDMedicalRecordViewModel, addMedicalRecord);
            addMedicalRecord.ShowDialog();
        }
    }
}
