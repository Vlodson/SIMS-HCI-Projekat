using HospitalMain.Enums;
using HospitalMain.Model;
using HospitalMain.Repository;
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

        public bool CheckIfAppointmentExists(DateTime date, Doctor doctor, String PatientID, String RoomID)
        {
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.ExaminationList;
            ObservableCollection<Patient> patientsFromBase = _patientRepo.Patients;
            ObservableCollection<Doctor> doctorsFromBase = _doctorRepo.DoctorList;

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
            ObservableCollection<Examination> examinationsFromBase = _examinationRepo.ExaminationList;
            ObservableCollection<Patient> patientsFromBase = _patientRepo.Patients;
            ObservableCollection<Doctor> doctorsFromBase = _doctorRepo.DoctorList;

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

        public bool CreateExam(Examination examination, DateTime newDate)
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
            //dodato
            //List<Examination> others = new List<Examination>();
            //foreach(Examination exam in _examinationRepo.GetAll())
            //{
            //    if (!exam.PatientId.Equals(id))
            //    {
            //        others.Add(exam);
            //    }
            //}

            //foreach(Examination exam in others)
            //{
            //    _examinationRepo.DeleteExamination(exam.Id);
            //}

            ////return _examinationRepo.ExaminationsForPatient(id);
            //return _examinationRepo.GetAll();
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