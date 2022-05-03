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
      
        public int generateID (ObservableCollection<Examination> examinations)
        {
            return _patientService.generateID(examinations);
        }

        public bool checkIfAppointmentExists(DateTime date, Doctor doctor, String PatientID, String RoomID)
        {
            return _patientService.CheckIfAppointmentExists(date, doctor, PatientID, RoomID);
        }

        public bool checkIfAppointmentExistsForEditing(String ExamID, DateTime date, Doctor doctor, String PatientID, String RoomID)
        {
            return _patientService.CheckIfAppointmentExistsForEditing(ExamID ,date, doctor, PatientID, RoomID);
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