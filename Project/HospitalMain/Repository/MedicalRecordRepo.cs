
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HospitalMain.Enums;
using HospitalMain.Model;

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

            //Therapy therapy1 = new Therapy("t1", "lek1", 5, 2, true);
            //Therapy therapy2 = new Therapy("t2", "lek2", 5, 2, false);
            //Therapy therapy3 = new Therapy("t3", "lek3", 5, 2, true);
            //ObservableCollection<Therapy> ts = new ObservableCollection<Therapy>();
            //ts.Add(therapy1);
            //ts.Add(therapy2);
            //ts.Add(therapy3);

            //Report report = new Report("examId", "Neki opis", new DateTime(2022, 5, 1), "p1", "d1", ts);
            //ObservableCollection<Report> reports = new ObservableCollection<Report>();
            //reports.Add(report);

            //String allergenName1 = "Prasina";
            //String allergenName2 = "Polen trave";
            //String allergenName3 = "Grinje";
            //String allergenName4 = "Lesnici";

            //Allergen allergen1 = new Allergen(allergenName1);
            //Allergen allergen2 = new Allergen(allergenName2);
            //Allergen allergen3 = new Allergen(allergenName3);
            //Allergen allergen4 = new Allergen(allergenName4);

            //ObservableCollection<Allergen> allergens1 = new ObservableCollection<Allergen>();
            //ObservableCollection<Allergen> allergens2 = new ObservableCollection<Allergen>();
            //ObservableCollection<Allergen> allergens3 = new ObservableCollection<Allergen>();

            //allergens1.Add(allergen1);
            //allergens1.Add(allergen2);

            //allergens2.Add(allergen3);

            //allergens3.Add(allergen4);
            //allergens3.Add(allergen1);

            //List<Examination> examinationsDoctor1 = new List<Examination>();
            //DateTime dtDoctor1 = DateTime.Now;
            //Doctor doctor1 = new Doctor("d1", "Ivan", "Peric", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);

            //List<Examination> examinationsDoctor2 = new List<Examination>();
            //DateTime dtDoctor2 = DateTime.Now;
            //Doctor doctor2 = new Doctor("d11", "Marko", "Markovic", dtDoctor2, DoctorType.Pulmonology, examinationsDoctor2);

            //MedicalRecord mr1 = new MedicalRecord("1", "0605994802463", "Pera", "Peric", "4098240", "pera@mail.com", "Partizanska 13, Novi Sad", Gender.Male, new DateTime(1994, 05, 06), "A", reports, allergens1, new ObservableCollection<Notification>());
            //MedicalRecord mr2 = new MedicalRecord("2", "0808985802463", "Ivan", "Ivic", "4489965", "ivan@mail.com", "Partizanska 14, Novi Sad", Gender.Male, new DateTime(1985, 08, 08), "B", reports, allergens2, new ObservableCollection<Notification>());
            //MedicalRecord mr3 = new MedicalRecord("3", "1111001802463", "Zika", "Zikic", "41478115", "zika@mail.com", "Partizanska 15, Novi Sad", Gender.Male, new DateTime(2001, 11, 11), "AB", reports, allergens3, new ObservableCollection<Notification>());

            //this.MedicalRecords.Add(mr1);
            //this.MedicalRecords.Add(mr2);
            //this.MedicalRecords.Add(mr3);

            //foreach (MedicalRecord mc in MedicalRecords)
            //{

            //    List<String> stringList = new List<string>();
            //    foreach (Report r in mc.Reports)
            //    {
            //        foreach (Therapy therapy in r.Therapy)
            //        {

            //            DateTime start = new DateTime(report.CreateDate.Year, report.CreateDate.Month, report.CreateDate.Day, 0, 0, 0);
            //            DateTime end = report.CreateDate.AddDays(therapy.Duration);
            //            int addingHours = 24 / therapy.PerDay; //ovoliko da se dodaje

            //            for (int i = 0; i < therapy.Duration; ++i)
            //            {
            //                for (int j = 0; j < therapy.PerDay; ++j)
            //                {
            //                    DateTime dateTime = start.AddDays(i).AddHours(j * addingHours);
            //                    String content = "Popiti lek " + therapy.Medicine + " u " + dateTime.ToString();
            //                    Notification notification = new Notification(content, false, dateTime);
            //                    mc.Notifications.Add(notification);

            //                }
            //            }
            //        }
            //    }
            //}




            if (File.Exists(DBPath))
                LoadMedicalRecord();

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
            SaveMedicalRecord();
            return true;
        }

        public void EditMedicalRecord(String medRecordID, MedicalRecord medRecord)
        {
            foreach(MedicalRecord oneMedRecord in MedicalRecords)
            {
                if (oneMedRecord.ID.Equals(medRecordID))
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
                    oneMedRecord.Allergens = medRecord.Allergens;
                    oneMedRecord.Notifications = medRecord.Notifications;
                    SaveMedicalRecord();
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
                    SaveMedicalRecord();
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

        public List<Notification> GetNotificationTimes(MedicalRecord medicalRecord)
        {
            
            LoadMedicalRecord();
            return medicalRecord.Notifications.ToList();
        }

        public void EditReadNotification(MedicalRecord medicalRecord, Notification notification)
        {
            foreach (MedicalRecord oneMedRecord in MedicalRecords)
            {
                if (oneMedRecord.ID.Equals(medicalRecord.ID))
                {
                    
                    foreach(Notification not in oneMedRecord.Notifications)
                    {
                        if(not.Content == notification.Content && not.DateTimeNotification == notification.DateTimeNotification)
                        {
                            oneMedRecord.Notifications.Remove(not);
                            SaveMedicalRecord();
                            break;
                        }
                    }
                    
                    
                }
            }

        }

        public void AddNewReport(string id, Report report)
        {
            foreach(MedicalRecord mr in MedicalRecords)
            {
                if(mr.ID.Equals(id))
                {
                    mr.Reports.Add(report);
                    break;
                }
            }
        }
      

    }
}
