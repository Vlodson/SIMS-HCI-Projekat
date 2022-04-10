using System;
using Service;
using System.Collections.ObjectModel;
using Model;

namespace Controller
{
   public class PatientController
   {
            
      private PatientAccountService patientAccService;

      public PatientController(PatientAccountService patientAccService)
        {
            this.patientAccService = patientAccService;
        }

      public bool CreatePatient(String id, String name, String surname, DateTime doB, ObservableCollection<Examination> examinations)
      {
            patientAccService.CreatePatient(id, name, surname, doB, examinations);
            return true;
      }
      
      public bool RemovePatient(String patientId)
      {
            patientAccService.RemovePatient(patientId);
            return true;
      }
      
      public void EditPatient(String patientId, String newName, String newSurname, DateTime newDoB, ObservableCollection<Examination> examinations)
      {
            patientAccService.EditPatient(patientId, newName, newSurname, newDoB, examinations);
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
            return patientAccService.ReadPatient(patientId);
      }

      public ObservableCollection<Patient> ReadAllPatients()
      {
            return patientAccService.ReadAllPatients();
      }
      
      public bool UpgradeGuest(String guestId, String name, String surname, DateTime doB)
      {
            patientAccService.UpgradeGuest(guestId, name, surname, doB);
            return true;
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

        internal void CreatePatient(string uCIN, string name, string surname, string dob)
        {
            throw new NotImplementedException();
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