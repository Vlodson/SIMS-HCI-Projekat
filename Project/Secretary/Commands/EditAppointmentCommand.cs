﻿using Controller;
using Model;
using Secretary.ViewModel;
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
        private readonly CRUDAppointmentsViewModel _crudAppointmentsViewModel;
        private readonly ExamController _examController;
        private readonly EditAppointmentViewModel _editAppointmentViewModel;
        private Window _editApointment;
        private int duration = 30;

        public EditAppointmentCommand(EditAppointmentViewModel editAppointmentViewModel, CRUDAppointmentsViewModel cRUDAppointmentsViewModel,ExamController examController, Window editAppointment)
        {
            _crudAppointmentsViewModel = cRUDAppointmentsViewModel;
            _editAppointmentViewModel = editAppointmentViewModel;
            _examController = examController;
            _editApointment = editAppointment;

            _editAppointmentViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            //treba pregeledati jos jednom
            Examination newExamination = new Examination(_editAppointmentViewModel.RoomID, _editAppointmentViewModel.Date, _crudAppointmentsViewModel.ExaminationViewModel.ID, _crudAppointmentsViewModel.ExaminationViewModel.Duration, _editAppointmentViewModel.ExaminationTypeEnum, _editAppointmentViewModel.PatientID, _editAppointmentViewModel.Doctor.Id);

            _examController.EditExam(_editAppointmentViewModel.ExamID ,newExamination);
            UpdateExaminations();
            _editApointment.Close();
        }

        private void UpdateExaminations()
        {
            _crudAppointmentsViewModel.ExaminationList.Clear();
            ObservableCollection<Examination> examinationsFromBase = _examController.getAllExaminations();

            foreach(Examination exam in examinationsFromBase)
            {
                _crudAppointmentsViewModel.ExaminationList.Add(new ExaminationViewModel(exam));
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_editAppointmentViewModel.RoomID) && !string.IsNullOrEmpty(_editAppointmentViewModel.PatientID) && base.CanExecute(parameter);
        }
    
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EditAppointmentViewModel.RoomID) || e.PropertyName == nameof(EditAppointmentViewModel.PatientID))
            {
                OnCanExecutedChanged();
            }
        }
    }
}