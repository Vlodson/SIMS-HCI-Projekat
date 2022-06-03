using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Repository
{
    public class DoctorRepo
    {
        public string DBPath { get; set; }
        public ObservableCollection<Doctor> Doctors { get; set; }

        public DoctorRepo(string dbPath)
        {
            this.DBPath = dbPath;
            this.Doctors = new ObservableCollection<Doctor>();

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Doctor doctor1 = new Doctor("d1", "Milan", "Markovic", dtDoctor1, DoctorType.Pulmonology, 20,  examinationsDoctor1);

            List<Examination> examinationsDoctor2 = new List<Examination>();
            DateTime dtDoctor2 = DateTime.Now;
            Doctor doctor2 = new Doctor("d11", "Jovan", "Petrovic", dtDoctor2, DoctorType.Cardiology, 20, examinationsDoctor2);

            List<Examination> examinationsDoctor3 = new List<Examination>();
            DateTime dtDoctor3 = DateTime.Now;
            Doctor doctor3 = new Doctor("d12", "Miroslav", "Katic", dtDoctor3, DoctorType.Neurology, 20, examinationsDoctor3);

            List<Examination> examinationsDoctor4 = new List<Examination>();
            DateTime dtDoctor4 = DateTime.Now;
            Doctor doctor4 = new Doctor("d13", "Andrija", "Stanisic", dtDoctor4, DoctorType.General, 20, examinationsDoctor4);

            List<Examination> examinationsDoctor5 = new List<Examination>();
            DateTime dtDoctor5 = DateTime.Now;
            Doctor doctor5 = new Doctor("d14", "Milos", "Gravara", dtDoctor5, DoctorType.Pulmonology, 20, examinationsDoctor5);

            List<Examination> examinationsDoctor6 = new List<Examination>();
            DateTime dtDoctor6 = DateTime.Now;
            Doctor doctor6 = new Doctor("d15", "Ivica", "Pajic", dtDoctor6, DoctorType.Dermatology, 20, examinationsDoctor6);

            List<Examination> examinationsDoctor7 = new List<Examination>();
            DateTime dtDoctor7 = DateTime.Now;
            Doctor doctor7 = new Doctor("d16", "Goran", "Djuric", dtDoctor7, DoctorType.Cardiology, 20, examinationsDoctor7);

            this.Doctors.Add(doctor1);
            this.Doctors.Add(doctor2);
            this.Doctors.Add(doctor3);
            this.Doctors.Add(doctor4);
            this.Doctors.Add(doctor5);
            this.Doctors.Add(doctor6);
            this.Doctors.Add(doctor7);

            if (File.Exists(dbPath))
                LoadDoctor();

        }

        public bool LoadDoctor()
        {

            using FileStream stream = File.OpenRead(DBPath);
            this.Doctors = JsonSerializer.Deserialize<ObservableCollection<Doctor>>(stream);

            return true;
        }

        public bool SaveDoctor()
        {
            string jsonString = JsonSerializer.Serialize(Doctors);
            File.WriteAllText(DBPath, jsonString);
            return true;
        }
    }
}
