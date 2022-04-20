using HospitalMain.Model;
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
    public class MedicalRecordRepo
    {
        public string dbPath { get; set; }
        public ObservableCollection<MedicalRecord> medicalRecords { get; set; }

        public MedicalRecordRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.medicalRecords = new ObservableCollection<MedicalRecord>();

            Therapy therapy1 = new Therapy("t1", "lek1", 5, 2, true);
            Therapy therapy2 = new Therapy("t2", "lek2", 5, 2, false);
            Therapy therapy3 = new Therapy("t3", "lek3", 5, 2, true);
            ObservableCollection<Therapy> ts = new ObservableCollection<Therapy>();
            ts.Add(therapy1);
            ts.Add(therapy2);
            ts.Add(therapy3);

            Report report = new Report("examId", "Neki opis", new DateTime(), "p1", "d1", ts);
            ObservableCollection<Report> reports = new ObservableCollection<Report>();
            reports.Add(report);

            MedicalRecord mr = new MedicalRecord("4", "ucin", "ime", "prezime", "4098240", "mejl", "adresa", Enums.Gender.Male, new DateTime(), new Doctor(), "A", reports, new ObservableCollection<string>());
            this.medicalRecords.Add(mr);
        }

        public bool LoadMedicalRecord()
        {
            using FileStream fileStream = File.OpenRead(dbPath);
            this.medicalRecords = JsonSerializer.Deserialize<ObservableCollection<MedicalRecord>>(fileStream);
            return true;
        }

        public bool SaveMedicalRecord()
        {
            string jsonString = JsonSerializer.Serialize(medicalRecords);
            File.WriteAllText(dbPath, jsonString);
            return true;
        }
    }
}
