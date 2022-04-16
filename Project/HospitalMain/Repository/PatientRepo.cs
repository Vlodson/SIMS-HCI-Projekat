using System;
using System.Collections.ObjectModel;
using Model;
using Service;
using System.IO;
using System.Text.Json;

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

            Patient p1 = new Patient(guest.ID, "0605994802463", "Pera", "Peric", new DateTime(1994, 05, 06), new ObservableCollection<Examination>());
            Patient p2 = new Patient("2", "0808985802463", "Ivan", "Ivic", new DateTime(1985, 08, 08), new ObservableCollection<Examination>());
            Patient p3 = new Patient("3", "1111001802463", "Zika", "Zikic", new DateTime(2001, 11, 11), new ObservableCollection<Examination>());

            this.patients.Add(p1);
            this.patients.Add(p2);
            this.patients.Add(p3);
        }

        public PatientRepo(string dbPath, ObservableCollection<Patient> patientCollection)
        {
            this.dbPath=dbPath;
            this.patients = patientCollection;
            Guest guest = new Guest("123");
            Patient p1 = new Patient(guest.ID, "0111000802463","Jelena", "Dinic", new DateTime(2000, 11, 1), new ObservableCollection<Examination>());
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