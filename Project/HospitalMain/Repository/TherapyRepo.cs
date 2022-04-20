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
    public class TherapyRepo
    {
        public string dbPath { get; set; }
        public ObservableCollection<Therapy> therapyList { get; set; }

        public TherapyRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.therapyList = new ObservableCollection<Therapy>();

            Therapy therapy1 = new Therapy("t1", "lek1", 5, 2, true);
            Therapy therapy2 = new Therapy("t2", "lek2", 5, 2, false);
            Therapy therapy3 = new Therapy("t3", "lek3", 5, 2, true);

            this.therapyList.Add(therapy1);
            this.therapyList.Add(therapy2);
            this.therapyList.Add(therapy3);

        }

        public bool LoadTherapy()
        {
            using FileStream fileStream = File.OpenRead(dbPath);
            this.therapyList = JsonSerializer.Deserialize<ObservableCollection<Therapy>>(fileStream);
            return true;
        }

        public bool SaveTherapy()
        {
            string jsonString = JsonSerializer.Serialize(therapyList);
            File.WriteAllText(dbPath, jsonString);
            return true;
        }
    }
}
