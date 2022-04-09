using Model;
using Service;
using System;

namespace Repository
{
   public class PatientRepo
   {
      private String dbPath;
      
      public bool NewPatient(Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public Patient GetPatient(String patientId)
      {
         throw new NotImplementedException();
      }
      
      public void SetPaetient(String patientId, Patient newPatient)
      {
         throw new NotImplementedException();
      }
      
      public bool DeletePaetient(String patientId)
      {
         throw new NotImplementedException();
      }
      
      public bool LoadPatient()
      {
         throw new NotImplementedException();
      }
      
      public bool SavePatient()
      {
         throw new NotImplementedException();
      }
      
      public PatientAccountService patientAccountService;
   
   }
}