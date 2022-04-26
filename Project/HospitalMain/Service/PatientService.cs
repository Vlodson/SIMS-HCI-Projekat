using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class PatientService
    {
        //dodato
        private readonly PatientRepo _patientRepo;
        private readonly ExaminationRepo _examinationRepo;

        public PatientService(PatientRepo patientRepo, ExaminationRepo examinationRepo)
        {
            _patientRepo = patientRepo;
            _examinationRepo = examinationRepo;
        }

        public int generateID (ObservableCollection<Examination> examinations)
        {
            return _examinationRepo.generateID(examinations);
        }

        public ObservableCollection<Examination> getAllExaminations()
        {
            return _examinationRepo.GetAll();
        }

        public void SaveExaminationRepo()
        {
            _examinationRepo.SaveExamination();
        }

        private List<DateTime> GetFreeDates(Doctor doctor, int maxDates)
        {
            throw new NotImplementedException();
        }

        public Model.Patient GetPatient(String id)
        {
            return _patientRepo.GetPatient(id);
        }

        public void CreateExam(Examination examination)
        {
            _examinationRepo.NewExamination(examination);
        }

        public void RemoveExam(Examination examination)
        {
             _examinationRepo.DeleteExamination(examination);
        }

        public void SetExam(string examID, Examination examination)
        {
            _examinationRepo.SetExamination(examID, examination);
        }

        public Examination GetExam(string examID)
        {
            return _examinationRepo.GetExamination(examID);
        }

        public void EditExam(String examId, DateTime newDate)
        {
            _examinationRepo.EditExamination(examId, newDate);
        }

        public ObservableCollection<Examination> ReadMyExams(string id)
        {
            //dodato
            List<Examination> others = new List<Examination>();
            foreach(Examination exam in _examinationRepo.GetAll())
            {
                if (!exam.PatientId.Equals(id))
                {
                    others.Add(exam);
                }
            }

            foreach(Examination exam in others)
            {
                _examinationRepo.DeleteByPatient(exam.Id);
            }

            //return _examinationRepo.ExaminationsForPatient(id);
            return _examinationRepo.GetAll();
        }

        public ObservableCollection<Model.Patient> GetPatients()
        {
            return _patientRepo.GetAllPatients();
        }

        public ObservableCollection<Examination> GetExaminations()
        {
            return _examinationRepo.GetAll();
        }

    }
}