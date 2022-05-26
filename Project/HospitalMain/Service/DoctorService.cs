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

        private List<DateTime> GetFreeDates(Doctor doctor, int maxDates)
        {
            throw new NotImplementedException();
        }

        public DoctorType GetDoctorsType(string doctorID)
        {
            foreach (Doctor doctor in _doctorRepo.DoctorList)
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
            return _doctorRepo.AddExaminationToDoctor(doctorId, examination);
        }

        public void EditDoctorsExamination(String doctorID, Examination newExamination)
        {
            _doctorRepo.EditDoctorsExamination(doctorID, newExamination);
        }

        public void CreateExam(Examination exam)
        {
            _examinationRepo.NewExamination(exam);
        }

        public void RemoveExam(Examination exam)
        {
            _examinationRepo.DeleteExamination(exam.Id);
        }

        public void RemoveExam(string examID)
        {
            _examinationRepo.DeleteExamination(examID);
        }

        public void EditExams(string id, Examination exam)
        {
            _examinationRepo.SetExamination(id,exam);
        }

        public ObservableCollection<Doctor> GetDoctors()
        {
            return _doctorRepo.DoctorList;
        }

        public Doctor GetDoctor(string id)
        {
            foreach (Doctor doctor in _doctorRepo.DoctorList)
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
            foreach (Examination exam in _examinationRepo.ExaminationList)
            {
                if (exam.DoctorId.Equals(id))
                    examsForDoctor.Add(exam);
            }
            return examsForDoctor;
        }

        public List<Examination> GenerateDoctorFreeExaminations(Doctor doctor, DateTime startDate, DateTime endDate)
        {
            
            List<Examination> examinations = _examinationRepo.GenerateDoctorFreeExaminations(doctor, startDate, endDate);
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
                if (room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room)
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
        /*
        public List<Examination> GetFreeExaminations(Doctor doctor, DateTime startDate, DateTime endDate, bool priority)
        {
            ObservableCollection<Doctor> doctors = _doctorRepo.DoctorList;
            List<Examination> listExaminations = _examinationRepo.GetFreeExaminationsForDoctor(doctor, startDate, endDate, priority, doctors);
            List<Examination> listExaminationsWithRooms = new List<Examination>();
            //provera da li postoji slobodna prostorija za dati termin
            
            foreach (Examination exam in listExaminations)
            {
                List<Room> patientRooms = new List<Room>();
                foreach(Room room in _roomRepo.Rooms)
                {
                    if(room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room)
                    {
                        patientRooms.Add(room);
                    }
                }
                foreach (Examination examinationExists in _examinationRepo.getExamByTime(exam.Date))
                {
                    int counter = 0;
                    foreach (Room room in patientRooms)
                    {
                        if (room.Occupancy == true)
                        {
                            ++counter;
                        }
                    }
                    if (counter < patientRooms.Count)
                    {
                        listExaminationsWithRooms.Add(exam);
                    }
                }
                if(_examinationRepo.getExamByTime(exam.Date).Count == 0)
                {
                    listExaminationsWithRooms.Add(exam);
                }
                //exam.DoctorNameSurname = _doctorController.GetDoctor(doctor.Id).NameSurname;
            }
            //return _examinationRepo.GetFreeExaminationsForDoctor(doctor, startDate, endDate, priority, doctors);
            return listExaminationsWithRooms;
        }
        */

        public List<Examination> MoveExaminations(Examination examination)
        {
            Doctor doctor = GetDoctor(examination.DoctorId);
            List<Examination> listForDoctor = _examinationRepo.GetMovingDatesForExamination(examination, doctor);
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
            foreach (Examination exam in _examinationRepo.ExaminationList)
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
            return _examinationRepo.occupiedDate(dt);
        }
        public ObservableCollection<string> GetDoctorsBySpecialization(DoctorType type)
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            foreach (Doctor doctor in _doctorRepo.DoctorList)
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