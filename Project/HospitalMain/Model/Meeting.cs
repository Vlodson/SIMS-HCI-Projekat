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
        private ObservableCollection<Doctor> _doctors;

        public Meeting() { }

        public Meeting(string id, string meetingTopic, string roomID, DateTime dateTime, ObservableCollection<Doctor> doctors)
        {
            _id = id;
            _meetingTopic = meetingTopic;
            _roomID = roomID;
            _dateTime = dateTime;
            _doctors = doctors;
        }

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
            get { return _doctors; }
            set
            {
                if(_doctors != value)
                {
                    _doctors = value;
                    OnPropertyChanged(nameof(Doctors));
                }
            }
        }
    }
}
