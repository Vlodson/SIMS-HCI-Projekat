
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository
{
    public class MedicalRecordRepo
    {
        public String DBPath { get; set; }
        public ObservableCollection<MedicalRecord> MedicalRecords { get; set; }

        public MedicalRecordRepo(String dbPath)
        {
            this.DBPath = dbPath;
            this.MedicalRecords = new ObservableCollection<MedicalRecord>();
          
            Therapy therapy1 = new Therapy("t1", "lek1", 5, 2, true);
            Therapy therapy2 = new Therapy("t2", "lek2", 5, 2, false);
            Therapy therapy3 = new Therapy("t3", "lek3", 5, 2, true);
            ObservableCollection<Therapy> ts = new ObservableCollection<Therapy>();
            ts.Add(therapy1);
            ts.Add(therapy2);
            ts.Add(therapy3);
          
            Report report = new Report("examId", "Neki opis", new DateTime(), "p1", "d1", ts);
            ObservableCollection<Report> reports = new ObservableCollection<Report>();
            reports.Add(report);
          
            MedicalRecord mr = new MedicalRecord("4", "ucin", "ime", "prezime", "4098240", "mejl", "adresa", Enums.Gender.Male, new DateTime(), new Doctor(), "A", reports, new ObservableCollection<string>());
            this.MedicalRecords.Add(mr);
        }

        public bool NewMedicalRecord(MedicalRecord medRecord)
        {
            foreach(MedicalRecord oneMedRecord in MedicalRecords)
            {
                if (oneMedRecord.ID.Equals(medRecord.ID))
                {
                    return false;
                }
            }
            MedicalRecords.Add(medRecord);
            return true;
        }

        public void EditMedicalRecord(String medRecordID, MedicalRecord medRecord)
        {
            foreach(MedicalRecord oneMedRecord in MedicalRecords)
            {
                if (medRecord.ID.Equals(medRecordID))
                {
                    oneMedRecord.UCIN = medRecord.UCIN;
                    oneMedRecord.Name = medRecord.Name;
                    oneMedRecord.Surname = medRecord.Surname;
                    oneMedRecord.Adress = medRecord.Adress;
                    oneMedRecord.PhoneNumber = medRecord.PhoneNumber;
                    oneMedRecord.BloodType = medRecord.BloodType;
                    oneMedRecord.Gender = medRecord.Gender;
                    oneMedRecord.Mail = medRecord.Mail;
                    oneMedRecord.DoB = medRecord.DoB;
                    oneMedRecord.Doctor = medRecord.Doctor;
                    oneMedRecord.Allergens = medRecord.Allergens;
                    break;
                }
            }

        }

        public bool DeleteMedicalRecord(String medRecordID)
        {
            foreach(MedicalRecord medRecord in MedicalRecords)
            {
                if (medRecord.ID.Equals(medRecordID))
                {
                    MedicalRecords.Remove(medRecord);
                    return true;
                }
            }
            return false;
        }

        public MedicalRecord ReadMedicalRecord(String medRecordID)
        {
            foreach(MedicalRecord medRecord in MedicalRecords)
            {
                if(medRecord.ID.Equals(medRecordID))
                {
                    return medRecord;
                }
            }    

            return null;
        }

        public ObservableCollection<MedicalRecord> ReadAllMedicalRecords()
        {
            return MedicalRecords;
        }

        //public void EditAllergens(String medRecordID, ObservableCollection<String> newAllergens)
        //{
        //    foreach(MedicalRecord medRecord in medicalRecords)
        //    {
        //        if (medRecord.ID.Equals(medRecordID))
        //        {
        //            medRecord.Allergens = newAllergens;
        //            break;
        //        }
        //    }
        //}
      
        public bool LoadMedicalRecord()
        {
            using FileStream fileStream = File.OpenRead(DBPath);
            this.MedicalRecords = JsonSerializer.Deserialize<ObservableCollection<MedicalRecord>>(fileStream);
            return true;
        }

        public bool SaveMedicalRecord()
        {
            string jsonString = JsonSerializer.Serialize(MedicalRecords);
            File.WriteAllText(DBPath, jsonString);
            return true;
        }
      
    }
}
