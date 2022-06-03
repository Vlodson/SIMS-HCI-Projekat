using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private ViewModelBase _currentHomeView;
        public ViewModelBase CurrentHomeView
        {
            get { return _currentHomeView; }
            set { _currentHomeView = value; OnPropertyChanged(nameof(CurrentHomeView)); }
        }

        public HomeViewModel()
        {
            CurrentHomeView = new HomePageViewModel(this);
        }

    }
}
