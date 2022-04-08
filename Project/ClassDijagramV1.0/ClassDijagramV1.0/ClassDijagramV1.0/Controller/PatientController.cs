using Service;
using System;

namespace Controller
{
   public class PatientController
   {
        private PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

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
         return _patientService.GetPatient(patientId);
      }
      
      public bool UpgradeGuest(String guestId, String name, String surname, DateTime doB)
      {
         throw new NotImplementedException();
      }
      
      public System.Collections.ArrayList patientAccountService;
      
      
      public System.Collections.ArrayList PatientAccountService
      {
         get
         {
            if (patientAccountService == null)
               patientAccountService = new System.Collections.ArrayList();
            return patientAccountService;
         }
         set
         {
            RemoveAllPatientAccountService();
            if (value != null)
            {
               foreach (Service.PatientAccountService oPatientAccountService in value)
                  AddPatientAccountService(oPatientAccountService);
            }
         }
      }
      
      
      public void AddPatientAccountService(Service.PatientAccountService newPatientAccountService)
      {
         if (newPatientAccountService == null)
            return;
         if (this.patientAccountService == null)
            this.patientAccountService = new System.Collections.ArrayList();
         if (!this.patientAccountService.Contains(newPatientAccountService))
            this.patientAccountService.Add(newPatientAccountService);
      }
      
      
      public void RemovePatientAccountService(Service.PatientAccountService oldPatientAccountService)
      {
         if (oldPatientAccountService == null)
            return;
         if (this.patientAccountService != null)
            if (this.patientAccountService.Contains(oldPatientAccountService))
               this.patientAccountService.Remove(oldPatientAccountService);
      }
      
      
      public void RemoveAllPatientAccountService()
      {
         if (patientAccountService != null)
            patientAccountService.Clear();
      }
   
   }
}