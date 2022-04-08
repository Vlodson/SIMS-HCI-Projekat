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

        private List<DateTime> GetFreeDates(Doctor doctor, int maxDates)
      {
         throw new NotImplementedException();
      }

        public Patient GetPatient(String id)
        {
            return _patientRepo.GetPatient(id);
        }
      
      public void CreateExam(Examination examination)
      {
             _examinationRepo.SetExamination(examination);
      }
      
      public bool RemoveExam(String examId)
      {
         throw new NotImplementedException();
      }
      
      public void EditExam(String examId, DateTime newDate)
      {
         throw new NotImplementedException();
      }
      
      public ObservableCollection<Examination> ReadMyExams(string id)
      {
            return _examinationRepo.ExaminationsForPatient(id);
      }
   
   }
}