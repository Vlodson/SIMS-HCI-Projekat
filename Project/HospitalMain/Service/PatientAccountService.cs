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
            int maxID = 0;
            ObservableCollection<Patient> patients = patientRepo.Patients;

            foreach (Patient patient in patients)
            {
                int patientID = Int32.Parse(patient.ID);
                if (patientID > maxID)
                {
                    maxID = patientID;
                }
            }

            return maxID + 1;
      }

      public bool CreatePatient(String id, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID, List<Answer> answers)
      {
            return patientRepo.NewPatient(new Patient(id, ucin, name, surname, phoneNum, mail, adress, gender, doB, medicalRecordID, false, answers, DateTime.Now.ToString("MM"), 0, 0));
      }

      public bool CreateGuest(String id, String name, String surname, Gender gender, List<Answer> answers)
      {
            return patientRepo.NewPatient(new Patient(id, "", name, surname, "", "", "", gender, new DateTime(), "", true, answers, DateTime.Now.ToString("MM"), 0, 0));
      }
      
      public bool RemovePatient(String patientId)
      {
            return patientRepo.DeletePatient(patientId);
      }
      
      public void EditPatient(String patientId, String ucin, String newName, String newSurname, String newPhoneNum, String newMail, String newAdress, Gender newGender, DateTime newDoB, String newMedicalRecordID, List<Answer> answers, String currentMonth, int numberCanceling, int numberNewExams)
      {
            patientRepo.SetPaetient(patientId, new Patient(patientId, ucin, newName, newSurname, newPhoneNum, newMail, newAdress, newGender, newDoB, newMedicalRecordID, false, answers, currentMonth, numberCanceling, numberNewExams));
      }
      
      public Model.Patient ReadPatient(String patientId)
      {
            foreach (Patient patient in patientRepo.Patients)
            {
                if (patient.ID.Equals(patientId))
                {
                    return patient;
                }
            }

            return null;
      }

      public ObservableCollection<Patient> ReadAllPatients()
      {
            return patientRepo.Patients;
      }
      
      public bool UpgradeGuest(String guestId, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime doB, String medicalRecordID, List<Answer> answers)
      {
            Patient patient = new Patient(guestId, ucin, name, surname, phoneNum, mail, adress, gender, doB, medicalRecordID, false, answers, DateTime.Now.ToString("MM"), 0, 0);
            return patientRepo.NewPatient(patient);
      }
   
   }
}