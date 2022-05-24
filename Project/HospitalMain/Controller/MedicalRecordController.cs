using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;
using System.Collections.ObjectModel;
using HospitalMain.Enums;
using HospitalMain.Model;

namespace Controller
{
    public class MedicalRecordController
    {

        private MedicalRecordService medRecordService;

        public MedicalRecordController(MedicalRecordService medRecordService)
        {
            this.medRecordService = medRecordService;
        }

        public int generateID()
        {
            return medRecordService.generateID();
        }

        public bool CreateMedicalRecord(String medRecordID, String ucin, String name, String surname, String phoneNum, String mail, String adress, Gender gender, DateTime dob, BloodType bloodType, ObservableCollection<Report> reports, ObservableCollection<Allergens> allergens, ObservableCollection<Notification> notifications)
        {
            return medRecordService.CreateMedicalRecord(medRecordID, ucin, name, surname, phoneNum, mail, adress, gender, dob, bloodType, reports, allergens, notifications);
        }

        public void EditMedicalRecord(String medRecordID, String newUCIN, String newName, String newSurname, String newPhoneNum, String newMail, String newAdress, Gender newGender, DateTime newDoB, BloodType newBloodType, ObservableCollection<Report> newReports, ObservableCollection<Allergens> newAllergens, ObservableCollection<Notification> newNotifications)
        {
            medRecordService.EditMedicalRecord(medRecordID, newUCIN, newName, newSurname, newPhoneNum, newMail, newAdress, newGender, newDoB, newBloodType, newReports, newAllergens, newNotifications);
        }

        public bool DeleteMedicalRecord(String medRecordID)
        {
            return medRecordService.DeleteMedicalRecord(medRecordID);
        }

        public MedicalRecord GetMedicalRecord(String medRecordID)
        {
            return medRecordService.GetMedicalRecord(medRecordID);
        }

        public ObservableCollection<MedicalRecord> GetAllMedicalRecords()
        {
            return medRecordService.GetAllMedicalRecords();
        }


        public List<Notification> GetNotificationTimes(MedicalRecord medicalRecord)
        {
            return medRecordService.GetNotificationTimes(medicalRecord);
        }


        public void AddNewReport(string id, Report report)
        {
            medRecordService.AddNewReport(id, report);
        }

        public void EditReadNotification(MedicalRecord medicalRecord, Notification notification)
        {
            medRecordService.EditReadNotification(medicalRecord, notification);
        }

        public void AddNote(Report report,String note)
        {
            medRecordService.AddNote(report,note);
        }
    }
}
