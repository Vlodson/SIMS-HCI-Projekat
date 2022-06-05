using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class RequestsViewModel : ViewModelBase
    {

        private ViewModelBase _equipmentTab;
        public ViewModelBase EquipmentTab
        {
            get { return _equipmentTab; }
            set { _equipmentTab = value; OnPropertyChanged(nameof(EquipmentTab)); }
        }

        private ViewModelBase _absenceTab;
        public ViewModelBase AbsenceTab
        {
            get { return _absenceTab; }
            private set { _absenceTab = value; OnPropertyChanged(nameof(AbsenceTab)); }
        }

        private MainViewModel _mainViewModel;

        public RequestsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            EquipmentTab = new EquipmentViewModel(_mainViewModel);
            AbsenceTab = new FreeDaysRequestViewModel(_mainViewModel);
        }

    }
}
