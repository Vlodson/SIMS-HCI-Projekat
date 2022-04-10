using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Repository
{
    public class ExaminationRepo
    {
        private String dbPath;
        //lista pregleda
        public ObservableCollection<Examination> examinationList = new ObservableCollection<Examination>();
        private List<Examination> examinationList1 = new List<Examination>();


        public ExaminationRepo(string dbPath)
        {
            this.dbPath = dbPath;
            //this.examinationList = examinationList;
            examinationList = new ObservableCollection<Examination>();
            List<Examination> examinations = new List<Examination>();
            Equipment equipment1 = new Equipment("name", 1);
            List<Equipment> equipmentList1 = new List<Equipment>();
            equipmentList1.Add(equipment1);
            Room r1 = new Room("name1", 1, 2, false, "type");

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Doctor doctor = new Doctor("idDoctor1", "nameDoctor1", "surnameDoctor1", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);

            List<Examination> examinationsPatient1 = new List<Examination>();
            DateTime dtPatient1 = DateTime.Now;
            Model.Patient patient = new Model.Patient("idPatient1", "namePatient1", "surnamePatient1", dtPatient1, examinationsPatient1);

            DateTime dtExam1 = new DateTime(2022, 4, 11, 7, 30, 0);
            Examination exam1 = new Examination(r1, dtExam1, "idExam1", 2, "kontrola", patient, doctor);
            
            examinations.Add(exam1);
            this.examinationList.Add(exam1);
            
        }

        public ExaminationRepo(string dbPath, List<Examination> examinationList1)
        {
            this.dbPath = dbPath;
            List<Examination> examinations = new List<Examination>();

            Room r1 = new Room("idRoom1",2, 1, false, "typeRoom1");

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Doctor doctor = new Doctor("idDoctor1", "nameDoctor1", "surnameDoctor1", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);

            List<Examination> examinationsPatient1 = new List<Examination>();
            DateTime dtPatient1 = DateTime.Now;
            Model.Patient patient = new Model.Patient("idPatient1", "namePatient1", "surnamePatient1", dtPatient1, examinationsPatient1);

            DateTime dtExam1 = DateTime.Now;
            Examination exam1 = new Examination(r1, dtExam1, "idExam1", 2, "kontrola", patient, doctor);
            examinations.Add(exam1);

            this.examinationList1 = examinations;
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
            examinationList1.Add(examination);
            examinationList.Add(examination);

        }

        public void DeleteExamination(Examination examination)
        {
            examinationList.Remove(examination);
            examinationList1.Remove(examination);

        }

        public bool LoadExamination()
        {
            throw new NotImplementedException();
        }

        public bool SaveExamination()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Examination> ExaminationsForPatient(string id)
        {
            ObservableCollection<Examination> examsForPatient = new ObservableCollection<Examination>();
            foreach (Examination exam in examinationList1)
            {
                if (exam.Patient.Id.Equals(id)) examsForPatient.Add(exam);
            }
            return examsForPatient;
        }

        //public PatientService patientService;
        //public DoctorService doctorService;


        public List<DateTime> GetFreeExaminationsForDoctor(Doctor doctor)
        {
            List<DateTime> examinationsTime = new List<DateTime>();
            List<DateTime> doctorsExaminationsTime = new List<DateTime>();
            List<Examination> doctorsExaminations = doctor.getExaminations();
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
            foreach (Examination exam in examinationList1)
            {
                //if (exam.doctor.getId().Equals(id)) 
                examsForDoctor.Add(exam);
            }
            return examsForDoctor;
        }


    }
}