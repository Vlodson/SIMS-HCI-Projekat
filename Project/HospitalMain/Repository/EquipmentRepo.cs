using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

using Model;
using HospitalMain.Enums;

namespace Repository
{
    public class EquipmentRepo
    {
        public String dbPath { get; set; }
        public ObservableCollection<Equipment> Equipment { get; set; }

        public EquipmentRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.Equipment = new ObservableCollection<Equipment>();
        }

        public bool NewEquipment(Equipment equipment)
        {
            Equipment.Add(equipment);
            return true;
        }

        public Equipment GetEquipment(String equipmentId)
        {
            foreach(Equipment equipment in Equipment)
                if(equipment.Id.Equals(equipmentId))
                    return equipment;

            return null;
        }

        public void SetEquipment(Equipment newEquipment)
        {
            for(int i = 0; i < Equipment.Count; i++)
            {
                if (Equipment[i].Id.Equals(newEquipment.Id))
                {
                    Equipment[i].RoomId = newEquipment.RoomId;
                    Equipment[i].Type = newEquipment.Type;
                    break;
                }
            }
        }

        public bool DeleteEquipment(String equipmentId)
        {
            foreach(Equipment equipment in Equipment)
                if (equipment.Id.Equals(equipmentId))
                {
                    Equipment.Remove(equipment);
                    return true;
                }

            return false;
        }

        public String GenerateID()
        {
            int id = 0;
            if (Equipment.Count > 0)
                id = Equipment.Max(r => int.Parse(r.Id)) + 1;

            return id.ToString();
        }

        public bool LoadEquipment()
        {
            using FileStream fileStream = File.OpenRead(dbPath);
            this.Equipment = JsonSerializer.Deserialize<ObservableCollection<Equipment>>(fileStream);
            return true;
        }

        public bool SaveEquipment()
        {
            string jsonString = JsonSerializer.Serialize(this.Equipment);
            File.WriteAllText(dbPath, jsonString);
            return true;
        }
    }
}
