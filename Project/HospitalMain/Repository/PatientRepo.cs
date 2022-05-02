using System;
using System.Collections.ObjectModel;
using Model;
using Service;
using System.IO;
using System.Text.Json;
using HospitalMain.Enums;

namespace Repository
{
   public class PatientRepo
   {
      public String DBPath { get; set; }
      public ObservableCollection<Patient> Patients { get; set; }
      
      public PatientRepo(String dbPath)
      {

            this.DBPath = dbPath;
            this.Patients = new ObservableCollection<Patient>();
            Guest guest = new Guest("1");

            Patient p1 = new Patient(guest.ID, "0605994802463", "Pera", "Peric", "0605235548", "pera@mail.com", "Partizanska 13, Novi Sad", Gender.Male, new DateTime(1994, 05, 06), "1");
            Patient p2 = new Patient("2", "0808985802463", "Ivan", "Ivic", "0605234548", "ivan@mail.com", "Partizanska 14, Novi Sad", Gender.Male, new DateTime(1985, 08, 08), "2");
            Patient p3 = new Patient("3", "1111001802463", "Zika", "Zikic", "0605235598", "zika@mail.com", "Partizanska 15, Novi Sad", Gender.Male, new DateTime(2001, 11, 11), "3");

            this.Patients.Add(p1);
            this.Patients.Add(p2);
            this.Patients.Add(p3);

            if (File.Exists(dbPath))
                LoadPatient();
        }

       public PatientRepo(string dbPath, ObservableCollection<Patient> patientCollection)
       {
            this.DBPath=dbPath;
            this.Patients = patientCollection;
            Guest guest = new Guest("123");
            Patient p1 = new Patient(guest.ID, "0111000802463","Jelena", "Dinic", "0615235548", "jelena@mail.com", "Partizanska 23, Novi Sad", Gender.Female, new DateTime(2000, 11, 1), "4");
            this.Patients.Add(p1);
       }


      public bool NewPatient(Patient patient)
      {

         foreach (Patient _patient in Patients)
            {
                if (_patient.ID.Equals(patient.ID))
                {
                    return false;
                }
            }

         Patients.Add(patient);
         SavePatient();
         return true;
      }

      public int generateID()
      {
            int maxID = 0;
            ObservableCollection<Patient> patients = Patients;

            foreach(Patient patient in patients)
            {
                int patientID = Int32.Parse(patient.ID);
                if(patientID > maxID)
                {
                    maxID = patientID;
                }
            }

            return maxID + 1;
      }
      
      public Patient GetPatient(String patientId)
      {
            foreach (Patient patient in Patients)
            {
                if (patient.ID.Equals(patientId))
                {
                    return patient;
                }
            }

            return null;
      }

      public ObservableCollection<Patient> GetAllPatients()
        {
            return Patients;
        }
      
      public void SetPaetient(String patientId, Patient newPatient)
      {
         for (int i = 0; i < Patients.Count; i++)
            {
                if (Patients[i].ID.Equals(patientId))
                {
                    Patients[i].ID = newPatient.ID;
                    Patients[i].UCIN = newPatient.UCIN;
                    Patients[i].Name = newPatient.Name;
                    Patients[i].Surname = newPatient.Surname;
                    Patients[i].PhoneNumber = newPatient.PhoneNumber;
                    Patients[i].Mail = newPatient.Mail;
                    Patients[i].Adress = newPatient.Adress;
                    Patients[i].Gender = newPatient.Gender;
                    Patients[i].DoB = newPatient.DoB;
                    Patients[i].MedicalRecordID = newPatient.MedicalRecordID;
                    break;
                }
            }
            SavePatient();
        }
      
      public bool DeletePatient(String patientId)
      {
         for (int i = 0; i < Patients.Count; i++)
            {
                if (Patients[i].ID.Equals(patientId))
                {
                    Patients.RemoveAt(i);
                    SavePatient();
                    return true;
                }
            }

            SavePatient();
            return false;
      }
      
      public bool LoadPatient()
      {
            using FileStream fileStream = File.OpenRead(DBPath);
            Patients = JsonSerializer.Deserialize<ObservableCollection<Patient>>(fileStream);
            return true;
      }
      
      public bool SavePatient()
      {
            string jsonString = JsonSerializer.Serialize(Patients);
            File.WriteAllText(DBPath, jsonString);
            return true;
      }
      
      //public PatientAccountService patientAccountService;
   
   }
}