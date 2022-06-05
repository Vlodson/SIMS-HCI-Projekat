using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalMain.Enums;
using System.IO;
using System.Text.Json;

namespace Repository
{
    public class UserAccountRepo
    {
        public string DBPath { get; set; }
        public ObservableCollection<UserAccount> UserAccCollection { get; set; }

        public UserAccountRepo(string dbPath)
        {
            DBPath = dbPath;
            UserAccCollection = new ObservableCollection<UserAccount>();


            UserAccount secretaryUser = new UserAccount("Srbija", "1312", UserType.Secretary);
            UserAccount adminUser = new UserAccount("Gromina", "69", UserType.Admin);
            UserAccount patientUser = new UserAccount("Sandra", "123", UserType.Patient);
            UserAccount doctorUser = new UserAccount("d1", "1234", UserType.Doctor);

            UserAccCollection.Add(doctorUser);
            UserAccCollection.Add(secretaryUser);
            UserAccCollection.Add(adminUser);
            UserAccCollection.Add(patientUser);

            if (File.Exists(DBPath))
            {
                LoadUserAccounts();
            }
        }

        public bool AddUserAccount(UserAccount userAcc)
        {
            foreach(UserAccount userAccount in UserAccCollection)
            {
                if (userAccount.UserName.Equals(userAcc.UserName))
                {
                    return false;
                }
            }

            UserAccCollection.Add(userAcc);
            SaveUserAccounts();
            return true;
        }

        public void EditUserAccount(String username, UserAccount userAcc)
        {
            foreach(UserAccount userAccount in UserAccCollection)
            {
                if (userAccount.UserName.Equals(username))
                {
                    userAccount.UserName = userAcc.UserName;
                    userAccount.Password = userAcc.Password;
                    userAccount.Type = userAcc.Type;
                    break;
                }
            }
            SaveUserAccounts();
        }

        public bool DeleteUserAccount(String username)
        {
            foreach(UserAccount userAccount in UserAccCollection)
            {
                if (userAccount.UserName.Equals(username))
                {
                    UserAccCollection.Remove(userAccount);
                    return true;
                }
            }
            SaveUserAccounts();
            return false;
        }

        public UserAccount ReadUserAccount(String username)
        {
            foreach(UserAccount userAccount in UserAccCollection)
            {
                if (userAccount.UserName.Equals(username))
                {
                    return userAccount;
                }
            }
            return null;
        }

        public void LoadUserAccounts()
        {
            using FileStream fileStream = File.OpenRead(DBPath);
            UserAccCollection = JsonSerializer.Deserialize<ObservableCollection<UserAccount>>(fileStream);
        }

        public void SaveUserAccounts()
        {
            string jsonString = JsonSerializer.Serialize(UserAccCollection);
            File.WriteAllText(DBPath, jsonString);
        }
    
    }
}
