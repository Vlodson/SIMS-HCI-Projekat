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

        public DoctorRepo(string dbPath, List<Doctor> listDoctor)
        {
            this.dbPath = dbPath;
            this.doctorList = new ObservableCollection<Doctor>();

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Doctor doctor1 = new Doctor("idDoctor1", "nameDoctor1", "surnameDoctor1", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);

            List<Examination> examinationsDoctor2 = new List<Examination>();
            DateTime dtDoctor2 = DateTime.Now;
            Doctor doctor2 = new Doctor("idDoctor2", "nameDoctor2", "surnameDoctor2", dtDoctor2, DoctorType.Pulmonology, examinationsDoctor2);

            this.doctorList.Add(doctor1);
            this.doctorList.Add(doctor2);
           
        }
        public DoctorRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.doctorList = new ObservableCollection<Doctor>();

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Doctor doctor1 = new Doctor("d1", "Milan", "Markovic", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);

            List<Examination> examinationsDoctor2 = new List<Examination>();
            DateTime dtDoctor2 = DateTime.Now;
            Doctor doctor2 = new Doctor("d11", "Jovan", "Petrovic", dtDoctor2, DoctorType.Pulmonology, examinationsDoctor2);

            this.doctorList.Add(doctor1);
            this.doctorList.Add(doctor2);

        }

        public ObservableCollection<Doctor> GetAllDoctors()
        {
            //foreach(Doctor doctor in listDoctor) Console.WriteLine(doctor.Name);
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

        public bool LoadExamination()
        {

            using FileStream stream = File.OpenRead(dbPath);
            this.doctorList = JsonSerializer.Deserialize<ObservableCollection<Doctor>>(stream);

            return true;
        }

        public bool SaveExamination()
        {
            string jsonString = JsonSerializer.Serialize(doctorList);

            File.WriteAllText(dbPath, jsonString);
            return true;
        }



    }
}
