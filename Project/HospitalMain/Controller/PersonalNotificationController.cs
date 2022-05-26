using HospitalMain.Model;
using HospitalMain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Controller
{
    public class PersonalNotificationController
    {
        private PersonalNotificationService _personalNotificationService;

        public PersonalNotificationController(PersonalNotificationService personalNotificationService)
        {
            _personalNotificationService = personalNotificationService;
        }

        public void AddPersonalNotification(PersonalNotification personalNotification)
        {
            _personalNotificationService.AddPersonalNotification(personalNotification);
        }

        public void DeletePersonalNotification(PersonalNotification personalNotification)
        {
            _personalNotificationService.DeletePersonalNotification(personalNotification);
        }

        public List<PersonalNotification> GetPatientPersonalNotifications(String patientID)
        {
            return _personalNotificationService.GetPatientPersonalNotifications(patientID);
        }
    }
}
