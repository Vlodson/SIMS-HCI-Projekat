using System;
using System.ComponentModel;

namespace Model
{
    public class Examination : INotifyPropertyChanged
    {



        private Room examRoom;
        private DateTime date;
        private String id;
        private int duration;

        public Patient patient;
        public Doctor doctor;



        // public DateTime Date { get => date; set => date = value; }


        /*
        public DateTime getDate()
        {
            return date;
        }
        public void setDate(DateTime newDate)
        {
            this.date = newDate;
        }
        */


        public Room Room
        {
            get
            {
                return examRoom;
            }
            set
            {
                if (value != examRoom)
                {
                    examRoom = value;
                    OnPropertyChanged("Room");
                    
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

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                   
                }
            }
        }

        public int Duration
        {
            get
            {
                return duration;
            }
            set
            {
                if (value != duration)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                    
                }
            }
        }

        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                if (value != patient)
                {
                    patient = value;
                    OnPropertyChanged("Patient");
                    
                }
            }
        }

        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                if (value != doctor)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");
                    
                }
            }
        }

        public Examination(Room examRoom, DateTime date, string id, int duration, Patient patient, Doctor doctor)
        {
            Room = examRoom;
            Date = date;
            Id = id;
            Duration = duration;
            Patient = patient;
            Doctor = doctor;
        }

        //public event PropertyChangedEventHandler? PropertyChanged;

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangedEventHandler PropertyChanging;


        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        

    }
}