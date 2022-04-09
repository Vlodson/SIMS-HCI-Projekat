using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Model
{
    public class Patient : Guest, INotifyPropertyChanged
    {
        private String id;
        private String name { get; set; }
        private String surname { get; set; }
        private DateTime doB { get; set; }

        //public Examination[] examination;
        private List<Examination> examinations;

        public event PropertyChangedEventHandler PropertyChanged;

        public String getId()
        {
            return id;
        }

        public void SetId(String newId)
        {
            this.id = newId;
        }

        public Patient(string id, string name, string surname, DateTime doB, List<Examination> examinations)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.doB = doB;
            this.examinations = examinations;
        }
    }
}