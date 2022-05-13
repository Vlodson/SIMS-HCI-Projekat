using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Referral: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string doctorId;
        private string patientId;
        private string referralId;
        private DoctorType specialization;
        private DateTime date;

        public Referral(string doctorId, string patientId, string referralId, DoctorType specialization, DateTime date)
        {
            this.doctorId = doctorId;
            this.patientId = patientId;
            this.referralId = referralId;
            this.specialization = specialization;
            this.date = date;
        }
        public string DoctorId
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
        public string PatientId
        {
            get
            {
                return patientId;
            }
            set
            {
                if (value != patientId)
                {
                    patientId = value;
                    OnPropertyChanged("PatientId");
                }
            }

        }
        public string ReferralId
        {
            get
            {
                return referralId;
            }
            set
            {
                if (value != referralId)
                {
                    referralId = value;
                    OnPropertyChanged("ReferralId");
                }
            }

        }
        public DoctorType Specialization
        {
            get
            {
                return specialization;
            }
            set
            {
                if (value != specialization)
                {
                    specialization = value;
                    OnPropertyChanged("Specialization");
                }
            }

        }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                if (value != date)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }

        }

    }
}
