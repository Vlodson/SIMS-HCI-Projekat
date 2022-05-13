using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class RequestsViewModel : ViewModelBase
    {
        private ViewModelBase _currentRequestView;
        public ViewModelBase CurrentRequestView
        {
            get { return _currentRequestView; }
            set { _currentRequestView = value; OnPropertyChanged(nameof(CurrentRequestView)); }
        }

        public RequestsViewModel()
        {
            CurrentRequestView = new OrderDynamicEquipmentViewModel(this);
        }

    }
}
