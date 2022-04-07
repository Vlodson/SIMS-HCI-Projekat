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
        private String id { get; set; }
        private String name;
      private String surname { get; set; }
      private DateTime doB { get; set; }
      private DoctorType type { get; set; }

      //public Examination[] examination;
      private List<Examination> examination;

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
                    OnPropertyChanged("Surame");
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

        public Doctor(string id, string name, string surname, DateTime doB, DoctorType type, List<Examination> examination)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.doB = doB;
            this.type = type;
            this.examination = examination;
        }
    }
}