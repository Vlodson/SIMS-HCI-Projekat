using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Patient : Guest, INotifyPropertyChanged
    {
        
        private string name;
        private string surname { get; set; }
        private DateTime doB { get; set; }
        //public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }

        private List<Examination> examinations;

        public event PropertyChangedEventHandler PropertyChanged;



        public Patient(Guest guest, string name, string surname, DateTime doB, List<Examination> examinations):base(guest.ID)
        {
            //this.Id = id;
            this.Name = name;
            this.surname = surname;
            this.doB = doB;
            this.examinations = examinations;
        }


        public override string ToString()
        {
            return name;
        }

        public Patient(Guest guest):base(guest.ID)
        {

        }

        public Patient():base()
        {

        }
    }
}