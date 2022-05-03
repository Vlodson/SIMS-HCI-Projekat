using Secretary.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigatiorStore;

        public ViewModelBase CurrentViewModel { get; set; }
        //public ViewModelBase CurrentViewModel => _navigatiorStore.CurrentViewModel;

        public MainViewModel()
        {
            //_navigatiorStore = navigationStore;
            
            //CurrentViewModel = new AddAppointmentViewModel();

            //_navigatiorStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        //private void OnCurrentViewModelChanged()
        //{
        //    OnPropertyChanged(nameof(CurrentViewModel));
        //}

    }
}
