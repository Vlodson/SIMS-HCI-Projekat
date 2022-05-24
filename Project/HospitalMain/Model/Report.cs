using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Report : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string examinationId; //jedan pregled se vezuje za tacno jedan izvjestaj i obrnuto
        private string description;
        private DateTime createDate;
        private string patientId;
        private string doctorId;
        private ObservableCollection<Therapy> therapy;
        private string note;

        public Report(string examinationId, string description, DateTime createDate, string patientId, string doctorId, ObservableCollection<Therapy> therapy, string note)
        {
            this.examinationId = examinationId;
            this.description = description;
            this.createDate = createDate;
            this.patientId = patientId;
            this.doctorId = doctorId;
            this.therapy = therapy;
            this.note = note;
        }

        public string ExaminationId
        {
            get { return examinationId; }
            set
            {
                if (examinationId != value)
                {
                    examinationId = value;
                   // OnPropertyChanged("ExaminationId");
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public DateTime CreateDate
        {
            get { return createDate; }
            set
            {
                if (createDate != value)
                {
                    createDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }

        public string PatientId
        {
            get { return patientId; }
            set
            {
                if (patientId != value)
                {
                    patientId = value;
                    OnPropertyChanged("PatientId");
                }
            }
        }

        public string DoctorId
        {
            get { return doctorId; }
            set
            {
                if (doctorId != value)
                {
                    doctorId = value;
                    OnPropertyChanged("DoctorId");
                }
            }
        }

        public ObservableCollection<Therapy> Therapy
        {
            get { return therapy; }
            set
            {
                if (therapy != value)
                {
                    therapy = value;
                    OnPropertyChanged("Therapy");
                }
            }
        }

        public String Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
                OnPropertyChanged("Note");
            }
        }

        public String DoctorNameSurname { get; set; }
        public String DoctorType { get; set; }
    }
}
