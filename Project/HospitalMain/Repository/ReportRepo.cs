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
    public class ReportRepo
    {
        public string DBPath { get; set; }
        public ObservableCollection<Report> Reports { get; set; }

        public ReportRepo(string dbPath)
        {
            this.DBPath = dbPath;
            this.Reports = new ObservableCollection<Report>();

            Therapy therapy1 = new Therapy("t1", "lek1", 5, 2, true);
            Therapy therapy2 = new Therapy("t2", "lek2", 5, 2, false);
            Therapy therapy3 = new Therapy("t3", "lek3", 5, 2, true);
            ObservableCollection<Therapy> ts = new ObservableCollection<Therapy>();
            ts.Add(therapy1);
            ts.Add(therapy2);
            ts.Add(therapy3);

            /*Report report = new Report("examId", "Neki opis", new DateTime(), "1", "d1", ts);
            Report report1 = new Report("examId1", "Neki opis1", new DateTime(), "1", "d1", ts);
            reports.Add(report);
            reports.Add(report1);*/
        }

        public bool LoadReport()
        {
            using FileStream fileStream = File.OpenRead(DBPath);
            this.Reports = JsonSerializer.Deserialize<ObservableCollection<Report>>(fileStream);
            return true;
        }

        public bool SaveReport()
        {
            string jsonString = JsonSerializer.Serialize(Reports);
            File.WriteAllText(DBPath, jsonString);
            return true;
        }

        public void NewReport(Report report)
        {
            Reports.Add(report);
        }

        public ObservableCollection<Report> findByPatientId(string id)
        {
            ObservableCollection<Report> reportsForPatient = new ObservableCollection<Report>();
            foreach (Report report in Reports)
            {
                if (report.PatientId.Equals(id))
                    reportsForPatient.Add(report);
            }
            return reportsForPatient;
        }
    }
}
