using System;
using Repository;
using Model;
using System.Collections.Generic;

namespace Service
{
   public class PatientAccountService
   {

      private readonly PatientRepo patientRepo;

      public PatientAccountService(PatientRepo patientRepo)
      {
            this.patientRepo = patientRepo;
      }

      public bool CreatePatient(String id, String name, String surname, DateTime doB)
      {
            patientRepo.NewPatient(new Patient(id, name, surname, doB, new List<Examination>()));
            return true;
      }
      
      public bool RemovePatient(String patientId)
      {
            patientRepo.DeletePatient(patientId);
            return true;
      }
      
      public void EditPatient(String patientId, String newName, String newSurname, DateTime newDoB, List<Examination> examinations)
      {
            patientRepo.SetPaetient(patientId, new Patient(patientId, newName, newSurname, newDoB, examinations));
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
            return patientRepo.GetPatient(patientId);
      }

      public List<Patient> ReadAllPatients()
        {
            return patientRepo.GetAllPatients();
        }
      
      public bool UpgradeGuest(String guestId, String name, String surname, DateTime doB)
      {
            Patient patient = new Patient(guestId, name, surname, doB, new List<Examination>());
            patientRepo.NewPatient(patient);
            return true;
      }
   
   }
}