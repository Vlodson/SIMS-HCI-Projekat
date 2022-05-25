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

namespace Secretary.Commands
{
    public class AddAppointmentCommand : CommandBase
    {
        private readonly AddAppointmentViewModel _addAppointmentViewModel;
        private readonly ExamController _examController;
        private readonly CRUDAppointmentsViewModel _crudAppointmentsViewModel;
        private readonly DoctorController _doctorController;
        private Window _addApointment;

        public AddAppointmentCommand(AddAppointmentViewModel addAppointmentViewModel, CRUDAppointmentsViewModel cRUDAppointmentsViewModel,ExamController examController, Window addApointment, DoctorController doctorController)
        {
            _addAppointmentViewModel = addAppointmentViewModel;
            _crudAppointmentsViewModel = cRUDAppointmentsViewModel;
            _examController = examController;
            _addApointment = addApointment;
            _doctorController = doctorController;

            _addAppointmentViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _examController.AppointmentDoctorValidation(_addAppointmentViewModel.Date, _addAppointmentViewModel.Doctor) && _examController.AppointmentPatientValidation(_addAppointmentViewModel.Date, _addAppointmentViewModel.PatientID) && _examController.AppointmentRoomValidation(_addAppointmentViewModel.Date, _addAppointmentViewModel.RoomID) && !string.IsNullOrEmpty(_addAppointmentViewModel.RoomID) && !string.IsNullOrEmpty(_addAppointmentViewModel.PatientID) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            int examID = _examController.generateID(_examController.getAllExaminations());
            int duration = 30;

            //pravljenje novog pregleda
            Examination examination = new Examination(_addAppointmentViewModel.RoomID, _addAppointmentViewModel.Date, examID.ToString(), duration, _addAppointmentViewModel.ExaminationTypeEnum, _addAppointmentViewModel.PatientID, _addAppointmentViewModel.Doctor.Id);
            _examController.CreateExamination(examination);
            _doctorController.AddExaminationToDoctor(_addAppointmentViewModel.Doctor.Id, examination);

            //update liste pregleda
            UpdateExaminations();

            //zatvaranje prozora
            _addApointment.Close();
        }

        private void UpdateExaminations()
        {
            _crudAppointmentsViewModel.ExaminationList.Clear();
            ObservableCollection<Examination> examinationsFromBase = _examController.getAllExaminations();

            foreach(Examination examination in examinationsFromBase)
            {
                _crudAppointmentsViewModel.ExaminationList.Add(new ExaminationViewModel(examination));
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AddAppointmentViewModel.RoomID) || e.PropertyName == nameof(AddAppointmentViewModel.PatientID) || e.PropertyName == nameof(AddAppointmentViewModel.Date) || e.PropertyName == nameof(AddAppointmentViewModel.Doctor))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
