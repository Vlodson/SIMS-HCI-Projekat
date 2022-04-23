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
      public String dbPath { get; set; }
      public ObservableCollection<Patient> patients { get; set; }
      
      public PatientRepo(String dbPath)
      {
            this.dbPath = dbPath;
            this.patients = new ObservableCollection<Patient>();
            Guest guest = new Guest("1");

            Patient p1 = new Patient(guest.ID, "0605994802463", "Pera", "Peric", "0605235548", "pera@mail.com", "Partizanska 13, Novi Sad", Gender.Male, new DateTime(1994, 05, 06), new MedicalRecord(),  new ObservableCollection<Examination>());
            Patient p2 = new Patient("2", "0808985802463", "Ivan", "Ivic", "0605234548", "ivan@mail.com", "Partizanska 14, Novi Sad", Gender.Male, new DateTime(1985, 08, 08), new MedicalRecord(new ObservableCollection<Report>()),  new ObservableCollection<Examination>());
            Patient p3 = new Patient("3", "1111001802463", "Zika", "Zikic", "0605235598", "zika@mail.com", "Partizanska 15, Novi Sad", Gender.Male, new DateTime(2001, 11, 11), new MedicalRecord(),  new ObservableCollection<Examination>());

            this.patients.Add(p1);
            this.patients.Add(p2);
            this.patients.Add(p3);
        }

        public PatientRepo(string dbPath, ObservableCollection<Patient> patientCollection)
        {
            this.dbPath=dbPath;
            this.patients = patientCollection;
            Guest guest = new Guest("123");
            Patient p1 = new Patient(guest.ID, "0111000802463","Jelena", "Dinic", "0615235548", "jelena@mail.com", "Partizanska 23, Novi Sad", Gender.Female, new DateTime(2000, 11, 1), new MedicalRecord(),  new ObservableCollection<Examination>());
            this.patients.Add(p1);
        }

      public bool NewPatient(Patient patient)
      {

         foreach (Patient _patient in patients)
            {
                if (_patient.ID.Equals(patient.ID))
                {
                    return false;
                }
            }

         patients.Add(patient);
         return true;
      }
      
      public Patient GetPatient(String patientId)
      {
            foreach (Patient patient in patients)
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
            return patients;
        }
      
      public void SetPaetient(String patientId, Patient newPatient)
      {
         for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].ID == patientId)
                {
                    patients[i].ID = newPatient.ID;
                    patients[i].UCIN = newPatient.UCIN;
                    patients[i].Name = newPatient.Name;
                    patients[i].Surname = newPatient.Surname;
                    patients[i].PhoneNumber = newPatient.PhoneNumber;
                    patients[i].Mail = newPatient.Mail;
                    patients[i].Adress = newPatient.Adress;
                    patients[i].Gender = newPatient.Gender;
                    patients[i].DoB = newPatient.DoB;
                    patients[i].Examinations = newPatient.Examinations;
                    break;
                }
            }
      }
      
      public bool DeletePatient(String patientId)
      {
         for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].ID == patientId)
                {
                    patients.RemoveAt(i);
                    return true;
                }
            }

         return false;
      }
      
      public bool LoadPatient()
      {
            using FileStream fileStream = File.OpenRead(dbPath);
            this.patients = JsonSerializer.Deserialize<ObservableCollection<Patient>>(fileStream);
            return true;
      }
      
      public bool SavePatient()
      {
            string jsonString = JsonSerializer.Serialize(patients);
            File.WriteAllText(dbPath, jsonString);
            return true;
      }
      
      //public PatientAccountService patientAccountService;
   
   }
}