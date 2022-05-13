using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class EmergencyGeneralViewModel : ViewModelBase
    {
        private ViewModelBase _currentEmergencyView;
        public ViewModelBase CurrentEmergencyView
        {
            get { return _currentEmergencyView; }
            set { _currentEmergencyView = value; OnPropertyChanged(nameof(CurrentEmergencyView)); }
        }

        public EmergencyGeneralViewModel()
        {
            CurrentEmergencyView = new EmergencyViewModel(this);
        }
    }
}
