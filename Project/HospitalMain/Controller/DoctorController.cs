﻿using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class DoctorController
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public DoctorType GetDoctorsType(string doctorID)
        {
            return _doctorService.GetDoctorsType(doctorID);
        }

        public bool AddExaminationToDoctor(String doctorID, Examination exam)
        {
            return _doctorService.AddExaminationToDoctor(doctorID, exam);
        }

        public void EditDoctorsExamination(String doctorID, Examination newExam)
        {
            _doctorService.EditDoctorsExamination(doctorID, newExam);
        }

        public bool EmergencyValidation(DateTime dateTime, DoctorType doctorType)
        {
            return _doctorService.EmergencyValidation(dateTime, doctorType);
        }

        public ObservableCollection<Doctor> GetAll()
        {
            return _doctorService.GetDoctors();
        }

        public Doctor GetDoctor(string id)
        {
            return _doctorService.GetDoctor(id);
        }

        public string CheckForAvailableDateForEmergency(DateTime dateTime, DoctorType doctorType)
        {
            return _doctorService.CheckForAvailableDateForEmergency(dateTime, doctorType);
        }

        public Examination GetBookedExamination(DateTime dateTime, DoctorType doctorType)
        {
            return _doctorService.GetBookedExamination(dateTime, doctorType);
        }

        public ObservableCollection<Examination> GetFreeExaminations(ObservableCollection<DateTime> startEndRange, DoctorType doctorType)
        {
            return _doctorService.GetFreeExaminations(startEndRange, doctorType);
        }

        public List<Examination> GenerateDoctorFreeExaminations(Doctor doctor, DateTime startDate, DateTime endDate)
        {
            return _doctorService.GenerateDoctorFreeExaminations(doctor, startDate, endDate);
        }
        /*
        public List<Examination> GetFreeGetFreeExaminations(Doctor doctor, DateTime startDate, DateTime endDate, bool priority)
        {
            return _doctorService.GetFreeExaminations(doctor, startDate, endDate, priority);
        }
        */
        public List<Examination> MoveExaminations(Examination examination)
        {
            return _doctorService.MoveExaminations(examination);
        }
        public ObservableCollection<string> GetDoctorsBySpecialization(DoctorType selectedSpec)
        {
            return _doctorService.GetDoctorsBySpecialization(selectedSpec);
        }
    }
}
