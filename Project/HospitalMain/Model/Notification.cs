using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Model
{
    public class Notification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String content;
        private DateTime dateTimeNotification;

        public String Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
                OnPropertyChanged("Content");
            }
        }

        public DateTime DateTimeNotification
        {
            get
            {
                return dateTimeNotification;
            }
            set
            {
                dateTimeNotification = value;
                OnPropertyChanged("DateTimeNotification");
            }
        }

        public Notification(string content, bool isRead, DateTime dateTimeNotification)
        {
            Content = content;
            DateTimeNotification = dateTimeNotification;
        }

        public Notification()
        {

        }
    }
}
