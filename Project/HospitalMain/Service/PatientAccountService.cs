using System;
using Repository;
using Model;
using System.Collections.ObjectModel;
using HospitalMain.Enums;

namespace Service
{
   public class PatientAccountService
   {

      private readonly PatientRepo patientRepo;
      private readonly MedicalRecordService medicalRecordService;

        public PatientAccountService(PatientRepo patientRepo)
        {
            this.patientRepo = patientRepo;

        }

      public bool CreatePatient(String id, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID, ObservableCollection<Examination> examinations)
      {
            Guest guest = new Guest(id);
            return patientRepo.NewPatient(new Patient(guest.ID, ucin, name, surname, phoneNum, mail, adress, gender, doB, medicalRecordID,  examinations));
      }
      
      public bool RemovePatient(String patientId)
      {
            return patientRepo.DeletePatient(patientId);
      }
      
      public void EditPatient(String patientId, String ucin, String newName, String newSurname, String newPhoneNum, String newMail, String newAdress, Gender newGender, DateTime newDoB, String newMedicalRecordID, ObservableCollection<Examination> examinations)
      {
            Guest guest = new Guest(patientId);
            patientRepo.SetPaetient(patientId, new Patient(guest.ID, ucin, newName, newSurname, newPhoneNum, newMail, newAdress, newGender, newDoB, newMedicalRecordID, examinations));
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
            return patientRepo.GetPatient(patientId);
      }

      public ObservableCollection<Patient> ReadAllPatients()
        {
            return patientRepo.GetAllPatients();
        }
      
      public bool UpgradeGuest(String guestId, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID)
      {
            Guest guest = new Guest("guestID");
            Patient patient = new Patient(guest.ID, ucin, name, surname, phoneNum, mail, adress, gender, doB, medicalRecordID,  new ObservableCollection<Examination>());
            return patientRepo.NewPatient(patient);
      }
   
   }
}