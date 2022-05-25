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
    public class EditNoteViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private MedicalRecordController _medicalRecordController;

        private string currentNote;
        private Report thisReport;
        private Window thisWindow;
        
        public String CurrentNote
        {
            get
            {
                return currentNote;
            }
            set
            {
                currentNote = value;
                OnPropertyChanged("CurrentNote");
            }
        }

        public MyICommand AddNoteCommand { get; set; }

        public EditNoteViewModel(Report report, Window window)
        {
            App app = Application.Current as App;
            _medicalRecordController = app.MedicalRecordController;

            AddNoteCommand = new MyICommand(OnAddNoteCommand);

            currentNote = report.Note;
            thisReport = report;
            thisWindow = window;
        }

        private void OnAddNoteCommand()
        {
            _medicalRecordController.AddNote(thisReport, CurrentNote);
            thisWindow.Close();
        }
    }
}
