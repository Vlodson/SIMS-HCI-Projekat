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


        private String examRoomId;
        private DateTime date;
        private string id;
        private int duration;
        private ExaminationTypeEnum type;

        private String patientId;
        private String doctorId;

        public String ExamRoomId 
        {
            get
            {
                return examRoomId;
            }
            set
            {
                examRoomId = value;
            }
        }
        //public DateTime Date { get => date; set => date = value; }
        public string Id { get => id; set => id = value; }
        public int Duration { get => duration; set => duration = value; }
        //public ExaminationType Type { get => type; set => type = value; }
        public String PatientId
        {
            get
            {
                return patientId;
            }
            set
            {
                patientId = value;
            }
        }
        public String DoctorId
        {
            get
            {
                return doctorId;
            }
            set
            {
                doctorId = value;
            }
        }

        public String DoctorNameSurname { get; set; }
        public DoctorType DoctorType { get; set; }
        

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
        public Examination(String examRoom, DateTime date, string id, int duration, ExaminationTypeEnum type, String patient, String doctor)
        {
            this.ExamRoomId = examRoom;
            this.Date = date;
            this.Id = id;
            this.Duration = duration;
            this.EType = type;
            this.PatientId = patient;
            this.DoctorId = doctor;
        }
        public Examination()
        {

        }
    }
}