using Model;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;
using HospitalMain.Enums;
using Utility;
using System.Collections.Generic;

namespace Repository
{
    public class ExaminationRepo
    {
        public String DBPath { get; set; }
        public ObservableCollection<Examination> Examinations { get; set; }
        public Examination TemporaryExam { get; set; }
        public int ValidationCounter { get; set; }

        public ExaminationRepo(String dbPath)
        {
            this.DBPath = dbPath;
            Examinations = new ObservableCollection<Examination>();

            Examination exam1 = new Examination("1", new DateTime(2022, 6, 5, 12, 00, 00), "1", 30, ExaminationTypeEnum.OrdinaryExamination, "3", "d1");
            Examination exam2 = new Examination("1", new DateTime(2022, 6, 5, 12, 30, 00), "2", 30, ExaminationTypeEnum.OrdinaryExamination, "2", "d11");
            Examination exam3 = new Examination("2", new DateTime(2022, 6, 5, 11, 00, 00), "3", 30, ExaminationTypeEnum.Surgery, "3", "d13");
            Examination exam4 = new Examination("5", new DateTime(2022, 6, 5, 15, 00, 00), "4", 30, ExaminationTypeEnum.Surgery, "2", "d13");
            Examination exam5 = new Examination("4", new DateTime(2022, 6, 6, 11, 00, 00), "5", 30, ExaminationTypeEnum.OrdinaryExamination, "4", "d12");
            Examination exam6 = new Examination("13", new DateTime(2022, 6, 6, 8, 30, 00), "6", 30, ExaminationTypeEnum.OrdinaryExamination, "1", "d11");
            Examination exam7 = new Examination("8", new DateTime(2022, 6, 6, 8, 30, 00), "7", 30, ExaminationTypeEnum.Surgery, "4", "d13");
            Examination exam8 = new Examination("13", new DateTime(2022, 6, 6, 9, 00, 00), "8", 30, ExaminationTypeEnum.OrdinaryExamination, "1", "d12");
            Examination exam9 = new Examination("10", new DateTime(2022, 6, 7, 14, 00, 00), "9", 30, ExaminationTypeEnum.OrdinaryExamination, "3", "d1");
            Examination exam10 = new Examination("7", new DateTime(2022, 6, 7, 10, 00, 00), "10", 30, ExaminationTypeEnum.OrdinaryExamination, "2", "d14");

            this.Examinations.Add(exam1);
            this.Examinations.Add(exam2);
            this.Examinations.Add(exam3);
            this.Examinations.Add(exam4);
            this.Examinations.Add(exam5);
            this.Examinations.Add(exam6);
            this.Examinations.Add(exam7);
            this.Examinations.Add(exam8);
            this.Examinations.Add(exam9);
            this.Examinations.Add(exam10);

            //SaveExamination();
            if (File.Exists(dbPath))
                LoadExamination();

        }

        public void RemoveExamination(String id)
        {
            Examinations.Remove(GetExaminationById(id));
            SaveExamination();
        }

        public Examination GetExaminationById(String id)
        {
            foreach(Examination examination in Examinations)
            {
                if(examination.Id.Equals(id))
                {
                    return examination;
                }
            }
            return null;
        }
        public bool NewExamination(Examination examination)
        {
            
            foreach(Examination exam in Examinations)
            {
                if(exam.Id.Equals(examination.Id))
                {
                    return false;
                }
            }
            Examinations.Add(examination);
            SaveExamination();
            return true;
        }

        public void SetExamination(string examID, Examination examination)
        {
            
            foreach(Examination exam in Examinations)
            {
                if (exam.Id.Equals(examID))
                {
                    exam.Id = examination.Id;
                    exam.ExamRoomId = examination.ExamRoomId;
                    exam.Date = examination.Date;
                    exam.Duration = examination.Duration;
                    exam.DoctorId = examination.DoctorId;
                    exam.PatientId = examination.PatientId;
                    exam.EType = examination.EType;
                    break;
                }
            }
            SaveExamination();
        }

        public bool LoadExamination()
        {
            
            using FileStream stream = File.OpenRead(DBPath);
            Examinations = JsonSerializer.Deserialize<ObservableCollection<Examination>>(stream);
            
            return true;
        }

        public bool SaveExamination()
        {
            string jsonString = JsonSerializer.Serialize(Examinations);

            File.WriteAllText(DBPath, jsonString);
            return true;
        }

        //moze da se prebaci za lekara
        public List<Examination> getExamByTime(DateTime dateTime)
        {
            List<Examination> returnList = new List<Examination> ();
            foreach(Examination examination in this.Examinations)
            {
                if (examination.Date.Equals(dateTime))
                {
                    returnList.Add(examination);
                }
            }
            return returnList;
        }

    }
}