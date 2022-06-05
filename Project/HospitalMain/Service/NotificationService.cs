using HospitalMain.Model;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Service
{
    public class NotificationService
    {
        private MedicalRecordRepo _medicalRecordRepo;

        public NotificationService(MedicalRecordRepo medicalRecordRepo)
        {
            _medicalRecordRepo = medicalRecordRepo;
        }

        public List<Notification> GetPatientNotifications(MedicalRecord medicalRecord)
        {
            List<Notification> unreadNotifications = new List<Notification>();
            foreach (Notification notification in medicalRecord.Notifications.ToList())
            {
                if (notification.DateTimeNotification.CompareTo(DateTime.Now) < 0)
                {
                    unreadNotifications.Add(notification);
                }
            }
            return unreadNotifications;
        }

        public void CheckNotification(MedicalRecord medicalRecord, Notification notification)
        {
            foreach (Notification not in medicalRecord.Notifications)
            {
                if (not.Content == notification.Content && not.DateTimeNotification == notification.DateTimeNotification)
                {
                    medicalRecord.Notifications.Remove(not);
                    _medicalRecordRepo.SaveMedicalRecord();
                    break;
                }
            }
        }
        public void EditReadNotification(MedicalRecord medicalRecord, Notification notification)
        {
            foreach (MedicalRecord oneMedRecord in _medicalRecordRepo.MedicalRecords)
            {
                if (oneMedRecord.ID.Equals(medicalRecord.ID))
                {
                    CheckNotification(oneMedRecord, notification);
                }
            }
        }
    }
}
