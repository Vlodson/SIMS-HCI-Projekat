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
        public Examination TemporaryExam { get; set; }
        public int ValidationCounter { get; set; }

        public ExaminationRepo(String dbPath)
        {
            this.dbPath = dbPath;
            examinationList = new ObservableCollection<Examination>();

            /*Examination exam1 = new Examination("idRoom", new DateTime(2022, 6,6,12,12,0), "idExam", 30, ExaminationTypeEnum.Surgery, "1", "d1");
            Examination exam2 = new Examination("idRoom1", new DateTime(2021, 2, 2, 12, 12, 0), "idExam1", 30, ExaminationTypeEnum.Surgery, "1", "d11");
            examinationList.Add(exam1);
            examinationList.Add(exam2);*/

            Examination exam1 = new Examination("12", new DateTime(2022, 6, 5, 12, 00, 00), "1", 30, ExaminationTypeEnum.OrdinaryExamination, "1", "d1");
            Examination exam2 = new Examination("11", new DateTime(2022, 6, 5, 12, 30, 00), "2", 30, ExaminationTypeEnum.OrdinaryExamination, "2", "d11");
            Examination exam3 = new Examination("14", new DateTime(2022, 6, 5, 11, 00, 00), "3", 30, ExaminationTypeEnum.Surgery, "3", "d13");
            Examination exam4 = new Examination("14", new DateTime(2022, 6, 5, 15, 00, 00), "4", 30, ExaminationTypeEnum.Surgery, "2", "d13");
            Examination exam5 = new Examination("10A", new DateTime(2022, 6, 6, 11, 00, 00), "5", 30, ExaminationTypeEnum.OrdinaryExamination, "4", "d12");
            Examination exam6 = new Examination("15", new DateTime(2022, 6, 6, 8, 30, 00), "6", 30, ExaminationTypeEnum.OrdinaryExamination, "1", "d11");
            Examination exam7 = new Examination("14", new DateTime(2022, 6, 6, 8, 30, 00), "7", 30, ExaminationTypeEnum.Surgery, "4", "d13");
            Examination exam8 = new Examination("12", new DateTime(2022, 6, 6, 9, 00, 00), "8", 30, ExaminationTypeEnum.OrdinaryExamination, "1", "d12");
            Examination exam9 = new Examination("10A", new DateTime(2022, 6, 7, 14, 00, 00), "9", 30, ExaminationTypeEnum.OrdinaryExamination, "3", "d1");
            Examination exam10 = new Examination("11", new DateTime(2022, 6, 7, 10, 00, 00), "10", 30, ExaminationTypeEnum.OrdinaryExamination, "2", "d14");

            this.examinationList.Add(exam1);
            this.examinationList.Add(exam2);
            this.examinationList.Add(exam3);
            this.examinationList.Add(exam4);
            this.examinationList.Add(exam5);
            this.examinationList.Add(exam6);
            this.examinationList.Add(exam7);
            this.examinationList.Add(exam8);
            this.examinationList.Add(exam9);
            this.examinationList.Add(exam10);

            if (File.Exists(dbPath))
                LoadExamination();

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

        public void DeleteExamination(String id)
        {
            examinationList.Remove(GetExaminationById(id));
            SaveExamination();
        }

        public Examination GetExaminationById(String id)
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

        public ObservableCollection<Examination> GetFreeExaminations(DateTime startDate, DateTime endDate, ObservableCollection<Doctor> doctorsWithSameSpecialization)
        {
            //Svi termini pregleda u ustanovi u zadatom periodu
            ObservableCollection<DateTime> examinationsInSomeRange = new ObservableCollection<DateTime>();

            //Vremena
            DateTime start = startDate.Date;
            DateTime end = endDate.Date;

            //opseg dana
            int days = Convert.ToInt32((end - start).TotalDays);
            for(int i = 0; i < days + 1; i++)
            {
                //za svaki dan se generisu termini
                DateTime firstExamInDay = new DateTime(start.AddDays(i).Year, start.AddDays(i).Month, start.AddDays(i).Day, 7, 0, 0);
                for(int j = 0; j < 16; j++)
                {
                    examinationsInSomeRange.Add(firstExamInDay.AddMinutes(j*30));
                }
            }

            ObservableCollection<Examination> examinations = new ObservableCollection<Examination>();

            foreach (Doctor doctor in doctorsWithSameSpecialization)
            {
                foreach(DateTime dt in examinationsInSomeRange)
                {
                    bool freeExam = true;
                    foreach(Examination exam in ExaminationsForDoctor(doctor.Id))
                    {
                        int com = DateTime.Compare(dt, exam.Date);
                        if(com == 0)
                        {
                            freeExam = false;
                            break;
                        }
                    }
                    if (freeExam)
                    {
                        examinations.Add(new Examination("", dt, "-1", 30, ExaminationTypeEnum.OrdinaryExamination, "", doctor.Id));
                    }

                }
            }

            //situuacija ako prvog dana nema nijedan slobodan termin, trazice u narednim danima sve dok ne nadje neki slobodan
            while(examinations.Count == 0)
            {
                end = end.AddDays(1);

                days = Convert.ToInt32((end - start).TotalDays);
                for (int i = 0; i < days + 1; i++)
                {
                    //za svaki dan se generisu termini
                    DateTime firstExamInDay = new DateTime(start.AddDays(i).Year, start.AddDays(i).Month, start.AddDays(i).Day, 7, 0, 0);
                    for (int j = 0; j < 16; j++)
                    {
                        examinationsInSomeRange.Add(firstExamInDay.AddMinutes(j * 30));
                    }
                }

                foreach (Doctor doctor in doctorsWithSameSpecialization)
                {
                    foreach (DateTime dt in examinationsInSomeRange)
                    {
                        bool freeExam = true;
                        foreach (Examination exam in ExaminationsForDoctor(doctor.Id))
                        {
                            int com = DateTime.Compare(dt, exam.Date);
                            if (com == 0)
                            {
                                freeExam = false;
                                break;
                            }
                        }
                        if (freeExam)
                        {
                            examinations.Add(new Examination("", dt, "-1", 30, ExaminationTypeEnum.OrdinaryExamination, "", doctor.Id));
                        }

                    }
                }
            }

            return examinations;
        }

        public List<DateTime> GenerateExaminationTimes(DateTime startDate, DateTime endDate)
        {
            List<DateTime> examinationTimes = new List<DateTime>();
            int days = Convert.ToInt32((endDate.Date - startDate.Date).TotalDays);
            for (int i = 0; i < days + 1; ++i)
            {
                //za svaki dan generise koji sve termini postoje
                DateTime start = new DateTime(startDate.Date.AddDays(i).Year, startDate.Date.AddDays(i).Month, startDate.Date.AddDays(i).Day, 7, 0, 0);
                for (int j = 0; j < 16; ++j)
                {
                    examinationTimes.Add(start.AddMinutes(j * 30));
                }
            }
            return examinationTimes;
        }

        public bool CheckDoctorsExaminationExists(String doctorId, DateTime dateTime)
        {
            foreach (Examination doctorsExamination in ExaminationsForDoctor(doctorId))
            {
                if (doctorsExamination.Date == dateTime)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Examination> GenerateFreeExaminationTimes(List<DateTime> generatedExaminationTimes, Doctor doctor)
        {
            List<Examination> freeExaminationTimes = new List<Examination>();
            foreach (DateTime dt in generatedExaminationTimes)
            {
                bool free = CheckDoctorsExaminationExists(doctor.Id, dt);
                if (free)
                {
                    freeExaminationTimes.Add(new Examination("", dt, "-1", 1, ExaminationTypeEnum.OrdinaryExamination, "", doctor.Id));
                }
            }
            return freeExaminationTimes;
        }

        
        public List<Examination> GenerateDoctorFreeExaminations(Doctor doctor, DateTime startDate, DateTime endDate)
        {
            List<DateTime> examinationsTime = GenerateExaminationTimes(startDate, endDate);
            List<Examination> examinations = GenerateFreeExaminationTimes(examinationsTime, doctor);
            
            return examinations;
        }
        
        /*
        public List<Examination> GetFreeExaminationsForDoctor(Doctor doctor, DateTime startDate, DateTime endDate, bool priority, ObservableCollection<Doctor> doctors)
        {
            List<DateTime> examinationsTime = GenerateExaminationTimes(startDate, endDate);
            List<Examination> examinations = GenerateFreeExaminationTimes(examinationsTime, doctor);
            //List<Examination> examinations = GenerateDoctorFreeExaminations(doctor, startDate, endDate);
            //provera koji je prioritet izabran, true je lekar, false termin
            if (priority)
            {
                //ako nema pregleda i prioritet je lekar, nudi 4 dana pre i posle zeljenih dana dostuone termine
                if (examinations.Count == 0)
                {
                    DateTime before = startDate.Date.AddDays(-4);
                    if (before.CompareTo(DateTime.Now) < 0)
                    {
                        before = DateTime.Now;
                    }
                    DateTime after = startDate.Date.AddDays(4);
                    int days = after.Day - before.Day;
                    List<DateTime> newExaminationTimes = GenerateExaminationTimes(before, after);
                    examinationsTime.AddRange(newExaminationTimes);
                    List<Examination> newExaminations = GenerateFreeExaminationTimes(newExaminationTimes, doctor);
                }
            }
            else
            {
                //prolazi kroz sve lekare u zeljenom terminu
                if(examinations.Count == 0)
                {
                    foreach (Doctor doc in doctors)
                    {
                        List<Examination> newExaminations = GenerateFreeExaminationTimes(examinationsTime, doctor);
                        examinations.AddRange(newExaminations);
                    }
                } 
            }
            return examinations;
        }
        */

        public List<Examination> GetMovingDatesForExamination(Examination examination, Doctor doctor)
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

        public List<String> GetPatientsDoctors(String patientId)
        {
            List<String> doctors = new List<String>();
            foreach(Examination examination in ExaminationsForPatient(patientId))
            {
                if(!DoctorExists(examination.DoctorId, doctors) && (examination.Date.CompareTo(DateTime.Now) < 0))
                {
                    doctors.Add(examination.DoctorId);
                } 
            }
            return doctors;
        }

        public bool DoctorExists(String doctorId, List<String> doctors)
        {
            bool exists = false;
            foreach (String id in doctors)
            {
                if (id.Equals(doctorId))
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

    }
}