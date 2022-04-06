using Model;
using Service;
using System;

namespace Repository
{
   public class ExaminationRepo
   {
      private String dbPath;
      
      public bool NewExamination(Examination examination)
      {
         throw new NotImplementedException();
      }
      
      public Examination GetExamination(String examId)
      {
         throw new NotImplementedException();
      }
      
      public void SetExamination(String examId, Examination newExam)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteExamination(String examId)
      {
         throw new NotImplementedException();
      }
      
      public bool LoadExamination()
      {
         throw new NotImplementedException();
      }
      
      public bool SaveExamination()
      {
         throw new NotImplementedException();
      }
      
      public PatientService patientService;
      public DoctorService doctorService;
   
   }
}