using Enums;
using Interfaces;
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
    public class FreeDaysRequestRepo: IFreeDaysRequestRepo
    {
        public string DBPath { get; set; }
        public ObservableCollection<FreeDaysRequest> Requests { get; set; }

        public FreeDaysRequestRepo(string dbPath)
        {
            DBPath = dbPath;
            Requests = new ObservableCollection<FreeDaysRequest>();

            FreeDaysRequest request1 = new FreeDaysRequest("1", StatusEnum.Pending, "d1", DateTime.Now.AddDays(5), DateTime.Now.AddDays(12), FreeDaysReasons.Sick_leave, "");
            FreeDaysRequest request2 = new FreeDaysRequest("2", StatusEnum.Pending, "d13", DateTime.Now.AddDays(3), DateTime.Now.AddDays(25), FreeDaysReasons.Emergency, "");
            FreeDaysRequest request3 = new FreeDaysRequest("3", StatusEnum.Rejected, "d12", DateTime.Now.AddDays(2), DateTime.Now.AddDays(16), FreeDaysReasons.Vacation, "Nedostatak slobodnih dana");
            FreeDaysRequest request4 = new FreeDaysRequest("4", StatusEnum.Pending, "d16", DateTime.Now.AddDays(6), DateTime.Now.AddDays(13), FreeDaysReasons.Sick_leave, "");
            FreeDaysRequest request5 = new FreeDaysRequest("5", StatusEnum.Approved, "d14", DateTime.Now.AddDays(9), DateTime.Now.AddDays(15), FreeDaysReasons.Vacation, "");

            this.Requests.Add(request1);
            this.Requests.Add(request2);
            this.Requests.Add(request3);
            this.Requests.Add(request4);
            this.Requests.Add(request5);

            //SaveRequest();
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

        public void EditRequestStatus(FreeDaysRequest request)
        {
            foreach (FreeDaysRequest _request in Requests)
            {
                if (_request.ID.Equals(request.ID))
                {
                    _request.Status = request.Status;
                    _request.RejectionReason = request.RejectionReason;
                    break;
                }
            }
            SaveRequest();
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
