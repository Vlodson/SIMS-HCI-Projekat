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

        private MedicalRecordRepo medicalRecordRepo;

        public MedicalRecordService(MedicalRecordRepo medicalRecordRepo)
        {
            this.medicalRecordRepo = medicalRecordRepo;
        }

        public int generateID()
        {
            return medicalRecordRepo.generateID();
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
            return medicalRecordRepo.ReadMedicalRecord(medRecordID);
        }

        public ObservableCollection<MedicalRecord> GetAllMedicalRecords()
        {
            return medicalRecordRepo.ReadAllMedicalRecords();
        }


        public List<Notification> GetNotificationTimes(MedicalRecord medicalRecord)
        {
            List<Notification> unreadNotifications = new List<Notification>();
            foreach(Notification notification in medicalRecordRepo.GetNotificationTimes(medicalRecord))
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
            medicalRecordRepo.AddNewReport(id, report);
        }

        public void EditReadNotification(MedicalRecord medicalRecord, Notification notification)
        {
            medicalRecordRepo.EditReadNotification(medicalRecord, notification);
        }


    }
}
