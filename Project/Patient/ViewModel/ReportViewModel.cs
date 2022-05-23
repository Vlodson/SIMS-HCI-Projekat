using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patient.ViewModel
{
    public class ReportViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private String dateLabel;
        private String doctorLabel;
        private String description;
        private List<Therapy> therapyList;


        private MedicalRecordController _medicalRecordController;
        private DoctorController _doctorController;

        public String DateLabel
        {
            get
            {
                return dateLabel;
            }
            set
            {
                dateLabel = value;
                OnPropertyChanged("DateLabel");
            }
        }
        public String DoctorLabel
        {
            get
            {
                return doctorLabel;
            }
            set
            {
                doctorLabel = value;
                OnPropertyChanged("DoctorLabel");
            }
        }

        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public List<Therapy> TherapyList
        {
            get
            {
                return therapyList;
            }
            set
            {
                therapyList = value;
                OnPropertyChanged("TherapyList");
            }
        }

        public ReportViewModel(Report report)
        {
            App app = Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;
            _doctorController = app.DoctorController;

            dateLabel = report.CreateDate.ToString("dd.MM.yyyy HH:mm");
            doctorLabel = report.DoctorNameSurname;
            description = report.Description;
            therapyList = report.Therapy.ToList();


        }
    }
}
