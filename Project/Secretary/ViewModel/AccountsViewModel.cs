using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class AccountsViewModel : ViewModelBase
    {

        private ViewModelBase _currentCRUDAccView;
        public ViewModelBase CurrentCRUDAccView
        {
            get { return _currentCRUDAccView; }
            set { _currentCRUDAccView = value; OnPropertyChanged(nameof(CurrentCRUDAccView)); }
        }

        public AccountsViewModel()
        {
            CurrentCRUDAccView = new CRUDAccountOptionsViewModel(this);
        }
    }
}
