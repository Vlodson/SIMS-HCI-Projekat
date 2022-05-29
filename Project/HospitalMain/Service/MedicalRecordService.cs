using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Model;
using System.Collections.ObjectModel;
using HospitalMain.Enums;
using HospitalMain.Model;

namespace Service
{
    public class MedicalRecordService
    {

        private readonly MedicalRecordRepo medicalRecordRepo;

        public MedicalRecordService(MedicalRecordRepo medicalRecordRepo)
        {
            this.medicalRecordRepo = medicalRecordRepo;
        }

        public int generateID()
        {
            int maxID = 0;
            ObservableCollection<MedicalRecord> medicicalRecords = medicalRecordRepo.MedicalRecords;

            foreach (MedicalRecord medicalRecord in medicicalRecords)
            {
                int medicalRecordID = Int32.Parse(medicalRecord.ID);
                if (medicalRecordID > maxID)
                {
                    maxID = medicalRecordID;
                }
            }

            return maxID + 1;
        }

        public bool CreateMedicalRecord(String medRecordID, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime dob, BloodType bloodType, ObservableCollection<Report> reports, ObservableCollection<Allergens> allergens, ObservableCollection<Notification> notifications)
        {
            return medicalRecordRepo.NewMedicalRecord(new MedicalRecord(medRecordID, ucin, name, surname, phoneNum, mail, adress, gender, dob, bloodType, reports, allergens, notifications));
        }

        public void EditMedicalRecord(String medRecordID, String newUCIN, String newName, String newSurname, String newPhoneNum, String newMail, String newAdress, Gender newGender, DateTime newDoB, BloodType newBloodType, ObservableCollection<Report> reports, ObservableCollection<Allergens> newAllergens, ObservableCollection<Notification> notifications)
        {
            medicalRecordRepo.EditMedicalRecord(medRecordID, new MedicalRecord(medRecordID, newUCIN, newName, newSurname, newPhoneNum, newMail, newAdress, newGender, newDoB, newBloodType, reports, newAllergens, notifications));
        }

        public bool DeleteMedicalRecord(String medRecordID)
        {
            return medicalRecordRepo.DeleteMedicalRecord(medRecordID);
        }

        public MedicalRecord GetMedicalRecord(String medRecordID)
        {
            foreach (MedicalRecord medRecord in medicalRecordRepo.MedicalRecords)
            {
                if (medRecord.ID.Equals(medRecordID))
                {
                    return medRecord;
                }
            }

            return null;
        }

        public ObservableCollection<MedicalRecord> GetAllMedicalRecords()
        {
            return medicalRecordRepo.MedicalRecords;
        }


        public List<Notification> GetPatientNotifications(MedicalRecord medicalRecord)
        {
            List<Notification> unreadNotifications = new List<Notification>();
            foreach(Notification notification in medicalRecord.Notifications.ToList())
            {
                if(notification.DateTimeNotification.CompareTo(DateTime.Now) < 0)
                {
                    unreadNotifications.Add(notification);
                }
            }
            return unreadNotifications;
        }

        public void AddNewReport(string id, Report report)
        {
            foreach (MedicalRecord mr in medicalRecordRepo.MedicalRecords)
            {
                if (mr.ID.Equals(id))
                {
                    mr.Reports.Add(report);
    
                    break;
                }
            }
        }

        public void CheckNotification(MedicalRecord medicalRecord, Notification notification)
        {
            foreach (Notification not in medicalRecord.Notifications)
            {
                if (not.Content == notification.Content && not.DateTimeNotification == notification.DateTimeNotification)
                {
                    medicalRecord.Notifications.Remove(not);
                    medicalRecordRepo.SaveMedicalRecord();
                    break;
                }
            }
        }
        public void EditReadNotification(MedicalRecord medicalRecord, Notification notification)
        {
            foreach (MedicalRecord oneMedRecord in medicalRecordRepo.MedicalRecords)
            {
                if (oneMedRecord.ID.Equals(medicalRecord.ID))
                {
                    CheckNotification(oneMedRecord, notification);
                }
            }
        }

        public void AddNote(Report report,String note)
        {
            report.Note = note;
            medicalRecordRepo.SaveMedicalRecord();
        }
    }
}
