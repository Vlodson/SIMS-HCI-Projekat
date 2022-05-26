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
        private DateTime time;

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

        public PersonalNotification(string patientId, string text, DateTime time, List<string> days)
        {
            this.patientId = patientId;
            this.text = text;
            this.time = time;
        }

        public PersonalNotification()
        {
        }
    }
}
