using System;

namespace Service
{
   public class PatientAccountService
   {
      public bool CreatePatient(String id, String name, String surname, DateTime doB)
      {
         throw new NotImplementedException();
      }
      
      public bool RemovePatient(String patientId)
      {
         throw new NotImplementedException();
      }
      
      public void EditPatient(String patientId, String newName, String newSurname, DateTime newDoB)
      {
         throw new NotImplementedException();
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
         throw new NotImplementedException();
      }
      
      public bool UpgradeGuest(String guestId, String name, String surname, DateTime doB)
      {
         throw new NotImplementedException();
      }
   
   }
}