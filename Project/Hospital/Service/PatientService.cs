using Model;
using System;
using System.Collections.Generic;

namespace Service
{
   public class PatientService
   {
      private List<DateTime> GetFreeDates(Doctor doctor, int maxDates)
      {
         throw new NotImplementedException();
      }
      
      public bool CreateExam(Model.Patient patient, DateTime date, DoctorType examType)
      {
         throw new NotImplementedException();
      }
      
      public bool RemoveExam(String examId)
      {
         throw new NotImplementedException();
      }
      
      public void EditExam(String examId, DateTime newDate)
      {
         throw new NotImplementedException();
      }
      
      public List<Examination> ReadMyExams()
      {
         throw new NotImplementedException();
      }
   
   }
}