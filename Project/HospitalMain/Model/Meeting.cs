using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Model
{
    public class Meeting : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String _id;
        private String _meetingTopic;
        private String _roomID;
        private DateTime _dateTime;
        private ObservableCollection<Doctor> doctors;

        public String ID
        {
            get { return _id; }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        public String MeetingTopic
        {
            get { return _meetingTopic; }
            set
            {
                if(_meetingTopic != value)
                {
                    _meetingTopic = value;
                    OnPropertyChanged(nameof(MeetingTopic));
                }
            }
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                if(_dateTime != value)
                {
                    _dateTime = value;
                    OnPropertyChanged(nameof(DateTime));
                }
            }
        }

        public String RoomID
        {
            get { return _roomID; }
            set
            {
                if(_roomID != value)
                {
                    _roomID = value;
                    OnPropertyChanged(nameof(RoomID));
                }
            }
        }

        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                if(doctors != value)
                {
                    doctors = value;
                    OnPropertyChanged(nameof(Doctors));
                }
            }
        }
    }
}
