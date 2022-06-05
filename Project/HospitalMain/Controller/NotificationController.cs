using HospitalMain.Model;
using HospitalMain.Service;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Controller
{
    public class NotificationController
    {
        private NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public List<Notification> GetPatientNotifications(MedicalRecord medicalRecord)
        {
            return _notificationService.GetPatientNotifications(medicalRecord);
        }

        public void EditReadNotification(MedicalRecord medicalRecord, Notification notification)
        {
            _notificationService.EditReadNotification(medicalRecord, notification);
        }
    }
}
