using System;
using Repository;
using Model;
using System.Collections.ObjectModel;
using HospitalMain.Enums;
using System.Collections.Generic;
using HospitalMain.Model;

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

      public int generateID()
        {
            return patientRepo.generateID();
        }

      public bool CreatePatient(String id, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID, List<Answer> answers)
      {
            Guest guest = new Guest(id);
            return patientRepo.NewPatient(new Patient(guest.ID, ucin, name, surname, phoneNum, mail, adress, gender, doB, medicalRecordID, answers, DateTime.Now.ToString("MM"), 0, 0));
      }
      
      public bool RemovePatient(String patientId)
      {
            return patientRepo.DeletePatient(patientId);
      }
      
      public void EditPatient(String patientId, String ucin, String newName, String newSurname, String newPhoneNum, String newMail, String newAdress, Gender newGender, DateTime newDoB, String newMedicalRecordID, List<Answer> answers, String currentMonth, int numberCanceling, int numberNewExams)
      {
            //Guest guest = new Guest(patientId);
            patientRepo.SetPaetient(patientId, new Patient(patientId, ucin, newName, newSurname, newPhoneNum, newMail, newAdress, newGender, newDoB, newMedicalRecordID, answers, currentMonth, numberCanceling, numberNewExams));
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
            return patientRepo.GetPatient(patientId);
      }

      public ObservableCollection<Patient> ReadAllPatients()
        {
            return patientRepo.GetAllPatients();
        }
      
      public bool UpgradeGuest(String guestId, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID, List<Answer> answers)
      {
            Guest guest = new Guest("guestID");
            Patient patient = new Patient(guest.ID, ucin, name, surname, phoneNum, mail, adress, gender, doB, medicalRecordID, answers, DateTime.Now.ToString("MM"), 0, 0);
            return patientRepo.NewPatient(patient);
      }
   
   }
}