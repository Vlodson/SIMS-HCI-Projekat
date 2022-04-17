using System;
using System.ComponentModel;
using HospitalMain.Enums;

namespace Model
{

    public class Examination : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        private Room examRoom;
        private DateTime date;
        private string id;
        private int duration;
        private ExaminationTypeEnum type;

        private Patient patient;
        private Doctor doctor;

        public Room ExamRoom { get => examRoom; set => examRoom = value; }
        //public DateTime Date { get => date; set => date = value; }
        public string Id { get => id; set => id = value; }
        public int Duration { get => duration; set => duration = value; }
        //public ExaminationType Type { get => type; set => type = value; }
        public Patient Patient { get => patient; set => patient = value; }
        public Doctor Doctor { get => doctor; set => doctor = value; }

        public ExaminationTypeEnum EType
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged("EType");
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
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public Examination(Room examRoom, DateTime date, string id, int duration, ExaminationTypeEnum type, Patient patient, Doctor doctor)
        {
            this.ExamRoom = examRoom;
            this.Date = date;
            this.Id = id;
            this.Duration = duration;
            this.EType = type;
            this.Patient = patient;
            this.Doctor = doctor;
        }
    }
}