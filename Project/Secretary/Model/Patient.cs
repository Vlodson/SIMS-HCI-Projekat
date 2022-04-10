using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Model
{
    public class Patient : Guest, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String name)
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

        public ObservableCollection<Examination> examinations;

        public Patient(String id, String name, String surname, DateTime doB, ObservableCollection<Examination> examinations)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.doB = doB;
            this.examinations = examinations;
        }

        public String ID
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
                    OnPropertyChanged("ID");
                }
            }

        }

        public String Name
        {
            get { return name; }
            set
            {
                if(value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public String Surname
        {
            get { return surname; }
            set
            {
                if(surname != value)
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public DateTime DoB
        {
            get { return doB; }
            set
            {
                if(doB != value)
                {
                    doB = value;
                    OnPropertyChanged("DoB");
                }
            }
        }

        public ObservableCollection<Examination> Examinations
        {
            get{ return examinations; }
            set
            {
                if(examinations != value)
                {
                    examinations = value;
                    OnPropertyChanged("Examinations");
                }
            }
        }

    }
}