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

        private string id;
        private StatusEnum status;
        private string doctorId;
        private DateTime startDate;
        private DateTime endDate;
        private FreeDaysReasons reason;
        private string rejectionReason;

        public FreeDaysRequest(string id, StatusEnum status, string doctorId, DateTime startDate, DateTime endDate, FreeDaysReasons reason, string rejectionReason)
        {
            this.id = id;
            this.status = status;
            this.doctorId = doctorId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.reason = reason;
            this.rejectionReason = rejectionReason;
        }
        public FreeDaysRequest() { }

        public string ID
        {
            get { return id; }
            set
            {
                if(value != id)
                {
                    id = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        public string RejectionReason
        {
            get { return rejectionReason; }
            set 
            {
                if(value != rejectionReason)
                {
                    rejectionReason = value;
                    OnPropertyChanged("RejectionReason");
                }
            }
        }

        public StatusEnum Status
        {
            get
            {
                return status;
            }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
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
