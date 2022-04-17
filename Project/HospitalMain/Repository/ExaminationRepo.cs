using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.IO;
using HospitalMain.Enums;

namespace Repository
{
    public class ExaminationRepo
    {
        public String dbPath { get; set; }
        //lista pregleda
        public ObservableCollection<Examination> examinationList;


        public ExaminationRepo(string dbPath)
        {
            this.dbPath = dbPath;
            this.examinationList = new ObservableCollection<Examination>();
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
            foreach(Examination exam in examinationList)
            {
                if(exam.Id == examination.Id)
                {
                    return false;
                }
            }

            examinationList.Add(examination);
            return true;
        }

        public Examination GetExamination(String examId)
        {
            foreach(Examination examination in examinationList)
            {
                if(examination.Id == examId)
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
                if (exam.Id == examID)
                {
                    exam.Id = examination.Id;
                    exam.ExamRoom = examination.ExamRoom;
                    exam.Date = examination.Date;
                    exam.Duration = examination.Duration;
                    exam.Doctor = examination.Doctor;
                    exam.Patient = examination.Patient;
                    exam.EType = examination.EType;
                    break;
                }
            }

        }

        public void DeleteExamination(Examination examination)
        {
            foreach(Examination exam in examinationList)
            {
                if(exam.Id == examination.Id)
                {
                    examinationList.Remove(examination);
                    break;
                }
            }

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
                if (exam.Doctor.Id.Equals(id)) 
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


        public List<Examination> GetFreeExaminationsForDoctor(Doctor doctor, DateTime startDate, DateTime endDate, bool priority, List<Doctor> doctors)
        {
            List<DateTime> examinationsTime = new List<DateTime>();
            List<DateTime> doctorsExaminationsTime = new List<DateTime>();
            List<Examination> doctorsExaminations = doctor.Examinations;

            //danasnji datum
            DateTime today = DateTime.Now;
            DateTime date = startDate.Date;
            DateTime end = endDate.Date;

            //koliki je korak
            int days = end.Day - date.Day;
            for (int i = 0; i < days + 1; ++i)
            {
                //za svaki dan generise koji sve termini postoje
                DateTime start = new DateTime(date.Year, date.Month, date.Day + i, 7, 0, 0);
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
                    examinations.Add(new Examination(new Room(), dt, "-1", 1, ExaminationTypeEnum.OrdinaryExamination, new Patient(), doctor));
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
                        DateTime start = new DateTime(date.Year, date.Month, date.Day + i, 7, 0, 0);
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
                            examinations.Add(new Examination(new Room(), dt, "-1", 1, ExaminationTypeEnum.OrdinaryExamination, new Patient(), doctor));
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
                                examinations.Add(new Examination(new Room(), dt, "-1", 1, ExaminationTypeEnum.OrdinaryExamination, new Patient(), doc));
                            }

                        }
                    }
                } 
            }
            


            return examinations;

        }

        public List<Examination> MoveExamination(Examination examination)
        {
            List<DateTime> examinationsTime = new List<DateTime>();
            List<DateTime> doctorsExaminationsTime = new List<DateTime>();
            List<Examination> doctorsExaminations = examination.Doctor.Examinations;


            //danasnji datum
            DateTime today = DateTime.Now;
            //dopustiti termine na pola sata tri dana unapred, radi od 8 do 4
            DateTime date = examination.Date; //uzima trenutno zakazani termin pa mu nudi 4 dana unapred da zakaze
            //sada mu se nudi samo 4 dana unapred da pomeri
            for (int i = 1; i < 5; ++i)
            {
                //date.AddDays(1);
                DateTime start = new DateTime(date.Year, date.Month, date.Day + i, 7, 0, 0);
                for (int j = 0; j < 16; ++j)
                {
                    examinationsTime.Add(start.AddMinutes(j * 30));
                }
            }

            List<Examination> examinations = new List<Examination>();
            foreach (DateTime dt in examinationsTime)
            {
                bool free = true;
                foreach (Examination doctorsExamination in ExaminationsForDoctor(examination.Doctor.Id))
                {
                    if (doctorsExamination.Date == dt)
                    {
                        free = false;
                    }
                }
                if (free)
                {
                    examinations.Add(new Examination(new Room(), dt, "-1", 1, ExaminationTypeEnum.OrdinaryExamination, new Patient(), examination.Doctor));
                }

            }
            return examinations;
        }

        public void EditExamination(string id, DateTime dateTime)
        {
            Examination examination = GetId(id);
            examination.Date = dateTime;
        }

        public void DoctorEditExamination(String id, Examination examination)
        {
            for (int i = 0; i < examinationList.Count; i++)
            {
                if (examinationList[i].Id.Equals(id))
                {
                    examinationList.Remove(examinationList[i]);
                    break;
                }
            }
            examinationList.Add(examination);
        }


    }
}