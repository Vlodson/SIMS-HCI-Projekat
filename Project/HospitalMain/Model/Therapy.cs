using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Therapy: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string examId;
        private string medicine;
        private int duration;
        private int perDay;
        private bool prescription;

        public Therapy(string examId, string medicine, int duration, int perDay, bool prescription )
        {
            this.examId = examId;
            this.medicine = medicine;
            this.duration = duration;
            this.perDay = perDay;
            this.prescription = prescription;
        }

        public bool Prescription
        {
            get { return prescription; }
            set
            {
                if (prescription != value)
                {
                    prescription = value;
                    OnPropertyChanged("Prescription");
                }
            }
        }

        public string ExamId
        {
            get { return examId; }
            set
            {
                if (examId != value)
                {
                    examId = value;
                }
            }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        public int PerDay
        {
            get { return perDay; }
            set
            {
                if (perDay != value)
                {
                    perDay = value;
                    OnPropertyChanged("PerDay");
                }
            }
        }

        public string Medicine 
        {
            get { return medicine; }
            set
            {
                if (medicine != value)
                {
                    medicine = value;
                    OnPropertyChanged("Medicine");
                }
            }
        }
    }
}
