using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class MedicalRecordsViewModel : ViewModelBase
    {
        private ViewModelBase _currentCRUDMedRecView;
        public ViewModelBase CurrentCRUDMedRecView
        {
            get { return _currentCRUDMedRecView; }
            set { _currentCRUDMedRecView = value; OnPropertyChanged(nameof(CurrentCRUDMedRecView)); }
        }

        public MedicalRecordsViewModel()
        {
            CurrentCRUDMedRecView = new CRUDMedicalRecordViewModel(this);
        }

    }
}
