using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HospitalMain.Repository
{
    public class PersonalNotificationRepo
    {
        private String dbPath;
        private List<PersonalNotification> personalNotifications;

        public PersonalNotificationRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.personalNotifications = new List<PersonalNotification>();

        }

        public bool LoadPersonalNotification()
        {
            using FileStream fileStream = File.OpenRead(dbPath);
            this.personalNotifications = JsonSerializer.Deserialize<List<PersonalNotification>>(fileStream);

            return true;
        }

        public bool SavePersonalNotification()
        {
            string jsonString = JsonSerializer.Serialize(personalNotifications);
            File.WriteAllText(dbPath, jsonString);
            return true;
        }

        public void AddPersonalNotification(PersonalNotification personalNotification)
        {
            personalNotifications.Add(personalNotification);
        }
        public void DeletePersonalNotification(PersonalNotification personalNotification)
        {
            personalNotifications.Remove(personalNotification);
        }
    }
}
