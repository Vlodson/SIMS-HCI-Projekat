using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Doctor : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private String id;
        private String name;
        private String surname;
        private DateTime doB;
        private DoctorType type;
        private int freeDaysLeft;

        
        //public Examination[] examination;
        private List<Examination> examinations;

        public DateTime DoB
        {
            get
            {
                return doB;
            }
            set
            {
                doB = value;
            }
        }

        public List<Examination> Examinations
        {
            get
            {
                return examinations;
            }
            set
            {
                examinations = value;
            }
        }
        

        public String Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

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

        public String Surname
        {
            get
            {
                return surname;
            }
            set
            {
                if (value != surname)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public DoctorType Type
        {
            get
            {
                return type;
            }
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        public string NameSurname
        {
            get
            {
                return name + " " + surname;
            }
            set
            {
                string[] splitted = value.Split(" ");
                name = splitted[0];
                surname = splitted[1];
                OnPropertyChanged("NameSurname");
            }
        }
        public Doctor(string id, string name, string surname, DateTime doB, DoctorType type, int freeDaysLeft, List<Examination> examination)
        {
            /*
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.doB = doB;
            this.type = type;
            this.examinations = examination;
            */
            Id = id;
            Name = name;
            Surname = surname;  
            DoB = doB;
            Type = type;
            FreeDaysLeft = freeDaysLeft;
            Examinations = examination;
        }

        public Doctor()
        {
        }
    }
}