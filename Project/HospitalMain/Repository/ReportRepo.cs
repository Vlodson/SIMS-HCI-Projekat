using Model;
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
    public class ReportRepo
    {
        public string dbPath { get; set; }
        public ObservableCollection<Report> reports { get; set; }

        public ReportRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.reports = new ObservableCollection<Report>();

            Therapy therapy1 = new Therapy("t1", "lek1", 5, 2, true);
            Therapy therapy2 = new Therapy("t2", "lek2", 5, 2, false);
            Therapy therapy3 = new Therapy("t3", "lek3", 5, 2, true);
            ObservableCollection<Therapy> ts = new ObservableCollection<Therapy>();
            ts.Add(therapy1);
            ts.Add(therapy2);
            ts.Add(therapy3);

            Report report = new Report("examId", "Neki opis", new DateTime(), "p1", "d1", ts);
            reports.Add(report);
        }

        public bool LoadReport()
        {
            using FileStream fileStream = File.OpenRead(dbPath);
            this.reports = JsonSerializer.Deserialize<ObservableCollection<Report>>(fileStream);
            return true;
        }

        public bool SaveReport()
        {
            string jsonString = JsonSerializer.Serialize(reports);
            File.WriteAllText(dbPath, jsonString);
            return true;
        }
    }
}
