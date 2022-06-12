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
    public class TherapyRepo: ITherapyRepo
    {
        public string DBPath { get; set; }
        public ObservableCollection<Therapy> Therapies { get; set; }

        public TherapyRepo(string dbPath)
        {
            this.DBPath = dbPath;
            this.Therapies = new ObservableCollection<Therapy>();

            Therapy therapy1 = new Therapy("idExam1", "lek1", 5, 2, true);
            Therapy therapy2 = new Therapy("t2", "lek2", 5, 2, false);
            Therapy therapy3 = new Therapy("t3", "lek3", 5, 2, true);

            this.Therapies.Add(therapy1);
            this.Therapies.Add(therapy2);
            this.Therapies.Add(therapy3);

        }

        public bool LoadTherapy()
        {
            using FileStream fileStream = File.OpenRead(DBPath);
            this.Therapies = JsonSerializer.Deserialize<ObservableCollection<Therapy>>(fileStream);
            return true;
        }

        public bool SaveTherapy()
        {
            string jsonString = JsonSerializer.Serialize(Therapies);
            File.WriteAllText(DBPath, jsonString);
            return true;
        }

        public ObservableCollection<Therapy> findById(string examId)
        {
            ObservableCollection<Therapy> therapies = new ObservableCollection<Therapy>();
            foreach (Therapy therapy in this.Therapies)
            {
                if(therapy.ExamId.Equals(examId))
                    therapies.Add(therapy);
            }
            return therapies;
        }

        public void NewTherapy(Therapy therapy)
        {
            Therapies.Add(therapy);
            SaveTherapy();
        }
    }
}
