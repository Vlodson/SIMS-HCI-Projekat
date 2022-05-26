using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Model
{
    public class PersonalNotification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String patientId;
        private String text;
        private List<int> days;
        private DateTime time;
        private bool status;

        public String Text
        {
            get
            {
                  return text;
            }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public DateTime Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

        public String PatientId
        {
            get
            {
                return patientId;
            }
            set
            {
                patientId = value;
                OnPropertyChanged("PatientId");
            }
        }

        public List<int> Days
        {
            get
            {
                return days;
            }
            set
            {
                days = value;
                OnPropertyChanged("Days");
            }
        }

        public bool Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public PersonalNotification(string patientId, string text, List<int> days,DateTime time)
        {
            this.patientId = patientId;
            this.text = text;
            this.days = days;
            this.time = time;
            this.status = true;
        }

        public PersonalNotification()
        {
        }
    }
}
