using System;
using Service;
using System.Collections.ObjectModel;
using Model;
using HospitalMain.Enums;

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

       

      public bool CreatePatient(String id, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID, ObservableCollection<Examination> examinations)
      {
            return patientAccService.CreatePatient(id, ucin, name, surname, phoneNum, mail, adress, gender, doB, medicalRecordID, examinations);
      }
      
      public bool RemovePatient(String patientId)
      {
            return patientAccService.RemovePatient(patientId);
      }
      
      public void EditPatient(String patientId, String newUCIN, String newName, String newSurname, String newPhoneNum, String newMail, String newAdress, Gender newGender, DateTime newDoB, String medicalRecordID, ObservableCollection<Examination> examinations)
      {
            patientAccService.EditPatient(patientId, newUCIN, newName, newSurname, newPhoneNum, newMail, newAdress, newGender, newDoB, medicalRecordID, examinations);
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
            return patientAccService.ReadPatient(patientId);
      }

      public ObservableCollection<Patient> ReadAllPatients()
      {
            return patientAccService.ReadAllPatients();
      }
      
      public bool UpgradeGuest(String guestId,  String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID)
      {
            return patientAccService.UpgradeGuest(guestId, ucin, name, surname, phoneNum, mail, adress, gender, doB, medicalRecordID);
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