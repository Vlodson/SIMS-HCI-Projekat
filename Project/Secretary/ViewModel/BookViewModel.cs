using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class BookViewModel : ViewModelBase
    {
        private ViewModelBase _appointmentsTab;
        public ViewModelBase AppointmentsTab
        {
            get { return _appointmentsTab; }
            set { _appointmentsTab = value; OnPropertyChanged(nameof(AppointmentsTab)); }
        }

        private ViewModelBase _meetingsTab;
        public ViewModelBase MeetingsTab
        {
            get { return _meetingsTab; }
            set { _meetingsTab = value; OnPropertyChanged(nameof(MeetingsTab)); }
        }

        private MainViewModel _mainViewModel;

        public BookViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            AppointmentsTab = new AddAppointmentViewModel(_mainViewModel);
            MeetingsTab = new AddMeetingViewModel(_mainViewModel);
        }
    }
}
