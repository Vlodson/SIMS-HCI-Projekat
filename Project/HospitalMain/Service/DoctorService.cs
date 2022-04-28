using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class DoctorService
    {


        private readonly DoctorRepo _doctorRepo;
        private readonly ExaminationRepo _examinationRepo;

        public DoctorService(DoctorRepo doctorRepo, ExaminationRepo examinationRepo)
        {
            _doctorRepo = doctorRepo;
            _examinationRepo = examinationRepo;
        }

        private List<DateTime> GetFreeDates(Doctor doctor, int maxDates)
        {
            throw new NotImplementedException();
        }

        public void CreateExam(Examination exam)
        {
            _examinationRepo.NewExamination(exam);
        }

        public void RemoveExam(Examination exam)
        {
            _examinationRepo.DeleteExamination(exam);
        }

        public void EditExams(string id, Examination exam)
        {
            _examinationRepo.DoctorEditExamination(id,exam);
        }

        public List<Examination> ReadPatientExams(String patientId)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Doctor> GetDoctors()
        {
            return _doctorRepo.GetAllDoctors();
        }

        public Doctor GetDoctor(string id)
        {
            return _doctorRepo.GetDoctor(id);
        }

        public List<Examination> GetFreeExaminations(Doctor doctor, DateTime startDate, DateTime endDate, bool priority)
        {
            ObservableCollection<Doctor> doctors = _doctorRepo.GetAllDoctors();
            return _examinationRepo.GetFreeExaminationsForDoctor(doctor, startDate, endDate, priority, doctors);
        }

        public List<Examination> MoveExaminations(Examination examination)
        {
            return _examinationRepo.MoveExamination(examination);
        }

        public ObservableCollection<Examination> ReadMyExams(string id)
        {
            return _examinationRepo.ExaminationsForDoctor(id);
        }

        public ObservableCollection<Examination> ReadEndedExams()
        {
            return _examinationRepo.ReadEndedExams();
        }

        public bool occupiedDate(DateTime dt)
        {
            return _examinationRepo.occupiedDate(dt);
        }
    }
}