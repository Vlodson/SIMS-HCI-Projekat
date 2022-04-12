using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;

namespace Repository
{
    public class ExaminationRepo
    {
        public String dbPath { get; set; }
        //lista pregleda
        public ObservableCollection<Examination> examinationList = new ObservableCollection<Examination>();


        public ExaminationRepo(string dbPath)
        {
            this.dbPath = dbPath;
            
        }

        public ObservableCollection<Examination> GetAll()
        {
            return examinationList;
        }

        public void DeleteByPatient(String id)
        {
            examinationList.Remove(GetId(id));
        }

        public Examination GetId(String id)
        {
            foreach(Examination examination in examinationList)
            {
                if(examination.Id == id)
                {
                    return examination;
                }
            }
            return null;
        }
        public bool NewExamination(Examination examination)
        {
            //examinationList.Add(examination);
            return true;
        }

        public Examination GetExamination(String examId)
        {
            throw new NotImplementedException();
        }

        public void SetExamination(Examination examination)
        {
            examinationList.Add(examination);

        }

        public void DeleteExamination(Examination examination)
        {
            examinationList.Remove(examination);

        }

        public bool LoadExamination()
        {
            
            using FileStream stream = File.OpenRead(dbPath);
            this.examinationList = JsonSerializer.Deserialize<ObservableCollection<Examination>>(stream);
            
            return true;
        }

        public bool SaveExamination()
        {
            string jsonString = JsonSerializer.Serialize(examinationList);

            File.WriteAllText(dbPath, jsonString);
            return true;
        }

        public ObservableCollection<Examination> ExaminationsForPatient(string id)
        {
            ObservableCollection<Examination> examsForPatient = new ObservableCollection<Examination>();
            //bilo je jedan
            foreach (Examination exam in examinationList)
            {
                if (exam.Patient.ID.Equals(id)) examsForPatient.Add(exam);
            }
            return examsForPatient;
        }

        //public PatientService patientService;
        //public DoctorService doctorService;


        public List<DateTime> GetFreeExaminationsForDoctor(Doctor doctor)
        {
            List<DateTime> examinationsTime = new List<DateTime>();
            List<DateTime> doctorsExaminationsTime = new List<DateTime>();
            List<Examination> doctorsExaminations = doctor.Examinations;
            //sutra
            DateTime today = DateTime.Now;
            

            //moze da zakaze dva dana unapred
            //popuni zauzetim za doktora
            /*
            foreach(Examination exam in examinationList)
            {
                if (doctor.getId().Equals(exam.Doctor.getId()))
                {
                    doctorsExaminationsTime.Add(exam.Date);
                }
            }
            
            

            DateTime startTime = new DateTime(today.Year, today.Month, today.Day, 7, 0, 0);
            for (int i = 1; i < 3; ++i)
            {
                today = startTime.AddDays(i);
                for (int j = 0; j < 24; ++j)
                {
                    DateTime dt = today.AddMinutes(j * 30);
                    
                    if (doctorsExaminationsTime.Count == 0)
                    {
                        examinationsTime.Add(dt);
                    }
                    
                    foreach (DateTime dateTime in doctorsExaminationsTime)
                    {
                        if (dateTime.CompareTo(dt) != 0)
                        {
                            examinationsTime.Add(dt);
                        }
                    }
                }
                
            }
            */
            examinationsTime.Add(new DateTime(2021, 1, 1, 15, 15, 15));
            examinationsTime.Add(new DateTime(2021, 2, 1, 12, 12, 12));
            examinationsTime.Add(new DateTime(2021, 3, 1, 12, 12, 12));
            examinationsTime.Add(new DateTime(2021, 4, 1, 12, 12, 12));
            return examinationsTime;

        }

        public void EditExamination(string id, DateTime dateTime)
        {
            Examination examination = GetId(id);
            examination.Date = dateTime;
        }

        public ObservableCollection<Examination> ExaminationsForDoctor(string id)
        {
            ObservableCollection<Examination> examsForDoctor = new ObservableCollection<Examination>();
            //bilo je 1
            foreach (Examination exam in examinationList)
            {
                //if (exam.doctor.getId().Equals(id)) 
                examsForDoctor.Add(exam);
            }
            return examsForDoctor;
        }

        public List<Examination> ReadAll(string id)
        {
            List<Examination> examsForDoctor = new List<Examination>();
            foreach (Examination exam in examinationList)
            {
                //if (exam.doctor.getId().Equals(id)) 
                examsForDoctor.Add(exam);
            }
            return examsForDoctor;
        }


    }
}