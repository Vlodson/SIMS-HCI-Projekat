using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

using Model;

namespace Repository
{
    public class MedicineRepo
    {
        public String dbPath { get; set; }
        public ObservableCollection<Medicine> Medicine { get; set; }

        public MedicineRepo(string db_path)
        {
            this.dbPath = db_path;
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

        public void LoadMedicine()
        {
            using FileStream medicineFileStream = File.OpenRead(dbPath);
            this.Medicine = JsonSerializer.Deserialize<ObservableCollection<Medicine>>(medicineFileStream);
        }

        public void SaveMedicine()
        {
            string jsonString = JsonSerializer.Serialize(this.Medicine);
            File.WriteAllText(dbPath, jsonString);
        }
    }
}
