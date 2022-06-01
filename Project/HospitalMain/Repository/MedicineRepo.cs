using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

using Model;
using Utility;
using Enums;

namespace Repository
{
    public class MedicineRepo
    {
        public String DBPath { get; set; }
        public ObservableCollection<Medicine> Medicine { get; set; }

        public MedicineRepo(string db_path)
        {
            this.DBPath = db_path;
            Medicine = new ObservableCollection<Medicine>();
            if (File.Exists(DBPath))
                LoadMedicine();
        }

        public void NewMedicine(Medicine medicine)
        {
            Medicine.Add(medicine);
        }

        public Medicine GetMedicine(String medicineId)
        {
            foreach(Medicine m in Medicine)
                if(m.Id == medicineId)
                    return m;

            return null;
        }

        public void SetMedicine(Medicine medicine)
        {
            for(int i = 0; i < Medicine.Count; i++)
                if(Medicine[i].Id == medicine.Id)
                {
                    Medicine[i].Name = medicine.Name;
                    Medicine[i].Type = medicine.Type;
                    Medicine[i].Ingredients = medicine.Ingredients;
                    Medicine[i].Status = medicine.Status;
                    Medicine[i].ReviewingDoctor = medicine.ReviewingDoctor;
                    Medicine[i].ArrivalDate = medicine.ArrivalDate;
                    Medicine[i].Comment = medicine.Comment;
                }
        }

        public bool DeleteMedicine(String medicineId)
        {
            foreach(Medicine m in Medicine)
                if(m.Id == medicineId)
                {
                    Medicine.Remove(m);
                    return true;
                }

            return false;
        }

        public String GenerateID()
        {
            int id = 0;
            if (Medicine.Count > 0)
                id = Medicine.Max(r => int.Parse(r.Id));

            return id.ToString();
        }

        public static String GenerateName(MedicineTypeEnum type)
        {
            // get the enum map, generate a random number, use it to index to get a random name return the name
            var random = new Random();
            int random_index = random.Next(MedicineNamesMap.MedicineTypeNameMap[type].Count);
            return MedicineNamesMap.MedicineTypeNameMap[type][random_index];
        }

        public void LoadMedicine()
        {
            using FileStream medicineFileStream = File.OpenRead(DBPath);
            this.Medicine = JsonSerializer.Deserialize<ObservableCollection<Medicine>>(medicineFileStream);
        }

        public void SaveMedicine()
        {
            string jsonString = JsonSerializer.Serialize(this.Medicine);
            File.WriteAllText(DBPath, jsonString);
        }
    }
}
