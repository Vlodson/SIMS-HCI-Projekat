using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalMain.Service
{
    public class ValidationService
    {
        private ExaminationRepo _examinationRepo;

        public ValidationService(ExaminationRepo examinationRepo)
        {
            _examinationRepo = examinationRepo;
        }

        public bool AppointmentRoomValidation(DateTime date, String RoomID)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.Examinations;

            foreach (Examination exam in examinationsFromBase)
            {
                //ne mogu se odvijati dva pregleda u jednoj sobi u isto vreme
                if (date > exam.Date.AddMinutes(-30) && date < exam.Date.AddMinutes(30) && RoomID.Equals(exam.ExamRoomId))
                {
                    return false;
                }
            }
            return true;
        }

        public bool AppointmentDoctorValidation(DateTime date, Doctor doctor)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.Examinations;

            foreach (Examination exam in examinationsFromBase)
            {
                //ne moze jedan doktor da ima dva pregleda u isto vreme
                if (doctor.Id.Equals(exam.DoctorId) && date > exam.Date.AddMinutes(-30) && date < exam.Date.AddMinutes(30))
                {
                    return false;
                }
            }
            return true;
        }

        public bool AppointmentPatientValidation(DateTime date, String PatientID)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.Examinations;

            foreach (Examination exam in examinationsFromBase)
            {
                //ne moze jedan pacijent da ima dva pregleda u isto vreme
                if (date > exam.Date.AddMinutes(-30) && date < exam.Date.AddMinutes(30) && PatientID.Equals(exam.PatientId))
                {
                    return false;
                }
            }
            return true;
        }

        public bool AppointmentRoomEditValidation(String ExamID, DateTime date, String RoomID)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.Examinations;

            foreach (Examination exam in examinationsFromBase)
            {
                if (exam.Id.Equals(ExamID))
                {
                    continue;
                }
                //ne mogu se odvijati dva pregleda u jednoj sobi u isto vreme
                if (date > exam.Date.AddMinutes(-30) && date < exam.Date.AddMinutes(30) && RoomID.Equals(exam.ExamRoomId))
                {
                    return false;
                }
            }
            return true;
        }

        public bool AppointmentDoctorEditValidation(String ExamID, DateTime date, Doctor doctor)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.Examinations;

            foreach (Examination exam in examinationsFromBase)
            {
                if (exam.Id.Equals(ExamID))
                {
                    continue;
                }
                //ne moze jedan doktor da ima dva pregleda u isto vreme
                if (doctor.Id.Equals(exam.DoctorId) && date > exam.Date.AddMinutes(-30) && date < exam.Date.AddMinutes(30))
                {
                    return false;
                }
            }
            return true;
        }

        public bool AppointmentPatientEditValidation(String ExamID, DateTime date, String PatientID)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.Examinations;

            foreach (Examination exam in examinationsFromBase)
            {
                if (exam.Id.Equals(ExamID))
                {
                    continue;
                }
                //ne moze jedan pacijent da ima dva pregleda u isto vreme
                if (date > exam.Date.AddMinutes(-30) && date < exam.Date.AddMinutes(30) && PatientID.Equals(exam.PatientId))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
