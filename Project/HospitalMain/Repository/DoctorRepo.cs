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
            Doctor doctor2 = new Doctor("d11", "Jovan", "Petrovic", dtDoctor2, DoctorType.Cardiology, 20, examinationsDoctor2);

            List<Examination> examinationsDoctor3 = new List<Examination>();
            DateTime dtDoctor3 = DateTime.Now;
            Doctor doctor3 = new Doctor("d12", "Miroslav", "Katic", dtDoctor3, DoctorType.specialistCheckup, 20, examinationsDoctor3);

            List<Examination> examinationsDoctor4 = new List<Examination>();
            DateTime dtDoctor4 = DateTime.Now;
            Doctor doctor4 = new Doctor("d13", "Andrija", "Stanisic", dtDoctor4, DoctorType.operation, 20, examinationsDoctor4);

            List<Examination> examinationsDoctor5 = new List<Examination>();
            DateTime dtDoctor5 = DateTime.Now;
            Doctor doctor5 = new Doctor("d14", "Milos", "Gravara", dtDoctor5, DoctorType.Pulmonology, 20, examinationsDoctor5);

            this.doctorList.Add(doctor1);
            this.doctorList.Add(doctor2);
            this.doctorList.Add(doctor3);
            this.doctorList.Add(doctor4);
            this.doctorList.Add(doctor5);

            if (File.Exists(dbPath))
                LoadDoctor();

        }

        public ObservableCollection<Doctor> GetAllDoctors()
        {
            return doctorList;
        }

        public ObservableCollection<Doctor> GetDoctorsByType(DoctorType doctorType)
        {
            ObservableCollection<Doctor> listOfDoctors = new ObservableCollection<Doctor>();

            foreach(Doctor doctor in this.doctorList)
            {
                if(doctor.Type == doctorType)
                {
                    listOfDoctors.Add(doctor);
                }
            }

            return listOfDoctors;
        }

        public DoctorType GetDoctorsType(string doctorID)
        {
            foreach(Doctor doctor in this.doctorList)
            {
                if (doctor.Id.Equals(doctorID))
                {
                    return doctor.Type;
                }
            }
            return DoctorType.None;
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

        public bool AddExaminationToDoctor(String doctorID, Examination examination)
        {
            foreach(Doctor doctor in this.doctorList)
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
            foreach (Doctor doctor in this.doctorList)
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
