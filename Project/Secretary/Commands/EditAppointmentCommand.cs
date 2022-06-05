using Controller;
using Model;
using Secretary.ViewModel;
using Secretary.ViewUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Secretary.Commands
{
    public class EditAppointmentCommand : CommandBase
    {
        private HomeViewModel viewModel;
        private HomePageViewModel pageViewModel;
        private readonly ExamController _examController;
        private readonly EditAppointmentViewModel _editAppointmentViewModel;
        private readonly DoctorController _doctorController;

        public EditAppointmentCommand(EditAppointmentViewModel editAppointmentViewModel, HomeViewModel homeViewModel, HomePageViewModel homePageViewModel, ExamController examController, DoctorController doctorController)
        {
            _editAppointmentViewModel = editAppointmentViewModel;
            _examController = examController;
            viewModel = homeViewModel;
            pageViewModel = homePageViewModel;
            _doctorController = doctorController;

            _editAppointmentViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            int duration = 30;

            foreach (SelectableItemWrapper<Doctor> doctor in _editAppointmentViewModel.DoctorListBox)
            {
                if (doctor.IsSelected)
                {
                    _editAppointmentViewModel.Doctor = doctor.Item;
                }
            }

            foreach (SelectableItemWrapper<Patient> patient in _editAppointmentViewModel.PatientListBox)
            {
                if (patient.IsSelected)
                {
                    _editAppointmentViewModel.Patient = patient.Item;
                }
            }

            //treba pregeledati jos jednom
            //Examination newExamination = new Examination(_editAppointmentViewModel.Room.Id, _editAppointmentViewModel.Date, _crudAppointmentsViewModel.ExaminationViewModel.ID, _crudAppointmentsViewModel.ExaminationViewModel.Duration, _editAppointmentViewModel.ExaminationTypeEnum, _editAppointmentViewModel.Patient.ID, _editAppointmentViewModel.Doctor.Id);

            //_examController.EditExam(_editAppointmentViewModel.ExamID ,newExamination);
            //_doctorController.EditDoctorsExamination(_editAppointmentViewModel.Doctor.Id ,newExamination);
            
            if(parameter.ToString() == "Edit")
            {
                viewModel.CurrentHomeView = new HomePageViewModel(viewModel);
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return _examController.AppointmentDoctorEditValidation(_editAppointmentViewModel.ExamID, _editAppointmentViewModel.Date, _editAppointmentViewModel.Doctor) && _examController.AppointmentPatientEditValidation(_editAppointmentViewModel.ExamID, _editAppointmentViewModel.Date, _editAppointmentViewModel.Patient.ID) && _examController.AppointmentRoomEditValidation(_editAppointmentViewModel.ExamID, _editAppointmentViewModel.Date, _editAppointmentViewModel.Room.Id)  && !string.IsNullOrEmpty(_editAppointmentViewModel.Room.Id) && !string.IsNullOrEmpty(_editAppointmentViewModel.Patient.ID) && base.CanExecute(parameter);
        }
    
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditAppointmentViewModel.Room.Id) || e.PropertyName == nameof(EditAppointmentViewModel.Patient.ID) || e.PropertyName == nameof(EditAppointmentViewModel.Date) || e.PropertyName == nameof(EditAppointmentViewModel.Doctor))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
