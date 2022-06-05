using System;
using Service;
using System.Collections.ObjectModel;
using Model;
using HospitalMain.Enums;
using System.Collections.Generic;
using HospitalMain.Model;

namespace Controller
{
   public class PatientController
   {
            
      private PatientAccountService patientAccService;
      private PatientService patientService;

      public PatientController(PatientAccountService patientAccService)
        {
            this.patientAccService = patientAccService;
        }

      public PatientController(PatientService patientService, PatientAccountService patientAccountService)
        {
            this.patientService = patientService;
            this.patientAccService = patientAccountService;
        }

      public int generateID()
        {
            return patientAccService.generateID();
        } 

      public bool CreatePatient(Patient newPatient)
      {
            return patientAccService.CreatePatient(newPatient);
      }
      
      public bool CreateGuest(Patient guest)
      {
            return patientAccService.CreateGuest(guest);
      }

      public bool RemovePatient(String patientId)
      {
            return patientAccService.RemovePatient(patientId);
      }
      
      public void EditPatient(Patient patient)
      {
            patientAccService.EditPatient(patient);
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
            return patientAccService.ReadPatient(patientId);
      }

      public ObservableCollection<Patient> ReadAllPatients()
      {
            return patientAccService.ReadAllPatients();
      }
      
      public bool UpgradeGuest(Patient patient)
      {
            return patientAccService.UpgradeGuest(patient);
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

        public List<Examination> GetExamByTime(DateTime dateTime)
        {
            return patientService.GetExamByTime(dateTime);
        }

        public List<String> GetPatientsDoctors(String patientId)
        {
            return patientService.GetPatientsDoctors(patientId);
        }

        public bool CheckStatusCancelled(String id)
        {
            return patientService.CheckStatusCancelled(id);
        }

        public bool CheckStatusAdded(String id)
        {
            return patientService.CheckStatusAdded(id);
        }
    }
}