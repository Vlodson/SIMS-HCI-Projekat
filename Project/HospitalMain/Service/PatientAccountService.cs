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

      public bool CreatePatient(String id, String ucin, String name, String surname, DateTime doB, ObservableCollection<Examination> examinations)
      {
            Guest guest = new Guest(id);
            patientRepo.NewPatient(new Patient(guest.ID, ucin, name, surname, doB, examinations));
            return true;
      }
      
      public bool RemovePatient(String patientId)
      {
            patientRepo.DeletePatient(patientId);
            return true;
      }
      
      public void EditPatient(String patientId, String ucin, String newName, String newSurname, DateTime newDoB, ObservableCollection<Examination> examinations)
      {
            Guest guest = new Guest(patientId);
            patientRepo.SetPaetient(patientId, new Patient(guest.ID, ucin, newName, newSurname, newDoB, examinations));
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
            return patientRepo.GetPatient(patientId);
      }

      public ObservableCollection<Patient> ReadAllPatients()
        {
            return patientRepo.GetAllPatients();
        }
      
      public bool UpgradeGuest(String guestId, String ucin, String name, String surname, DateTime doB)
      {
            Guest guest = new Guest("guestID");
            Patient patient = new Patient(guest.ID, ucin, name, surname, doB, new ObservableCollection<Examination>());
            patientRepo.NewPatient(patient);
            return true;
      }
   
   }
}