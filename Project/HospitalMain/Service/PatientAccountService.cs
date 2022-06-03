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

      public bool CreatePatient(Patient newPatient)
      {
            return patientRepo.NewPatient(newPatient);
      }

      public bool CreateGuest(Patient guest)
      {
            return patientRepo.NewPatient(guest);
      }
      
      public bool RemovePatient(String patientId)
      {
            return patientRepo.DeletePatient(patientId);
      }
      
      public void EditPatient(Patient patient)
      {
            patientRepo.SetPaetient(patient.ID, patient);
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
      
      public bool UpgradeGuest(Patient patient)
      {
            return patientRepo.NewPatient(patient);
      }
   
   }
}