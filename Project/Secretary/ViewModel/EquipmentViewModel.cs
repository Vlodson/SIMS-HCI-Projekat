using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class EquipmentViewModel : ViewModelBase
    {

        private ViewModelBase _currentEquipmentView;
        public ViewModelBase CurrentEquipmentView
        {
            get { return _currentEquipmentView; }
            set { _currentEquipmentView = value; OnPropertyChanged(nameof(CurrentEquipmentView)); }
        }
    
        public EquipmentViewModel(MainViewModel mainViewModel)
        {
            CurrentEquipmentView = new OrderDynamicEquipmentViewModel(this, mainViewModel);
        }
    }
}
