using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Model;

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

        //public bool LoadMedicalRecords()
        //{
        //    return true;
        //}

        //public bool SaveMedicalRecords()
        //{
        //    return true;
        //}
    }
}
