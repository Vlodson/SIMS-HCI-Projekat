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
        private String ucin;
        private String name;
        private String surname;
        private DateTime doB;

        private ObservableCollection<Examination> examinations;

        public Patient(Guest guest) : base(guest.ID)
        {

        }

        public Patient() : base()
        {

        }

        public Patient(String id, String ucin, String name, String surname, DateTime doB, ObservableCollection<Examination> examinations)
        {
            this.id = id;
            this.ucin = ucin;
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
                if(value != id)
                {
                    id = value;
                    //OnPropertyChanged("ID");
                }
            }
        }

        public String UCIN
        {
            get
            {
                return ucin;
            }
            set
            {
                if (value != ucin)
                {
                    ucin = value;
                    OnPropertyChanged("UCIN");
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

        public override string ToString()
        {
            return Name;
        }

    }
}