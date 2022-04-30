using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class PatientService
    {
        //dodato
        private readonly PatientRepo _patientRepo;
        private readonly ExaminationRepo _examinationRepo;
        private readonly DoctorRepo _doctorRepo;

        public PatientService(PatientRepo patientRepo, ExaminationRepo examinationRepo, DoctorRepo doctorRepo)
        {
            _patientRepo = patientRepo;
            _examinationRepo = examinationRepo;
            _doctorRepo = doctorRepo;
        }

        public int generateID (ObservableCollection<Examination> examinations)
        {
            return _examinationRepo.generateID(examinations);
        }

        public ObservableCollection<Examination> getAllExaminations()
        {
            return _examinationRepo.GetAll();
        }

        public void SaveExaminationRepo()
        {
            _examinationRepo.SaveExamination();
        }

        private List<DateTime> GetFreeDates(Doctor doctor, int maxDates)
        {
            throw new NotImplementedException();
        }

        public Model.Patient GetPatient(String id)
        {
            return _patientRepo.GetPatient(id);
        }

        public bool CheckIfAppointmentExists(DateTime date, Doctor doctor, String PatientID, String RoomID)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.GetAll();
            ObservableCollection<Patient> patientsFromBase = _patientRepo.GetAllPatients();
            ObservableCollection<Doctor> doctorsFromBase = _doctorRepo.GetAllDoctors();

            foreach (Examination exam in examinationsFromBase)
            {
                int res = DateTime.Compare(date, exam.Date);
                if (doctor.Id.Equals(exam.DoctorId) && res == 0)
                {
                    //ne moze jedan doktor da ima dva pregleda u isto vreme
                    return false;
                } else if(res == 0 && PatientID.Equals(exam.PatientId))
                {
                    //ne moze jedan pacijent da ima dva pregleda u isto vreme
                    return false;
                } else if(res == 0 && RoomID.Equals(exam.ExamRoomId))
                {
                    //ne mogu se odvijati dva pregleda u jednoj sobi u isto vreme
                    return false;
                }
            }
            return true;
        }

        public bool CheckIfAppointmentExistsForEditing(String ExamID ,DateTime date, Doctor doctor, String PatientID, String RoomID)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.GetAll();
            ObservableCollection<Patient> patientsFromBase = _patientRepo.GetAllPatients();
            ObservableCollection<Doctor> doctorsFromBase = _doctorRepo.GetAllDoctors();

            foreach (Examination exam in examinationsFromBase)
            {
                if (exam.Id.Equals(ExamID))
                {
                    continue;
                }
                int res = DateTime.Compare(date, exam.Date);
                if (doctor.Id.Equals(exam.DoctorId) && res == 0)
                {
                    //ne moze jedan doktor da ima dva pregleda u isto vreme
                    return false;
                }
                else if (res == 0 && PatientID.Equals(exam.PatientId))
                {
                    //ne moze jedan pacijent da ima dva pregleda u isto vreme
                    return false;
                }
                else if (res == 0 && RoomID.Equals(exam.ExamRoomId))
                {
                    //ne mogu se odvijati dva pregleda u jednoj sobi u isto vreme
                    return false;
                }
            }
            return true;
        }

        public bool CreateExam(Examination examination)
        {
            return _examinationRepo.NewExamination(examination);
        }

        public void RemoveExam(Examination examination)
        {
             _examinationRepo.DeleteExamination(examination);
        }

        public void SetExam(string examID, Examination examination)
        {
            _examinationRepo.SetExamination(examID, examination);
        }

        public Examination GetExam(string examID)
        {
            return _examinationRepo.GetExamination(examID);
        }

        public void EditExam(String examId, DateTime newDate)
        {
            _examinationRepo.EditExamination(examId, newDate);
        }

        public ObservableCollection<Examination> ReadMyExams(string id)
        {
            //dodato
            List<Examination> others = new List<Examination>();
            foreach(Examination exam in _examinationRepo.GetAll())
            {
                if (!exam.PatientId.Equals(id))
                {
                    others.Add(exam);
                }
            }

            foreach(Examination exam in others)
            {
                _examinationRepo.DeleteByPatient(exam.Id);
            }

            //return _examinationRepo.ExaminationsForPatient(id);
            return _examinationRepo.GetAll();
        }

        public ObservableCollection<Model.Patient> GetPatients()
        {
            return _patientRepo.GetAllPatients();
        }

        public ObservableCollection<Examination> GetExaminations()
        {
            return _examinationRepo.GetAll();
        }

    }
}