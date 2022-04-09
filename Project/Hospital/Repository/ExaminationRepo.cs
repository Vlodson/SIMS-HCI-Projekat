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
        private List<Examination> examinationList;


        public ExaminationRepo(string dbPath, List<Examination> examinationList)
        {
            this.dbPath = dbPath;
            //this.examinationList = examinationList;
            List<Examination> examinations = new List<Examination>();
            Equipment equipment1 = new Equipment();
            List<Equipment> equipmentList1 = new List<Equipment>();
            equipmentList1.Add(equipment1);
            Room r1 = new Room();

            List<Examination> examinationsDoctor1 = new List<Examination>();
            DateTime dtDoctor1 = DateTime.Now;
            Doctor doctor = new Doctor("idDoctor1", "nameDoctor1", "surnameDoctor1", dtDoctor1, DoctorType.Pulmonology, examinationsDoctor1);

            List<Examination> examinationsPatient1 = new List<Examination>();
            DateTime dtPatient1 = DateTime.Now;
            Model.Patient patient = new Model.Patient("idPatient1", "namePatient1", "surnamePatient1", dtPatient1, examinationsPatient1);

            DateTime dtExam1 = DateTime.Now;
            Examination exam1 = new Examination(r1, dtExam1, "idExam1", 2, patient, doctor);
            examinations.Add(exam1);

            this.examinationList = examinations;
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

        public bool DeleteExamination(Examination examination)
        {
            return examinationList.Remove(examination);
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
            foreach (Examination exam in examinationList)
            {
                if (exam.patient.getId().Equals(id)) examsForPatient.Add(exam);
            }
            return examsForPatient;
        }

        //public PatientService patientService;
        //public DoctorService doctorService;


        public List<DateTime> GetFreeExaminationsForDoctor(Doctor doctor)
        {
            List<DateTime> examinationsTime = new List<DateTime>();
            List<Examination> doctorsExaminations = doctor.getExaminations();
            //sutra
            DateTime today = DateTime.Now;

            //moze se zakazati 7 dana unapred
            DateTime startTime = new DateTime(today.Year, today.Month, today.Day, 7, 0, 0);
            for (int i = 1; i < 7; ++i)
            {
                today = today.AddDays(i);
                for (int j = 0; j < 24; ++j)
                {
                    foreach (Examination exam in doctorsExaminations)
                    {
                        if (exam.Date.CompareTo(today.AddMinutes(j * 30)) != 0)
                        {
                            examinationsTime.Add(today.AddMinutes(j * 30));
                        }
                    }
                }
            }
            return examinationsTime;

        }


    }
}