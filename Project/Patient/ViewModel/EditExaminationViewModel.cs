using Controller;
using Model;
using Patient.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Patient.ViewModel
{
    public class EditExaminationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DoctorController _doctorController;
        private ExamController _examController;

        private List<Examination> availableDates;
        private String doctorType;
        private String doctorNameSurname;
        private DateTime oldDate;
        private Examination selectedExamination;

        private Window thisWindow;

        public MyICommand EditExaminationCommand { get; set; }

        public List<Examination> AvailableDates
        {
            get
            {
                return availableDates;
            }
            set
            {
                availableDates = value;
                OnPropertyChanged("AvailableDates");
            }
        }

        public String DoctorType
        {
            get
            {
                return doctorType;
            }
            set
            {
                doctorType = value;
                OnPropertyChanged("DoctorType");
            }
        }

        public String DoctorNameSurname
        {
            get
            {
                return doctorNameSurname;
            }
            set
            {
                doctorNameSurname = value;
                OnPropertyChanged("DoctorNameSurname");
            }
        }

        public DateTime OldDate
        {
            get
            {
                return oldDate;
            }
            set
            {
                oldDate = value;
                OnPropertyChanged("OldDate");
            }
        }

        public Examination SelectedExamination
        {
            get
            {
                return selectedExamination;
            }
            set
            {
                selectedExamination = value;
                OnPropertyChanged("SelectedExamination");
                EditExaminationCommand.RaiseCanExecuteChanged();
            }
        }
        public EditExaminationViewModel(Window window)
        {
            App app = Application.Current as App;
            _doctorController = app.DoctorController;
            _examController = app.ExamController;

            AvailableDates = _doctorController.MoveExaminations(ExaminationsList.selected);
            switch (ExaminationsList.selected.DoctorType)
            {
                case Model.DoctorType.Pulmonology:
                    DoctorType = "pulmologija";
                    break;
                case Model.DoctorType.Cardiology:
                    DoctorType = "kardiologija";
                    break;
                case Model.DoctorType.Neurology:
                    DoctorType = "Neurologija";
                    break;
                case Model.DoctorType.Dermatology:
                    DoctorType = "dermatologija";
                    break;
                default:
                    DoctorType = "opšta praksa";
                    break;

            }
            DoctorType = ExaminationsList.selected.DoctorTypeString;
            DoctorNameSurname = ExaminationsList.selected.DoctorNameSurname;
            OldDate = ExaminationsList.selected.Date;

            EditExaminationCommand = new MyICommand(OnEditExamination, CanEditExamination);

            thisWindow = window;
        }

        private bool CanEditExamination()
        {
            return SelectedExamination != null;
        }

        private void OnEditExamination()
        {
            _examController.PatientEditExamForMoving(ExaminationsList.selected, SelectedExamination.Date);
            _examController.SaveExaminationRepo();
            thisWindow.Close();
        }
    }
}
