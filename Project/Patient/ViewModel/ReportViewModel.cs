using Controller;
using Model;
using Patient.Views;
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
        private String note;
        private Report thisReport;

        private MedicalRecordController _medicalRecordController;
        private DoctorController _doctorController;

        public MyICommand EditNoteCommand { get; set; }

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

        public ReportViewModel(Report report)
        {
            App app = Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;
            _doctorController = app.DoctorController;

            EditNoteCommand = new MyICommand(OnEditNote);

            dateLabel = report.CreateDate.ToString("dd.MM.yyyy HH:mm");
            doctorLabel = report.DoctorNameSurname;
            description = report.Description;
            therapyList = report.Therapy.ToList();
            note = report.Note;
            thisReport = report;
        }

        public void OnEditNote()
        {
            EditNote editNote = new EditNote(thisReport);
            editNote.ShowDialog();
            note = thisReport.Note;
            OnPropertyChanged("Note");
        }
    }
}
