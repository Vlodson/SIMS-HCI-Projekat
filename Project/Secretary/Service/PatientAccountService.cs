using System;
using Repository;
using Model;
using System.Collections.ObjectModel;

namespace Service
{
   public class PatientAccountService
   {

      private readonly PatientRepo patientRepo;

      public PatientAccountService(PatientRepo patientRepo)
      {
            this.patientRepo = patientRepo;
      }

      public bool CreatePatient(String id, String name, String surname, DateTime doB, ObservableCollection<Examination> examinations)
      {
            patientRepo.NewPatient(new Patient(id, name, surname, doB, examinations));
            return true;
      }
      
      public bool RemovePatient(String patientId)
      {
            patientRepo.DeletePatient(patientId);
            return true;
      }
      
      public void EditPatient(String patientId, String newName, String newSurname, DateTime newDoB, ObservableCollection<Examination> examinations)
      {
            patientRepo.SetPaetient(patientId, new Patient(patientId, newName, newSurname, newDoB, examinations));
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
            return patientRepo.GetPatient(patientId);
      }

      public ObservableCollection<Patient> ReadAllPatients()
        {
            return patientRepo.GetAllPatients();
        }
      
      public bool UpgradeGuest(String guestId, String name, String surname, DateTime doB)
      {
            Patient patient = new Patient(guestId, name, surname, doB, new ObservableCollection<Examination>());
            patientRepo.NewPatient(patient);
            return true;
      }
   
   }
}