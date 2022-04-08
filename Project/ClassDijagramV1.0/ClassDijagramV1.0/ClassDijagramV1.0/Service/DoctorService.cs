//using ClassDijagramV1._0.Repository;
using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
   public class DoctorService
   {


        private readonly DoctorRepo _doctorRepo;

        public DoctorService(DoctorRepo doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        private List<DateTime> GetFreeDates(Doctor doctor, int maxDates)
      {
         throw new NotImplementedException();
      }
      
      public bool CreateExam(Model.Patient patient, Doctor doctor, Model.Room examRoom, DateTime date)
      {
         throw new NotImplementedException();
      }
      
      public bool RemoveExam(String examId)
      {
         throw new NotImplementedException();
      }
      
      public void EditExams(String examId, Model.Room newExamRoom, DateTime newDate)
      {
         throw new NotImplementedException();
      }
      
      public List<Examination> ReadPatientExams(String patientId)
      {
         throw new NotImplementedException();
      }

        //dodato

        public List<Doctor> GetDoctors()
        {
            return _doctorRepo.GetAllDoctors();
        }
   
   }
}