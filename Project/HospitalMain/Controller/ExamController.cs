using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
    public class ExamController
    {

        private PatientService _patientService;
        private DoctorService _doctorService;


        public ExamController(PatientService patientService, DoctorService doctorService)
        {
            this._patientService = patientService;
            this._doctorService = doctorService;
        }
      
        public Examination getTemporaryExam()
        {
            return _patientService.getTemporaryExam();
        }

        public int getValidationCounter()
        {
            return _patientService.getValidationCounter();
        }

        public void setValidationCounter(int value)
        {
            _patientService.setValidationCounter(value);
        }

        public int generateID (ObservableCollection<Examination> examinations)
        {
            return _patientService.generateID(examinations);
        }

        public Examination GetExamByTime(DateTime dateTime)
        {
            return _patientService.getExamByTime (dateTime);
        }

        public bool AppointmentDoctorValidation(DateTime dateTime, Doctor doctor)
        {
            return _patientService.AppointmentDoctorValidation(dateTime, doctor);
        }
           
        public bool AppointmentPatientValidation(DateTime dateTime, String PatientID)
        {
            return _patientService.AppointmentPatientValidation(dateTime, PatientID);
        }

        public bool AppointmentRoomValidation(DateTime dateTime, String RoomID)
        {
            return _patientService.AppointmentRoomValidation(dateTime, RoomID);
        }

        public bool AppointmentDoctorEditValidation(String ExamID, DateTime dateTime, Doctor doctor)
        {
            return _patientService.AppointmentDoctorEditValidation(ExamID, dateTime, doctor);
        }

        public bool AppointmentPatientEditValidation(String ExamID, DateTime dateTime, String PatientID)
        {
            return _patientService.AppointmentPatientEditValidation(ExamID, dateTime, PatientID);
        }

        public bool AppointmentRoomEditValidation(String ExamID, DateTime dateTime, String RoomID)
        {
            return _patientService.AppointmentRoomEditValidation(ExamID, dateTime, RoomID);
        }

        public ObservableCollection<Examination> getAllExaminations()
        {
            return _patientService.getAllExaminations();
        }

        public Examination getExamination(String examID)
        {
            return _patientService.GetExam(examID);
        }
      
        public bool PatientCreateExam(Examination examination, DateTime newDate)
        {
            return _patientService.CreateExam(examination, newDate);
        }

        public bool CreateExamination(Examination examination)
        {
            return _patientService.CreateExamination(examination);
        }

        public void DoctorCreateExam(Examination examination)
        {
            _doctorService.CreateExam(examination);
        }

        public void RemoveExam(Examination examination)
        {
            _patientService.RemoveExam(examination);
        }
        public void DoctorRemoveExam(Examination exam)
        {
            _doctorService.RemoveExam(exam);
        }

        public void DeleteExam(string examID)
        {
            _doctorService.RemoveExam(examID);
        }

        public ObservableCollection<Examination> ReadPatientExams(string id)
        {
            return _patientService.ReadMyExams(id);
        }

        public ObservableCollection<Examination> ReadDoctorExams(string id)
        {
            return _doctorService.ReadMyExams(id);
        }

        public void DoctorEditExam(string id, Examination examination)
        {
            _doctorService.EditExams(id, examination);
        }

        public void PatientEditExamForMoving(Examination examination, DateTime dateTime)
        {
            _patientService.EditExamForMoving(examination.Id, dateTime);
        }

        public void EditExam(string examID, Examination examination)
        {
            _patientService.SetExam(examID, examination.Date, examination.ExamRoomId, examination.Duration, examination.EType, examination.PatientId, examination.DoctorId);
        }

        public ObservableCollection<Examination> ReadEndedExams()
        {
            return _doctorService.ReadEndedExams();
        }

        public ObservableCollection<Examination> GetExaminations()
        {
            return _patientService.GetExaminations();
        }

        public void SaveExaminationRepo()
        {
            _patientService.SaveExaminationRepo();
        }
        public bool occupiedDate(DateTime dt)
        {
            return _doctorService.occupiedDate(dt);
        }

    }
}