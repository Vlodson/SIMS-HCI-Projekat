using HospitalMain.Enums;
using HospitalMain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HospitalMain.Repository
{
    public class DynamicEquipmentRepo 
    {
        public string DBPath { get; set; }
        public ObservableCollection<DynamicEquipmentRequest> DynamicEquipment { get; set; }

        public DynamicEquipmentRepo(string dbPath)
        {
            DBPath = dbPath;
            DynamicEquipment = new ObservableCollection<DynamicEquipmentRequest>();

            DynamicEquipmentRequest request1 = new DynamicEquipmentRequest("1", 15, DynamicEquipmentTypeEnum.Bandages, "", DateTime.Now);
            DynamicEquipmentRequest request2 = new DynamicEquipmentRequest("2", 35, DynamicEquipmentTypeEnum.Iodine, "", DateTime.Now);
            DynamicEquipmentRequest request3 = new DynamicEquipmentRequest("3", 40, DynamicEquipmentTypeEnum.Needles, "", DateTime.Now);

            DynamicEquipment.Add(request1);
            DynamicEquipment.Add(request2);
            DynamicEquipment.Add(request3);

            //SaveDynamicEquipment();
            if (File.Exists(DBPath))
            {
                LoadDynamicEquipment();
            }
        }

        public bool OrderNewDynamicEquipmentRequest(DynamicEquipmentRequest dynamicEquipmentRequest)
        {
            foreach(DynamicEquipmentRequest dynamicEquipment in DynamicEquipment)
            {
                if (dynamicEquipment.ID.Equals(dynamicEquipmentRequest.ID))
                {
                    return false;
                }
            }
            DynamicEquipment.Add(dynamicEquipmentRequest);
            SaveDynamicEquipment();
            return true;
        }

        public void EditOrder(string orderID, DynamicEquipmentRequest newOrder)
        {
            foreach(DynamicEquipmentRequest dynamicEquipment in DynamicEquipment)
            {
                if (dynamicEquipment.ID.Equals(orderID))
                {
                    dynamicEquipment.ID = orderID;
                    dynamicEquipment.Quantity = newOrder.Quantity;
                    dynamicEquipment.ShortDescription = newOrder.ShortDescription;
                    dynamicEquipment.EquipmentType = newOrder.EquipmentType;
                    dynamicEquipment.OrderDate = newOrder.OrderDate;
                    break;
                }
            }
            SaveDynamicEquipment();
        }

        public void DeleteOrder(string orderID)
        {
            foreach(DynamicEquipmentRequest dynamicEquipment in DynamicEquipment)
            {
                if (dynamicEquipment.ID.Equals(orderID))
                {
                    DynamicEquipment.Remove(dynamicEquipment);
                    break;
                }
            }
            SaveDynamicEquipment();
        }

        public bool LoadDynamicEquipment()
        {
            using FileStream fileStream = File.OpenRead(DBPath);
            DynamicEquipment = JsonSerializer.Deserialize<ObservableCollection<DynamicEquipmentRequest>>(fileStream);
            return true;
        }

        public bool SaveDynamicEquipment()
        {
            string jsonString = JsonSerializer.Serialize(DynamicEquipment);
            File.WriteAllText(DBPath, jsonString);
            return true;
        }
    }
}
