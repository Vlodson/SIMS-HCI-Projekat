using Controller;
using HospitalMain.Enums;
using Model;
using Secretary.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secretary.Commands
{
    public class AddEmergencyCommand : CommandBase
    {
        private readonly EmergencyViewModel _emergencyViewModel;
        private readonly ExamController _examController;
        private readonly DoctorController _doctorController;
        private readonly EmergencyGeneralViewModel _emergencyGeneralViewModel;

        public AddEmergencyCommand(EmergencyViewModel emergencyViewModel, ExamController examController, DoctorController doctorController, EmergencyGeneralViewModel emergencyGeneralViewModel)
        {
            _emergencyViewModel = emergencyViewModel;
            _examController = examController;
            _doctorController = doctorController;
            _emergencyGeneralViewModel = emergencyGeneralViewModel;

            _emergencyViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            bool flag = _doctorController.EmergencyValidation(_emergencyViewModel.DateTime, _emergencyViewModel.DoctorType);
            return ( flag || _emergencyViewModel.SelectedAppointment != null) && !string.IsNullOrEmpty(_emergencyViewModel.RoomID) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            //trajanje pregleda
            int duration = 30;

            //ovde se provera da li je u pitanju laksa ili teza varijanta (na osnovu vrednosti brojaca)
            
            if (_examController.getValidationCounter() == 0)
            {
                int examID = generateExamID();
                //laksa varijanta
                //pravim hitan slucaj
                string doctorID = _doctorController.CheckForAvailableDateForEmergency(_emergencyViewModel.DateTime, _emergencyViewModel.DoctorType);
                Examination emergencyExam = new Examination(_emergencyViewModel.RoomID, _emergencyViewModel.DateTime, examID.ToString(), duration, _emergencyViewModel.SelectedExamType, _emergencyViewModel.SelectedPatient.ID, doctorID);
                _examController.CreateExamination(emergencyExam);

                //dodajem ga u doktorove preglede
                _doctorController.AddExaminationToDoctor(doctorID, emergencyExam);

            } else
            {
                //dobavljanje informacija pregleda koji ce biti pomeren
                Examination bookedExam = _examController.getTemporaryExam();

                //kreiranje hitnog slucaja
                int examID = generateExamID();
                Examination emergencyExam = new Examination(_emergencyViewModel.RoomID, _emergencyViewModel.DateTime, examID.ToString(), duration, _emergencyViewModel.SelectedExamType, _emergencyViewModel.SelectedPatient.ID, bookedExam.DoctorId);
                _examController.CreateExamination(emergencyExam);

                //informacije iz pregleda koji se pomera
                String bookedExamRoomID = bookedExam.ExamRoomId;
                String bookedExamID = bookedExam.Id;
                ExaminationTypeEnum bookedExamType = bookedExam.EType;
                String bookedExamPatientID = bookedExam.PatientId;

                //brisem taj termin u bazi, kako bih ispao iz while petlje
                _examController.DeleteExam(bookedExamID);

                //odabran novi termin
                ExaminationViewModel selectedSuggestedAppointment = _emergencyViewModel.SelectedAppointment;

                //kreiranje/pomeranje novog termina
                Examination newExamination = new Examination(bookedExamRoomID, Convert.ToDateTime(selectedSuggestedAppointment.Date), bookedExamID, duration, bookedExamType, bookedExamPatientID, selectedSuggestedAppointment.Doctor.Id);
                _examController.CreateExamination(newExamination);

            }

            _examController.setValidationCounter(0);

            //treba dodati navigaciju
            if (parameter.ToString() == "AddEmergency")
            {
                _emergencyGeneralViewModel.CurrentEmergencyView = new EmergencyViewModel(_emergencyGeneralViewModel);
            }
        }

        private int generateExamID()
        {
            return _examController.generateID(_examController.getAllExaminations());
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EmergencyViewModel.RoomID) || e.PropertyName == nameof(EmergencyViewModel.DateTime) || e.PropertyName == nameof(EmergencyViewModel.DoctorType) || e.PropertyName == nameof(EmergencyViewModel.SelectedAppointment))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
