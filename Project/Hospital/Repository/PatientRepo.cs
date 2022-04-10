using System;
using System.Collections.Generic;
using Model;
using Service;

namespace Repository
{
   public class PatientRepo
   {
      private String dbPath;
      private List<Patient> patients;
      
      public PatientRepo(String dbPath)
      {
            this.dbPath = dbPath;
            this.patients = new List<Patient>();

            Patient p1 = new Patient("1", "Pera", "Peric", new DateTime(1994, 05, 06), new List<Examination>());
            Patient p2 = new Patient("2", "Ivan", "Ivic", new DateTime(1985, 08, 08), new List<Examination>());
            Patient p3 = new Patient("3", "Zika", "Zikic", new DateTime(2001, 11, 17), new List<Examination>());

            this.patients.Add(p1);
            this.patients.Add(p2);
            this.patients.Add(p3);
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

      public List<Patient> GetAllPatients()
        {
            List<Patient> allPatients = new List<Patient>();
            for (int i = 0; i < patients.Count; i++)
            {
                allPatients.Add(patients[i]);
            }
            return allPatients;
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
         throw new NotImplementedException();
      }
      
      public bool SavePatient()
      {
         throw new NotImplementedException();
      }
      
      public PatientAccountService patientAccountService;
   
   }
}