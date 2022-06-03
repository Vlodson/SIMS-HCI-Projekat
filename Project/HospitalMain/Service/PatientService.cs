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
            int maxID = 0;

            foreach (Examination exam in examinations)
            {
                int examID = Int32.Parse(exam.Id);
                if (maxID < examID)
                {
                    maxID = examID;
                }
            }

            return maxID + 1;
        }

        public Examination getExamByTime(DateTime dateTime)
        {
            foreach(Examination e in _examinationRepo.ExaminationList)
            {
                if(e.Date == dateTime)
                {
                    return e;
                }
            }
            return null;
        }

        public ObservableCollection<Examination> getAllExaminations()
        {
            return _examinationRepo.ExaminationList;
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

        public bool AppointmentRoomValidation(DateTime date, String RoomID)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.ExaminationList;

            foreach(Examination exam in examinationsFromBase)
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
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.ExaminationList;

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
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.ExaminationList;

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
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.ExaminationList;

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
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.ExaminationList;

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
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.ExaminationList;

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
            
            Room getRoom = GetFreeRoom(newDate);
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
            return _patientRepo.Patients;
        }

        public ObservableCollection<Examination> GetExaminations()
        {
            return _examinationRepo.ExaminationList;
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
        public bool CheckAnswerAvailable(String doctorId, MedicalRecord medicalRecord)
        {
            return _patientRepo.CheckAnswerAvailable(doctorId, medicalRecord);
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