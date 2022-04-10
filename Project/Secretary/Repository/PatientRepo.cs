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
      private String dbPath;
      private ObservableCollection<Patient> patients;
      
      public PatientRepo(String dbPath)
      {
            this.dbPath = dbPath;
            this.patients = new ObservableCollection<Patient>();

            //Patient p1 = new Patient("1", "Pera", "Peric", new DateTime(1994, 05, 06), new ObservableCollection<Examination>());
            //Patient p2 = new Patient("2", "Ivan", "Ivic", new DateTime(1985, 08, 08), new ObservableCollection<Examination>());
            //Patient p3 = new Patient("3", "Zika", "Zikic", new DateTime(2001, 11, 11), new ObservableCollection<Examination>());

            //this.patients.Add(p1);
            //this.patients.Add(p2);
            //this.patients.Add(p3);
        }

      public bool NewPatient(Patient patient)
      {
         for (int i = 0; i < patients.Count; i++)
            {
                if(patients[i].ID == patient.ID)
                {
                    return false;
                }
            }

         patients.Add(patient);
         return true;
      }
      
      public Patient GetPatient(String patientId)
      {
         for (int i = 0; i < patients.Count; i++)
            {
                if (patients[i].ID == patientId)
                {
                    return patients[i];
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
                    patients[i].Name = newPatient.Name;
                    patients[i].Surname = newPatient.Surname;
                    patients[i].DoB = newPatient.DoB;
                    patients[i].examinations = newPatient.examinations;
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
      
      public PatientAccountService patientAccountService;
   
   }
}