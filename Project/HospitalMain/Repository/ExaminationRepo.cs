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
        public String dbPath { get; set; }
        public ObservableCollection<Examination> examinationList { get; set; }

        public ExaminationRepo(String dbPath)
        {
            this.dbPath = dbPath;
            examinationList = new ObservableCollection<Examination>();

            /*Examination exam1 = new Examination("idRoom", new DateTime(2022, 6,6,12,12,0), "idExam", 30, ExaminationTypeEnum.Surgery, "1", "d1");
            Examination exam2 = new Examination("idRoom1", new DateTime(2021, 2, 2, 12, 12, 0), "idExam1", 30, ExaminationTypeEnum.Surgery, "1", "d11");
            examinationList.Add(exam1);
            examinationList.Add(exam2);*/

            Examination exam1 = new Examination("12", new DateTime(2022, 6, 5, 12, 20, 00), "1", 30, ExaminationTypeEnum.OrdinaryExamination, "1", "d1");
            Examination exam2 = new Examination("11", new DateTime(2022, 6, 5, 12, 50, 00), "2", 30, ExaminationTypeEnum.OrdinaryExamination, "2", "d11");
            Examination exam3 = new Examination("14", new DateTime(2022, 6, 6, 11, 00, 00), "3", 30, ExaminationTypeEnum.OrdinaryExamination, "3", "d11");

            this.examinationList.Add(exam1);
            this.examinationList.Add(exam2);
            this.examinationList.Add(exam3);

            if (File.Exists(dbPath))
                LoadExamination();
            //SaveExamination();

        }

        public ObservableCollection<Examination> GetAll()
        {
            return examinationList;
        }

        public int generateID(ObservableCollection<Examination> examinations)
        {
            int maxID = 0;

            foreach(Examination exam in examinations)
            {
                int examID = Int32.Parse(exam.Id);
                if (maxID < examID)
                {
                    maxID = examID;
                }
            }

            return maxID + 1;
        }

        public void DeleteByPatient(String id)
        {
            examinationList.Remove(GetId(id));
            SaveExamination();
        }

        public Examination GetId(String id)
        {
            foreach(Examination examination in examinationList)
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
            
            foreach(Examination exam in examinationList)
            {
                if(exam.Id.Equals(examination.Id))
                {
                    return false;
                }
            }
            
            

            examinationList.Add(examination);
            SaveExamination();
            return true;
        }

        public Examination GetExamination(String examId)
        {
            foreach(Examination examination in examinationList)
            {
                if(examination.Id.Equals(examId))
                {
                    return (examination);
                }
            }

            return (null);
        }

        public void SetExamination(string examID, Examination examination)
        {
            
            foreach(Examination exam in examinationList)
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

        public void DeleteExamination(Examination examination)
        {
            foreach(Examination exam in examinationList)
            {
                if(exam.Id.Equals(examination.Id))
                {
                    examinationList.Remove(examination);
                    break;
                }
            }
            SaveExamination();
        }

        public bool LoadExamination()
        {
            
            using FileStream stream = File.OpenRead(dbPath);
            examinationList = JsonSerializer.Deserialize<ObservableCollection<Examination>>(stream);
            
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
                if (exam.DoctorId.Equals(id)) 
                examsForDoctor.Add(exam);
            }
            return examsForDoctor;
        }

        public ObservableCollection<Examination> ExaminationsForPatient(string id)
        {
            ObservableCollection<Examination> examsForPatient = new ObservableCollection<Examination>();
            foreach (Examination exam in examinationList)
            {
                if (exam.PatientId.Equals(id)) examsForPatient.Add(exam);
            }
            return examsForPatient;
        }

        //public PatientService patientService;
        //public DoctorService doctorService;


        public List<Examination> GetFreeExaminationsForDoctor(Doctor doctor, DateTime startDate, DateTime endDate, bool priority, ObservableCollection<Doctor> doctors)
        {
            List<DateTime> examinationsTime = new List<DateTime>();
            List<DateTime> doctorsExaminationsTime = new List<DateTime>();
            List<Examination> doctorsExaminations = doctor.Examinations;

            //danasnji datum
            DateTime today = DateTime.Now;
            DateTime date = startDate.Date;
            DateTime end = endDate.Date;

            //koliki je korak
            //int days = end.Day - date.Day;
            int days = Convert.ToInt32((end - date).TotalDays);
            for (int i = 0; i < days + 1; ++i)
            {
                //za svaki dan generise koji sve termini postoje
                DateTime start = new DateTime(date.AddDays(i).Year, date.AddDays(i).Month, date.AddDays(i).Day, 7, 0, 0);
                for(int j = 0; j < 16; ++j)
                {
                    examinationsTime.Add(start.AddMinutes(j*30));
                }
            }

            List<Examination> examinations = new List<Examination>();
            //proverava da li su termini kod izabranog doktora zauzeti
            foreach(DateTime dt in examinationsTime)
            {
                bool free = true;
                foreach(Examination doctorsExamination in ExaminationsForDoctor(doctor.Id))
                {
                    if(doctorsExamination.Date == dt)
                    {
                        free = false;
                    }
                }
                if (free)
                {
                    examinations.Add(new Examination("", dt, "-1", 1, ExaminationTypeEnum.OrdinaryExamination, "", doctor.Id));
                }
                
            }

            //provera koji je prioritet izabran, true je lekar, false termin
            if (priority)
            {
                //ako nema pregleda i prioritet je lekar, nudi 4 dana pre i posle zeljenih dana dostuone termine
                if (examinations.Count == 0)
                {
                    DateTime before = date.AddDays(-4);
                    if (before.CompareTo(today) < 0)
                    {
                        before = today;
                    }
                    DateTime after = date.AddDays(4);
                    days = after.Day - before.Day;
                    for (int i = 0; i < days + 1; ++i)
                    {
                        //date.AddDays(1);
                        //DateTime start = new DateTime(date.Year, date.Month, date.Day + i, 7, 0, 0);
                        DateTime start = new DateTime(date.AddDays(i).Year, date.AddDays(i).Month, date.AddDays(i).Day, 7, 0, 0);
                        for (int j = 0; j < 16; ++j)
                        {
                            examinationsTime.Add(start.AddMinutes(j * 30));
                        }
                    }
                    foreach (DateTime dt in examinationsTime)
                    {
                        bool free = true;
                        foreach (Examination doctorsExamination in ExaminationsForDoctor(doctor.Id))
                        {
                            if (doctorsExamination.Date == dt)
                            {
                                free = false;
                            }
                        }
                        if (free)
                        {
                            examinations.Add(new Examination("", dt, "-1", 1, ExaminationTypeEnum.OrdinaryExamination, "", doctor.Id));
                        }

                    }

                }
            }
            else
            {
                //prolazi kroz sve lekare u zeljenom terminu
                if(examinations.Count == 0)
                {

                    foreach (Doctor doc in doctors)
                    {
                        foreach (DateTime dt in examinationsTime)
                        {
                            bool free = true;
                            foreach (Examination doctorsExamination in ExaminationsForDoctor(doc.Id))
                            {
                                if (doctorsExamination.Date == dt)
                                {
                                    free = false;
                                }
                            }
                            if (free)
                            {
                                examinations.Add(new Examination("", dt, "-1", 1, ExaminationTypeEnum.OrdinaryExamination, "", doc.Id));
                            }

                        }
                    }
                } 
            }
            


            return examinations;

        }

        public List<Examination> MoveExamination(Examination examination, Doctor doctor)
        {
            List<DateTime> examinationsTime = new List<DateTime>();
            List<DateTime> doctorsExaminationsTime = new List<DateTime>();
            List<Examination> doctorsExaminations = doctor.Examinations;


            //danasnji datum
            DateTime today = DateTime.Now;
            //dopustiti termine na pola sata tri dana unapred, radi od 8 do 4
            DateTime date = examination.Date; //uzima trenutno zakazani termin pa mu nudi 4 dana unapred da zakaze
            //sada mu se nudi samo 4 dana unapred da pomeri
            for (int i = 1; i < 5; ++i)
            {
                //date.AddDays(1);
                DateTime start = new DateTime(date.AddDays(i).Year, date.AddDays(i).Month, date.AddDays(i).Day, 7, 0, 0);
                for (int j = 0; j < 16; ++j)
                {
                    examinationsTime.Add(start.AddMinutes(j * 30));
                }
            }

            List<Examination> examinations = new List<Examination>();
            //proverava da li su termini kod izabranog doktora zauzeti
            foreach (DateTime dt in examinationsTime)
            {
                bool free = true;
                foreach (Examination doctorsExamination in ExaminationsForDoctor(doctor.Id))
                {
                    if (doctorsExamination.Date == dt)
                    {
                        free = false;
                    }
                }
                if (free)
                {
                    examinations.Add(new Examination("", dt, "-1", 1, ExaminationTypeEnum.OrdinaryExamination, "", doctor.Id));
                }

            }
            return examinations;
        }

        public void EditExamination(string id, DateTime dateTime, Room room)
        {
            Examination examination = GetId(id);
            examination.Date = dateTime;
            examination.ExamRoomId = room.Id;
        }

        public void DoctorEditExamination(String id, Examination examination)
        {
            for (int i = 0; i < examinationList.Count; i++)
            {
                if (examinationList[i].Id.Equals(id))
                {
                    examinationList[i] = examination;
                    break;
                }
            }
        }

        public ObservableCollection<Examination> ReadEndedExams()
        {
            ObservableCollection<Examination> endedExams = new ObservableCollection<Examination>();
                foreach (Examination exam in this.examinationList)
                {
                    int res = DateTime.Compare(exam.Date, DateTime.Now);
                    if (res < 0)
                        endedExams.Add(exam);

                }
            return endedExams;
        }


        public List<Examination> getExamByTime(DateTime dateTime)
        {
            List<Examination> returnList = new List<Examination> ();
            foreach(Examination examination in this.examinationList)
            {
                if (examination.Date.Equals(dateTime))
                {
                    returnList.Add(examination);
                }
            }
            return returnList;
        }

        public bool occupiedDate(DateTime dt)
        {
            foreach(Examination exam in this.examinationList)
            {
                if (exam.Date.Equals(dt))
                    return true;
            }
            return false;
        }



    }
}