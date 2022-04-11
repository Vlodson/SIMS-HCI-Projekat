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

        private List<DateTime> GetFreeDates(Model.Doctor doctor, int maxDates)
        {
            throw new NotImplementedException();
        }

        public bool CreateExam(Model.Patient patient, Model.Doctor doctor, Model.Room examRoom, DateTime date)
        {
            throw new NotImplementedException();
        }

        public void RemoveExam(Examination exam)
        {
            _examinationRepo.DeleteExamination(exam);
        }

        public void EditExams(Examination exam)
        {
            _examinationRepo.SetExamination(exam);
        }

        public List<Examination> ReadPatientExams(String patientId)
        {
            throw new NotImplementedException();
        }

        //dodato

        public List<Model.Doctor> GetDoctors()
        {
            return _doctorRepo.GetAllDoctors();
        }

        public Model.Doctor GetDoctor(string id)
        {
            return _doctorRepo.GetDoctor(id);
        }

        public List<DateTime> GetFreeExaminations(Model.Doctor doctor)
        {
            return _examinationRepo.GetFreeExaminationsForDoctor(doctor);
        }

        public ObservableCollection<Examination> ReadMyExams(string id)
        {
            return _examinationRepo.ExaminationsForDoctor(id);
        }

        public List<Examination> ReadAll(string id)
        {
            return _examinationRepo.ReadAll(id);
        }

        public void CreateExam(Examination examination)
        {
            _examinationRepo.SetExamination(examination);
        }

    }
}