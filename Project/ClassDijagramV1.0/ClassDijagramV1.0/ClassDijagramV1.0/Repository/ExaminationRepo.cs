using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Repository
{
   public class ExaminationRepo
   {
      private String dbPath;
      //lista pregleda
      List<Examination> examinationList;

        public ExaminationRepo(string dbPath, List<Examination> examinationList)
        {
            this.dbPath = dbPath;
            //this.examinationList = examinationList;
            List<Examination> examinations = new List<Examination>();
            Equipment equipment1 = new Equipment("typeEquipment1", 2);
            List<Equipment> equipmentList1 = new List<Equipment>();
            equipmentList1.Add(equipment1);
            Room r1 = new Room("idRoom1", equipmentList1, 2, 1, false, "typeRoom1");

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
         throw new NotImplementedException();
      }
      
      public Examination GetExamination(String examId)
      {
         throw new NotImplementedException();
      }
      
      public void SetExamination(String examId, Examination newExam)
      {
         throw new NotImplementedException();
      }
      
      public bool DeleteExamination(String examId)
      {
         throw new NotImplementedException();
      }
      
      public bool LoadExamination()
      {
         throw new NotImplementedException();
      }
      
      public bool SaveExamination()
      {
         throw new NotImplementedException();
      }

        public List<Examination> ExaminationsForPatient(string id)
        {
            List<Examination> examsForPatient = new List<Examination>();
            foreach(Examination exam in examinationList)
            {
                if(exam.patient.getId().Equals(id)) examsForPatient.Add(exam);
            }
            return examsForPatient;
        }
      
      //public PatientService patientService;
      //public DoctorService doctorService;
   
   }
}