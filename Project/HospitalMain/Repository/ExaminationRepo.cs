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

        public ObservableCollection<Examination> ExaminationsForDoctor(string id)
        {
            ObservableCollection<Examination> examsForDoctor = new ObservableCollection<Examination>();
            foreach (Examination exam in examinationList)
            {
                //if (exam.doctor.getId().Equals(id)) 
                examsForDoctor.Add(exam);
            }
            return examsForDoctor;
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


        public List<Examination> GetFreeExaminationsForDoctor(Doctor doctor)
        {
            List<DateTime> examinationsTime = new List<DateTime>();
            List<DateTime> doctorsExaminationsTime = new List<DateTime>();
            List<Examination> doctorsExaminations = doctor.Examinations;
            

            //danasnji datum
            DateTime today = DateTime.Now;
            //dopustiti termine na pola sata tri dana unapred, radi od 8 do 4
            DateTime date = DateTime.Now;
            int year = today.Year;
            int month = today.Month;
            int day = today.Day;
            int minutes = 0;
            int hour = 7;
            for (int i = 0; i < 3; ++i)
            {
                //date.AddDays(1);
                DateTime start = new DateTime(date.Year, date.Month, date.Day + i, 7, 0, 0);
                for(int j = 0; j < 16; ++j)
                {
                    examinationsTime.Add(start.AddMinutes(j*30));
                }
            }

            List<Examination> examinations = new List<Examination>();
            foreach(DateTime dt in examinationsTime)
            {
                examinations.Add(new Examination(new Room(), dt, "-1", 1, "pregled", new Patient(), doctor));
            }
            //examinationsTime.Add(new DateTime(2021, 1, 1, 15, 15, 15));
            //examinationsTime.Add(new DateTime(2021, 2, 1, 12, 12, 12));
            //examinationsTime.Add(new DateTime(2021, 3, 1, 12, 12, 12));
            //examinationsTime.Add(new DateTime(2021, 4, 1, 12, 12, 12));
            return examinations;

        }

        public void EditExamination(string id, DateTime dateTime)
        {
            Examination examination = GetId(id);
            examination.Date = dateTime;
        }


    }
}