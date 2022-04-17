using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

using Model;

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

        public void SetEquipment(String equipmentId, Equipment newEquipment)
        {
            for(int i = 0; i < Equipment.Count; i++)
            {
                if (Equipment[i].Id.Equals(equipmentId))
                {
                    Equipment[i] = newEquipment;
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
