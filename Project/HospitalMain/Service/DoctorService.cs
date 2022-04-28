using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Service
{
    public class DoctorService
    {


        private readonly DoctorRepo _doctorRepo;
        private readonly ExaminationRepo _examinationRepo;
        private readonly RoomRepo _roomRepo;

        public DoctorService(DoctorRepo doctorRepo, ExaminationRepo examinationRepo, RoomRepo roomRepo)
        {
            _doctorRepo = doctorRepo;
            _examinationRepo = examinationRepo;
            _roomRepo = roomRepo;
        }

        private List<DateTime> GetFreeDates(Doctor doctor, int maxDates)
        {
            throw new NotImplementedException();
        }

        public void CreateExam(Examination exam)
        {
            _examinationRepo.NewExamination(exam);
        }

        public void RemoveExam(Examination exam)
        {
            _examinationRepo.DeleteExamination(exam);
        }

        public void EditExams(string id, Examination exam)
        {
            _examinationRepo.DoctorEditExamination(id,exam);
        }

        public List<Examination> ReadPatientExams(String patientId)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Doctor> GetDoctors()
        {
            return _doctorRepo.GetAllDoctors();
        }

        public Doctor GetDoctor(string id)
        {
            return _doctorRepo.GetDoctor(id);
        }

        public List<Examination> GetFreeExaminations(Doctor doctor, DateTime startDate, DateTime endDate, bool priority)
        {
            ObservableCollection<Doctor> doctors = _doctorRepo.GetAllDoctors();
            List<Examination> listExaminations = _examinationRepo.GetFreeExaminationsForDoctor(doctor, startDate, endDate, priority, doctors);
            List<Examination> listExaminationsWithRooms = new List<Examination>();
            //provera da li postoji slobodna prostorija za dati termin
            
            foreach (Examination exam in listExaminations)
            {
                /*
                int patientRooms = 0;
                int counter = 0;
                foreach (Room room in _roomRepo.Rooms)
                {
                    if(room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room)
                    {
                        patientRooms++;
                    }
                    if (room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room && room.Occupancy == true)
                    {
                        counter++;
                    }
                }
                if (counter < patientRooms)
                {
                    listExaminationsWithRooms.Add(exam);
                }
                */
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

        public List<Examination> MoveExaminations(Examination examination)
        {
            Doctor doctor = _doctorRepo.GetDoctor(examination.DoctorId);
            List<Examination> listForDoctor = _examinationRepo.MoveExamination(examination, doctor);
            List<Examination> listExaminationsWithRooms = new List<Examination>();
            foreach (Examination exam in listForDoctor)
            {
                int counter = 0;
                foreach (Room room in _roomRepo.Rooms)
                {
                    if (room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room && room.Occupancy == true)
                    {
                        counter++;
                    }
                }
                if (counter < _roomRepo.Rooms.Count)
                {
                    listExaminationsWithRooms.Add(exam);
                }
                //exam.DoctorNameSurname = _doctorController.GetDoctor(doctor.Id).NameSurname;
            }
            //return _examinationRepo.MoveExamination(examination, doctor);
            return listExaminationsWithRooms;
        }

        public ObservableCollection<Examination> ReadMyExams(string id)
        {
            return _examinationRepo.ExaminationsForDoctor(id);
        }

        public ObservableCollection<Examination> ReadEndedExams()
        {
            return _examinationRepo.ReadEndedExams();
        }

    }
}