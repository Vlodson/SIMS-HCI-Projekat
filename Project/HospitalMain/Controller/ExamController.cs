using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Controller
{
    public class ExamController
    {

        private PatientService _patientService;
        private DoctorService _doctorService;


        public ExamController(PatientService patientService, DoctorService doctorService)
        {
            this._patientService = patientService;
            this._doctorService = doctorService;
        }

        public void PatientCreateExam(Examination examination)
        {
            _patientService.CreateExam(examination);
        }

        public void DoctorCreateExam(Examination examination)
        {
            _doctorService.CreateExam(examination);
        }

        public void RemoveExam(Examination examination)
        {
            _patientService.RemoveExam(examination);
        }
        public void DoctorRemoveExam(Examination exam)
        {
            _doctorService.RemoveExam(exam);
        }

        public ObservableCollection<Examination> ReadPatientExams(string id)
        {
            return _patientService.ReadMyExams(id);
        }

        public ObservableCollection<Examination> ReadDoctorExams(string id)
        {
            return _doctorService.ReadMyExams(id);
        }

        public void DoctorEditExam(string id, Examination examination)
        {
            _doctorService.EditExams(id, examination);
        }

        public void PatientEditExam(Examination examination, DateTime dateTime)
        {
            _patientService.EditExam(examination.Id, dateTime);
        }

        public void EditExam(string examID, Examination examination)
        {
            _patientService.SetExam(examID, examination);
        }

        public ObservableCollection<Examination> ReadEndedExams()
        {
            return _doctorService.ReadEndedExams();
        }


        /*
      public System.Collections.ArrayList DoctorService
      {
         get
         {
            if (doctorService == null)
               doctorService = new System.Collections.ArrayList();
            return doctorService;
         }
         set
         {
            RemoveAllDoctorService();
            if (value != null)
            {
               foreach (Service.DoctorService oDoctorService in value)
                  AddDoctorService(oDoctorService);
            }
         }
      }
        */


        public void AddDoctorService(Service.DoctorService newDoctorService)
        {
            /*
         if (newDoctorService == null)
            return;
         if (this.doctorService == null)
            this.doctorService = new System.Collections.ArrayList();
         if (!this.doctorService.Contains(newDoctorService))
            this.doctorService.Add(newDoctorService);
            */
        }


        public void RemoveDoctorService(Service.DoctorService oldDoctorService)
        {
            /*
         if (oldDoctorService == null)
            return;
         if (this.doctorService != null)
            if (this.doctorService.Contains(oldDoctorService))
               this.doctorService.Remove(oldDoctorService);
            */
        }


        public void RemoveAllDoctorService()
        {
            /*
         if (doctorService != null)
            doctorService.Clear();
            */
        }
        //public System.Collections.ArrayList patientService;


        /*
      public System.Collections.ArrayList PatientService
      {
         get
         {
            if (patientService == null)
               patientService = new System.Collections.ArrayList();
            return patientService;
         }
         set
         {
            RemoveAllPatientService();
            if (value != null)
            {
               foreach (Service.PatientService oPatientService in value)
                  AddPatientService(oPatientService);
            }
         }
      }
        */


        public void AddPatientService(Service.PatientService newPatientService)
        {
            /*
         if (newPatientService == null)
            return;
         if (this.patientService == null)
            this.patientService = new System.Collections.ArrayList();
         if (!this.patientService.Contains(newPatientService))
            this.patientService.Add(newPatientService);
            */
        }


        public void RemovePatientService(Service.PatientService oldPatientService)
        {
            /*
         if (oldPatientService == null)
            return;
         if (this.patientService != null)
            if (this.patientService.Contains(oldPatientService))
               this.patientService.Remove(oldPatientService);
            */
        }


        public void RemoveAllPatientService()
        {
            /*
         if (patientService != null)
            patientService.Clear();
            */
        }

    }
}