using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FreeDaysRequest : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private int freeDaysLeft;
        private string doctorId;
        private DateTime startDate;
        private DateTime endDate;
        private FreeDaysReasons reason;

        public FreeDaysRequest(int freeDaysLeft, string doctorId, DateTime startDate, DateTime endDate, FreeDaysReasons reason)
        {
            this.freeDaysLeft = freeDaysLeft;
            this.doctorId = doctorId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.reason = reason;
        }
        public FreeDaysRequest() { }

        public int FreeDaysLeft
        {
            get
            {
                return freeDaysLeft;
            }
            set
            {
                if (value != freeDaysLeft)
                {
                    freeDaysLeft = value;
                    OnPropertyChanged("FreeDaysLeft");
                }
            }

        }

        public String DoctorId
        {
            get
            {
                return doctorId;
            }
            set
            {
                if (value != doctorId)
                {
                    doctorId = value;
                    OnPropertyChanged("DoctorId");
                }
            }

        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }
            }

        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }
            }

        }

        public FreeDaysReasons Reason
        {
            get
            {
                return reason;
            }
            set
            {
                if (value != reason)
                {
                    reason = value;
                    OnPropertyChanged("Reason");
                }
            }

        }


    }

}
