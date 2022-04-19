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
        private String dbPath { get; set; }
        private ObservableCollection<MedicalRecord> medicalRecords { get; set; }

        public MedicalRecordRepo(String dbPath)
        {
            this.dbPath = dbPath;
            this.medicalRecords = new ObservableCollection<MedicalRecord>();
        }

        public bool NewMedicalRecord(MedicalRecord medRecord)
        {
            foreach(MedicalRecord oneMedRecord in medicalRecords)
            {
                if (oneMedRecord.ID.Equals(medRecord.ID))
                {
                    return false;
                }
            }
            medicalRecords.Add(medRecord);
            return true;
        }

        public void EditMedicalRecord(String medRecordID, MedicalRecord medRecord)
        {
            foreach(MedicalRecord oneMedRecord in medicalRecords)
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
            foreach(MedicalRecord medRecord in medicalRecords)
            {
                if (medRecord.ID.Equals(medRecordID))
                {
                    medicalRecords.Remove(medRecord);
                    return true;
                }
            }
            return false;
        }

        public MedicalRecord ReadMedicalRecord(String medRecordID)
        {
            foreach(MedicalRecord medRecord in medicalRecords)
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
            return medicalRecords;
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
