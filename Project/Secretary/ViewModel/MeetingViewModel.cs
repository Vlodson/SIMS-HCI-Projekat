using HospitalMain.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.ViewModel
{
    public class MeetingViewModel : ViewModelBase
    {
        private Meeting _meeting;

        public String ID => _meeting.ID;
        public String MeetingTopic => _meeting.MeetingTopic;
        public String RoomID => _meeting.RoomID;
        public DateTime DateTime => _meeting.DateTime;
        public ObservableCollection<Doctor> Doctors => _meeting.Doctors;

        public MeetingViewModel(Meeting meeting)
        {
            _meeting = meeting;
        }
    }
}
