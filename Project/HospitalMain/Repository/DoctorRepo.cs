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
    public class DoctorRepo
    {
        private string dbPath { get; set; }
        private ObservableCollection<Doctor> doctorList { get; set; }

        public DoctorRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.doctorList = new ObservableCollection<Doctor>();

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Doctor doctor1 = new Doctor("d1", "Milan", "Markovic", dtDoctor1, DoctorType.Pulmonology, 20,  examinationsDoctor1);

            List<Examination> examinationsDoctor2 = new List<Examination>();
            DateTime dtDoctor2 = DateTime.Now;
            Doctor doctor2 = new Doctor("d11", "Jovan", "Petrovic", dtDoctor2, DoctorType.Pulmonology, 30,  examinationsDoctor2);

            this.doctorList.Add(doctor1);
            this.doctorList.Add(doctor2);

            //if (File.Exists(dbPath))
            //    LoadDoctor();

        }

        public ObservableCollection<Doctor> GetAllDoctors()
        {
            return doctorList;
        }

        public Doctor GetDoctor(string id)
        {
            foreach(Doctor doctor in this.doctorList)
            {
                if (doctor.Id.Equals(id))
                {
                    return doctor;
                }
            }
            return null;
        }

        public bool LoadDoctor()
        {

            using FileStream stream = File.OpenRead(dbPath);
            this.doctorList = JsonSerializer.Deserialize<ObservableCollection<Doctor>>(stream);

            return true;
        }

        public bool SaveDoctor()
        {
            string jsonString = JsonSerializer.Serialize(doctorList);

            File.WriteAllText(dbPath, jsonString);
            return true;
        }



    }
}
