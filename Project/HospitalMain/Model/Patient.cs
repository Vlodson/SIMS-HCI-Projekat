using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Patient : Guest, INotifyPropertyChanged
    {
        
        public string Name { get; set; }
        private string Surname { get; set; }
        private DateTime DoB { get; set; }

        private List<Examination> examinations;

        public event PropertyChangedEventHandler PropertyChanged;



        public Patient(Guest guest, string name, string surname, DateTime doB, List<Examination> examinations):base(guest.ID)
        {
            this.Name = name;
            this.Surname = surname;
            this.DoB = doB;
            this.examinations = examinations;
        }


        public override string ToString()
        {
            return Name;
        }

        public Patient(Guest guest):base(guest.ID)
        {

        }

        public Patient():base()
        {

        }
    }
}