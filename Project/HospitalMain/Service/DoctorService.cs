using HospitalMain.Enums;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Service
{
    public class DoctorService
    {


        private readonly DoctorRepo _doctorRepo;
        private readonly ExaminationRepo _examinationRepo;
        private readonly RoomRepo _roomRepo;
        private readonly PatientRepo _patientRepo;

        public DoctorService(DoctorRepo doctorRepo, ExaminationRepo examinationRepo, RoomRepo roomRepo, PatientRepo patientRepo)
        {
            _doctorRepo = doctorRepo;
            _examinationRepo = examinationRepo;
            _roomRepo = roomRepo;
            _patientRepo = patientRepo;
        }

        public DoctorType GetDoctorsType(string doctorID)
        {
            foreach (Doctor doctor in _doctorRepo.Doctors)
            {
                if (doctor.Id.Equals(doctorID))
                {
                    return doctor.Type;
                }
            }
            return DoctorType.None;
        }

        public bool AddExaminationToDoctor(String doctorId, Examination examination)
        {
            foreach (Doctor doctor in _doctorRepo.Doctors)
            {
                if (doctorId.Equals(doctor.Id))
                {
                    doctor.Examinations.Add(examination);
                    return true;
                }
            }
            return false;
        }

        public void EditDoctorsExamination(String doctorId, Examination newExamination)
        {
            foreach (Doctor doctor in _doctorRepo.Doctors)
            {
                if (doctorId.Equals(doctor.Id))
                {
                    foreach (Examination exam in doctor.Examinations)
                    {
                        if (exam.Id.Equals(newExamination.Id))
                        {
                            exam.Id = newExamination.Id;
                            exam.ExamRoomId = newExamination.ExamRoomId;
                            exam.Duration = newExamination.Duration;
                            exam.Date = newExamination.Date;
                            exam.PatientId = newExamination.PatientId;
                            exam.EType = newExamination.EType;
                            break;
                        }
                    }
                    break;
                }
            }
        }

        public void CreateExam(Examination exam)
        {
            _examinationRepo.NewExamination(exam);
        }

        public void RemoveExam(Examination exam)
        {
            _examinationRepo.RemoveExamination(exam.Id);
        }

        public void RemoveExam(string examID)
        {
            _examinationRepo.RemoveExamination(examID);
        }

        public void EditExams(string id, Examination exam)
        {
            _examinationRepo.SetExamination(id,exam);
        }

        public ObservableCollection<Doctor> GetAllDoctors()
        {
            return _doctorRepo.Doctors;
        }

        public Doctor GetDoctor(string id)
        {
            foreach (Doctor doctor in _doctorRepo.Doctors)
            {
                if (doctor.Id.Equals(id))
                {
                    return doctor;
                }
            }
            return null;
        }

        //public ObservableCollection<Examination> GetDox(DateTime dateTime, DoctorType doctorType)
        //{
        //    ObservableCollection<Doctor> listOfDoctors = _doctorRepo.GetDoctorsByType(doctorType);

        //    bool flag = false;

        //    foreach(Doctor doctor in listOfDoctors)
        //    {
        //        foreach(Examination examinationofDoctor in doctor.Examinations)
        //        {

        //        }
        //    }
        //}

        public ObservableCollection<Examination> ExaminationsForDoctor(string id)
        {
            ObservableCollection<Examination> examsForDoctor = new ObservableCollection<Examination>();
            foreach (Examination exam in _examinationRepo.Examinations)
            {
                if (exam.DoctorId.Equals(id))
                    examsForDoctor.Add(exam);
            }
            return examsForDoctor;
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
            List<Examination> examinationsWithRooms = new List<Examination>();
            foreach(Examination exam in examinations)
            {
                if(CheckRoomExists(exam.Date)) examinationsWithRooms.Add(exam);
            }
            return examinationsWithRooms;
        }

        public List<Room> GetPatientRooms()
        {
            List<Room> patientRooms = new List<Room>();
            foreach (Room room in _roomRepo.Rooms)
            {
                if (room.Type == RoomTypeEnum.Patient_Room)
                {
                    patientRooms.Add(room);
                }
            }
            return patientRooms;
        }

        public bool CheckRoomExists(DateTime date)
        {
            int counterExams = _examinationRepo.getExamByTime(date).Count();
            int counterOccupied = GetPatientRooms().Where(r => r.Occupancy == false).Count();
            if(counterExams < GetPatientRooms().Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Examination> GetMovingDatesForExamination(Examination examination, Doctor doctor)
        {
            List<DateTime> examinationTimes = GenerateExaminationTimes(examination.Date.AddDays(1), examination.Date.AddDays(6));
            List<Examination> examinations = GenerateFreeExaminationTimes(examinationTimes, doctor);
            return examinations;
        }

        public List<Examination> AvailableMoveExaminations(Examination examination)
        {
            Doctor doctor = GetDoctor(examination.DoctorId);
            List<Examination> listForDoctor = GetMovingDatesForExamination(examination, doctor);
            List<Examination> examinationsWithRooms = new List<Examination>();
            foreach (Examination exam in listForDoctor)
            {
                if (CheckRoomExists(exam.Date)) examinationsWithRooms.Add(exam);
            }
            return examinationsWithRooms;
        }

        public ObservableCollection<Examination> ReadMyExams(string id)
        {
            return ExaminationsForDoctor(id);
        }

        public ObservableCollection<Examination> ReadEndedExams()
        {
            ObservableCollection<Examination> endedExams = new ObservableCollection<Examination>();
            foreach (Examination exam in _examinationRepo.Examinations)
            {
                int res = DateTime.Compare(exam.Date, DateTime.Now);
                if (res < 0)
                {
                    exam.NameSurnamePatient = _patientRepo.GetPatient(exam.PatientId).Name + " " + _patientRepo.GetPatient(exam.PatientId).Surname;
                    endedExams.Add(exam);
                }
            }
            return endedExams;
        }

        public bool occupiedDate(DateTime dt)
        {
            foreach (Examination exam in _examinationRepo.Examinations)
            {
                if (exam.Date.Equals(dt))
                    return true;
            }
            return false;
        }
        public ObservableCollection<string> GetDoctorsBySpecialization(DoctorType type)
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            foreach (Doctor doctor in _doctorRepo.Doctors)
            {
                if (type == doctor.Type)
                {
                    list.Add(doctor.Id);
                }
            }
            return list;
        }
    }
}