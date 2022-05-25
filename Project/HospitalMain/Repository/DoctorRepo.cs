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
        public string dbPath { get; set; }
        public ObservableCollection<Doctor> DoctorList { get; set; }

        public DoctorRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.DoctorList = new ObservableCollection<Doctor>();

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

            this.DoctorList.Add(doctor1);
            this.DoctorList.Add(doctor2);
            this.DoctorList.Add(doctor3);
            this.DoctorList.Add(doctor4);
            this.DoctorList.Add(doctor5);
            this.DoctorList.Add(doctor6);
            this.DoctorList.Add(doctor7);

            //if (File.Exists(dbPath))
            //    LoadDoctor();

        }

        public bool AddExaminationToDoctor(String doctorID, Examination examination)
        {
            foreach(Doctor doctor in this.DoctorList)
            {
                if (doctorID.Equals(doctor.Id))
                {
                    doctor.Examinations.Add(examination);
                    return true;
                }
            }
            return false;
        }

        public void EditDoctorsExamination(String doctorID, Examination newExamination)
        {
            foreach (Doctor doctor in this.DoctorList)
            {
                if (doctorID.Equals(doctor.Id))
                {
                    foreach(Examination exam in doctor.Examinations)
                    {
                        if (exam.Id.Equals(newExamination.Id))
                        {
                            exam.Id = newExamination.Id;
                            exam.ExamRoomId = newExamination.ExamRoomId;
                            exam.Duration = newExamination.Duration;
                            exam.Date = newExamination.Date;
                            exam.PatientId = newExamination.PatientId;
                            exam.EType = newExamination.EType;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        public bool LoadDoctor()
        {

            using FileStream stream = File.OpenRead(dbPath);
            this.DoctorList = JsonSerializer.Deserialize<ObservableCollection<Doctor>>(stream);

            return true;
        }



        public bool SaveDoctor()
        {
            string jsonString = JsonSerializer.Serialize(DoctorList);

            File.WriteAllText(dbPath, jsonString);
            return true;
        }



    }
}
