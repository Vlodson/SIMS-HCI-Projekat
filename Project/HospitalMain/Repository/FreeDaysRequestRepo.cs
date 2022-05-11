using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repository
{
    public class FreeDaysRequestRepo
    {
        public string DBPath { get; set; }
        public ObservableCollection<FreeDaysRequest> Requests { get; set; }

        public FreeDaysRequestRepo(string dbPath)
        {
            DBPath = dbPath;
            Requests = new ObservableCollection<FreeDaysRequest>();

            if (File.Exists(DBPath))
                LoadRequest();
        }
        public bool NewRequest(FreeDaysRequest request)
        {
            foreach(FreeDaysRequest _request in Requests)
            {
                if (_request.DoctorId.Equals(request.DoctorId))
                {
                    return false;
                }
            }
            Requests.Add(request);
            SaveRequest();
            return true;
        }
        public bool LoadRequest()
        {
            using FileStream fileStream = File.OpenRead(DBPath);
            Requests = JsonSerializer.Deserialize<ObservableCollection<FreeDaysRequest>>(fileStream);
            return true;
        }

        public bool SaveRequest()
        {
            string jsonString = JsonSerializer.Serialize(Requests);
            File.WriteAllText(DBPath, jsonString);
            return true;
        }
    }
}
