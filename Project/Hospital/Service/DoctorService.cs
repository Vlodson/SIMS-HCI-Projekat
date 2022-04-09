using System;

namespace Service
{
   public class DoctorService
   {
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
   
   }
}