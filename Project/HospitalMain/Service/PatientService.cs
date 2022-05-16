using HospitalMain.Enums;
using HospitalMain.Model;
using HospitalMain.Repository;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Service
{
    public class PatientService
    {
        //dodato
        private readonly PatientRepo _patientRepo;
        private readonly ExaminationRepo _examinationRepo;
        private readonly DoctorRepo _doctorRepo;
        private readonly RoomRepo _roomRepo;
        private readonly QuestionnaireRepo _questionaryRepo;

        public PatientService(PatientRepo patientRepo, ExaminationRepo examinationRepo, DoctorRepo doctorRepo, RoomRepo roomRepo, QuestionnaireRepo questionnaireRepo)
        {
            _patientRepo = patientRepo;
            _examinationRepo = examinationRepo;
            _doctorRepo = doctorRepo;
            _roomRepo = roomRepo;
            _questionaryRepo = questionnaireRepo;
        }

        public Examination getTemporaryExam()
        {
            return _examinationRepo.TemporaryExam;
        }

        public int getValidationCounter()
        {
            return _examinationRepo.ValidationCounter;
        }

        public void setValidationCounter(int value)
        {
            _examinationRepo.ValidationCounter = value;
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
                DateTime dateTimeHigher = exam.Date.AddMinutes(30);
                DateTime dateTimeLower = exam.Date.AddMinutes(-30);

                //int res = DateTime.Compare(date, exam.Date);
                if (doctor.Id.Equals(exam.DoctorId) && date > dateTimeLower && date < dateTimeHigher)
                {
                    //ne moze jedan doktor da ima dva pregleda u isto vreme
                    return false;
                } else if(date > dateTimeLower && date < dateTimeHigher && PatientID.Equals(exam.PatientId))
                {
                    //ne moze jedan pacijent da ima dva pregleda u isto vreme
                    return false;
                } else if(date > dateTimeLower && date < dateTimeHigher && RoomID.Equals(exam.ExamRoomId))
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

                DateTime dateTimeHigher = exam.Date.AddMinutes(30);
                DateTime dateTimeLower = exam.Date.AddMinutes(-30);

                //int res = DateTime.Compare(date, exam.Date);
                if (doctor.Id.Equals(exam.DoctorId) && date > dateTimeLower && date < dateTimeHigher )
                {
                    //ne moze jedan doktor da ima dva pregleda u isto vreme
                    return false;
                }
                else if (date > dateTimeLower && date < dateTimeHigher && PatientID.Equals(exam.PatientId))
                {
                    //ne moze jedan pacijent da ima dva pregleda u isto vreme
                    return false;
                }
                else if (date > dateTimeLower && date < dateTimeHigher && RoomID.Equals(exam.ExamRoomId))
                {
                    //ne mogu se odvijati dva pregleda u jednoj sobi u isto vreme
                    return false;
                }
            }
            return true;
        }

        public bool CreateExamination(Examination examination)
        {
            return _examinationRepo.NewExamination(examination);
        }

        public Room GetFreeRoomFromRooms(List<Room> patientRooms)
        {
            Room getRoom = new Room();
            foreach (Room room in patientRooms)
            {
                if (room.Occupancy == false)
                {
                    getRoom = room;
                }
            }
            return getRoom;
        }

        public Room GetFreeRoomFromRoomsWhereOccupied(List<Room> patientRooms, DateTime dateTime)
        {
            Room getRoom = new Room();
            foreach (Examination examinationExists in _examinationRepo.getExamByTime(dateTime))
            {

                foreach (Room room in patientRooms)
                {
                    if (room.Occupancy == false && examinationExists.ExamRoomId != room.Id)
                    {
                        getRoom = room;
                        break;
                    }
                }
            }
            return getRoom;
        }
        public Room GetFirstRoom(DateTime dateTime, List<Room> patientRooms)
        {
            Room getRoom = new Room();
            if(_examinationRepo.getExamByTime(dateTime).Count == 0)
            {
                getRoom=GetFreeRoomFromRooms(patientRooms);
            }
            else
            {
                getRoom = GetFreeRoomFromRoomsWhereOccupied(patientRooms, dateTime);
            }
            return getRoom;
        }
        public Room GetFreeRoom(DateTime newDate)
        {
            Room getRoom = new Room();
            List<Room> patientRooms = _roomRepo.Rooms.Where(r => r.Type == RoomTypeEnum.Patient_Room).ToList();

            getRoom = GetFirstRoom(newDate, patientRooms);
            return getRoom;
        }
        public bool CreateExam(Examination examination, DateTime newDate)
        {
            //Room getRoom = new Room();
            //List<Room> patientRooms = new List<Room>();
            //foreach (Room room in _roomRepo.Rooms)
            //{
            //    if (room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room)
            //    {
            //        patientRooms.Add(room);
            //    }
            //}

            //if (_examinationRepo.getExamByTime(newDate).Count == 0)
            //{
            //    foreach (Room room in patientRooms)
            //    {
            //        if (room.Occupancy == false)
            //        {
            //            getRoom = room;
            //        }
            //    }
            //}

            //foreach (Examination examinationExists in _examinationRepo.getExamByTime(newDate))
            //{
            //    bool take = false;
            //    foreach (Room room in patientRooms)
            //    {
            //        if (room.Occupancy == false)
            //        {
            //            if (examinationExists.ExamRoomId != room.Id)
            //            {
            //                take = true;
            //                getRoom = room;
            //                break;
            //            }
            //        }
            //    }
            //}
            Room getRoom = GetFreeRoom(newDate);
            examination.ExamRoomId = getRoom.Id;
            Patient patient = _patientRepo.GetPatient(examination.PatientId);
            patient.NumberNewExams += 1;
            return _examinationRepo.NewExamination(examination);
        }

        

        public void RemoveExam(Examination examination)
        {
            Patient patient = _patientRepo.GetPatient(examination.PatientId);
            patient.NumberCanceling += 1;
            _patientRepo.SavePatient();
            _examinationRepo.DeleteExamination(examination.Id);
            //Room room = _roomRepo.GetRoom(examination.ExamRoomId);
            //_roomRepo.SetRoom(room.Id, room.Equipment, room.Floor, room.RoomNb, false, room.Type);
        }

        public void SetExam(string examID, DateTime date, String roomId, int duration, ExaminationTypeEnum examType, String patientId, String doctorId)
        {
            Examination examination = new Examination(roomId, date, examID, duration, examType, patientId, doctorId);
            _examinationRepo.SetExamination(examID, examination);
        }

        public Examination GetExam(string examID)
        {
            return _examinationRepo.GetExaminationById(examID);
        }

        public void EditExamForMoving(String examId, DateTime newDate)
        {
            Room getRoom = new Room();
            List<Room> patientRooms = new List<Room>();
            foreach (Room room in _roomRepo.Rooms)
            {
                if (room.Type == HospitalMain.Enums.RoomTypeEnum.Patient_Room)
                {
                    patientRooms.Add(room);
                }
            }
            if (_examinationRepo.getExamByTime(newDate).Count == 0)
            {
                foreach (Room room in patientRooms)
                {
                    if (room.Occupancy == false)
                    {
                        getRoom = room;
                    }
                }
            }
            foreach (Examination examinationExists in _examinationRepo.getExamByTime(newDate))
            {
                bool take = false;
                foreach (Room room in patientRooms)
                {
                    if (room.Occupancy == false)
                    {
                        if (examinationExists.ExamRoomId != room.Id)
                        {
                            take = true;
                            getRoom = room;
                            break;
                        }
                    }
                }
            }
            Examination examination = _examinationRepo.GetExaminationById(examId);
            examination.Date = newDate;
            examination.ExamRoomId = getRoom.Id;
            _patientRepo.GetPatient(examination.PatientId).NumberCanceling += 1;
            _examinationRepo.SetExamination(examId, examination);
        }

        public ObservableCollection<Examination> ReadMyExams(string id)
        {
            return _examinationRepo.ExaminationsForPatient(id);
            
        }

        public ObservableCollection<Model.Patient> GetPatients()
        {
            return _patientRepo.GetAllPatients();
        }

        public ObservableCollection<Examination> GetExaminations()
        {
            return _examinationRepo.GetAll();
        }

        public List<Examination> GetExamByTime(DateTime dateTime)
        {
            return _examinationRepo.getExamByTime(dateTime);
        }

        public Questionnaire GetHospitalQuestionnaire()
        {
            return _questionaryRepo.GetHospitalQuestionnaire();
        }

        public Questionnaire GetDoctorQuestionnaire()
        {
            return _questionaryRepo.GetDoctorQuestionnaire();
        }

        public void AddAnswer(String idPatient, Answer answer)
        {
            _patientRepo.AddAnswer(idPatient, answer);
        }

        public List<String> GetPatientsDoctors(String patientId)
        {
            return _examinationRepo.GetPatientsDoctors(patientId);
        }

        public bool CheckStatusCancelled(String id)
        {
            return _patientRepo.CheckStatusCancelled(id);
        }

        public bool CheckStatusAdded(String id)
        {
            return _patientRepo.CheckStatusAdded(id);
        }
    }
}