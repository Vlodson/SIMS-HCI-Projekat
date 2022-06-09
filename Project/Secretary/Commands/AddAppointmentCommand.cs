using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controller;
using Model;
using Secretary.ViewModel;
using Secretary.ViewUtils;

namespace Secretary.Commands
{
    public class AddAppointmentCommand : CommandBase
    {
        private readonly AddAppointmentViewModel _addAppointmentViewModel;
        private readonly ExamController _examController;
        private readonly MainViewModel _mainViewModel;
        private readonly DoctorController _doctorController;

        public AddAppointmentCommand(AddAppointmentViewModel addAppointmentViewModel, MainViewModel mainViewModel,ExamController examController, DoctorController doctorController)
        {
            _addAppointmentViewModel = addAppointmentViewModel;
            _mainViewModel = mainViewModel;
            _examController = examController;
            _doctorController = doctorController;

            _addAppointmentViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            DateTime dt = _addAppointmentViewModel.Date;
            return _examController.CheckIfDoctorIsOnVacation(_addAppointmentViewModel.Doctor.Id, dt.Add(TimeSpan.Parse(_addAppointmentViewModel.Time))) && _examController.AppointmentDoctorValidation(dt.Add(TimeSpan.Parse(_addAppointmentViewModel.Time)), _addAppointmentViewModel.Doctor) && _examController.AppointmentPatientValidation(dt.Add(TimeSpan.Parse(_addAppointmentViewModel.Time)), _addAppointmentViewModel.Patient.ID) && _examController.AppointmentRoomValidation(dt.Add(TimeSpan.Parse(_addAppointmentViewModel.Time)), _addAppointmentViewModel.Room.Id) && !string.IsNullOrEmpty(_addAppointmentViewModel.Room.Id) && !string.IsNullOrEmpty(_addAppointmentViewModel.Patient.ID) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            int examID = _examController.generateID(_examController.GetExaminations());
            int duration = 30;

            changeSelectedDoctor();

            foreach(SelectableItemWrapper<Patient> patient in _addAppointmentViewModel.PatientListBox)
            {
                if (patient.IsSelected)
                {
                    _addAppointmentViewModel.Patient = patient.Item;
                }
            }

            foreach (SelectableItemWrapper<Doctor> doctor in _addAppointmentViewModel.DoctorListBox)
            {
                if (doctor.IsSelected)
                {
                    _addAppointmentViewModel.Doctor = doctor.Item;
                }
            }

            //pravljenje novog pregleda
            Examination examination = new Examination(_addAppointmentViewModel.Room.Id, _addAppointmentViewModel.Date, examID.ToString(), duration, _addAppointmentViewModel.ExaminationTypeEnum, _addAppointmentViewModel.Patient.ID, _addAppointmentViewModel.Doctor.Id);
            _examController.CreateExamination(examination);
            _doctorController.AddExaminationToDoctor(_addAppointmentViewModel.Doctor.Id, examination);

            if(parameter.ToString() == "Add")
            {
                _mainViewModel.CurrentViewModel = new BookViewModel(_mainViewModel);
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //changeSelectedDoctor();
            if (e.PropertyName == nameof(AddAppointmentViewModel.Date) || e.PropertyName == nameof(AddAppointmentViewModel.Time) || e.PropertyName == nameof(AddAppointmentViewModel.Doctor) || e.PropertyName == nameof(AddAppointmentViewModel.DoctorListBox))
            {
                OnCanExecutedChanged();
            }
        }

        private void changeSelectedDoctor()
        {
            foreach (SelectableItemWrapper<Doctor> doctor in _addAppointmentViewModel.DoctorListBox)
            {
                if (doctor.IsSelected)
                {
                    _addAppointmentViewModel.Doctor = doctor.Item;
                }
            }
        }
    }
}
