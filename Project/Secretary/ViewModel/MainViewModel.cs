using Secretary.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm;
using Microsoft.Toolkit.Mvvm.Input;
using Secretary.Commands;

namespace Secretary.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigatiorStore;

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; OnPropertyChanged(nameof(CurrentViewModel)); }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            CurrentViewModel = new CRUDAppointmentsViewModel();

            UpdateViewCommand = new UpdateViewCommand(this);
        }

    }
}
