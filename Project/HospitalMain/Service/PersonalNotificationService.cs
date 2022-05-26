using HospitalMain.Model;
using HospitalMain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Service
{
    public class PersonalNotificationService
    {
        private PersonalNotificationRepo _personalNotificationRepo;

        public PersonalNotificationService(PersonalNotificationRepo personalNotificationRepo)
        {
            _personalNotificationRepo = personalNotificationRepo;
        }

        public void AddPersonalNotification(PersonalNotification personalNotification)
        {
            _personalNotificationRepo.AddPersonalNotification(personalNotification);
        }
        public void DeletePersonalNotification(PersonalNotification personalNotification)
        {
            _personalNotificationRepo.DeletePersonalNotification(personalNotification);
        }

        public List<PersonalNotification> GetPatientPersonalNotifications(String patientID)
        {
            return _personalNotificationRepo.ReadAllPersonalNotifications().Where(personalNotification => personalNotification.PatientId.Equals(patientID)).ToList();
        }

        public void SetNotificationRead(PersonalNotification personalNotification)
        {
            personalNotification.Status = false;
            _personalNotificationRepo.SavePersonalNotification();
        }

    }
}
