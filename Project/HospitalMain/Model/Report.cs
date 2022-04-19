using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Model
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
        private Patient patient;
        private Doctor doctor;
        private ObservableCollection<Therapy> therapy;

        public Report(string examinationId, string description, DateTime createDate, Patient patient, Doctor doctor, ObservableCollection<Therapy> therapy)
        {
            this.examinationId = examinationId;
            this.description = description;
            this.createDate = createDate;
            this.patient = patient;
            this.doctor = doctor;
            this.therapy = therapy;
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

        public Patient Patient
        {
            get { return patient; }
            set
            {
                if (patient != value)
                {
                    patient = value;
                    OnPropertyChanged("Patient");
                }
            }
        }

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                if (doctor != value)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");
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
    }
}
