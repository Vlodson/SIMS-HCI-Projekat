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
            return _doctorRepo.GetDoctorsType(doctorID);
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
            return _doctorRepo.GetAllDoctors();
        }

        public Doctor GetDoctor(string id)
        {
            return _doctorRepo.GetDoctor(id);
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

        //ova fja se poziva samo u slucaju kada svi doktori odredjene specijalizacije imaju zakazane termine kada je i hitan slucaj
        //funkcija vraca prvi termin na koji naidje, a koji se poklapa sa hitnim slucajem
        public Examination GetBookedExamination(DateTime dateTime, DoctorType doctorType)
        {
            ObservableCollection<Doctor> doctors = _doctorRepo.GetDoctorsByType(doctorType);

            foreach(Doctor doctor in doctors)
            {
                foreach(Examination exam in _examinationRepo.ExaminationsForDoctor(doctor.Id))
                {
                    if(exam.Date == dateTime)
                    {
                        return exam;
                    }
                }
            }
            return null;
        }

        //ova funkcija vraca id prvog doktora na kojeg naidje odredjene specijalizacije, koji nema zakazan termin u terminu hitnog slucaja (jednostavnija opcija)
        //u suprotnom vraca prazan string (ide se na tezu opciju, jer nema doktora koji ima slobodan termin u terminu hitnog slucaja) 
        public string CheckForAvailableDateForEmergency(DateTime dateTime, DoctorType doctorType)
        {
            ObservableCollection<Doctor> doctors = _doctorRepo.GetDoctorsByType(doctorType);
            
            foreach (Doctor doctor in doctors)
            {
                bool flag = false;
                ObservableCollection<Examination> exams = _examinationRepo.ExaminationsForDoctor(doctor.Id);
                foreach(Examination exam in exams)
                {
                    if(exam.Date == dateTime)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    continue;
                }
                return doctor.Id;
            }
            return "";
        }

        //validacija kod hitnih slucajeva
        public bool EmergencyValidation(DateTime dateTime, DoctorType doctorType)
        {
            if(CheckForAvailableDateForEmergency(dateTime, doctorType) != "")
            {
                return true;
            } 
            else
            {
                //dobavljam zakazan termin koji ce biti pomeren
                Examination bookedExam = GetBookedExamination(dateTime, doctorType);

                //cuvam njegove info u propertiju iz examRepo-a
                _examinationRepo.TemporaryExam = bookedExam;

                //brojac povecavam
                _examinationRepo.ValidationCounter++;

                return false;
            }
        }

        //funkcija koja vraca slobodne termine u odredjenom periodu razlicitih doktora iste specijalizacije, kako bi se odabrao jedan od tih termina u koji ce biti pomeren pregled i pacijent koga je pomerio hitan slucaj
        public ObservableCollection<Examination> GetFreeExaminations(DateTime startDate, DateTime endDate, DoctorType doctorType)
        {
            ObservableCollection<Doctor> doctors = _doctorRepo.GetDoctorsByType(doctorType);
            ObservableCollection<Examination> listExams = _examinationRepo.GetFreeExaminations(startDate, endDate, doctors);
            return listExams;
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
            ObservableCollection<Doctor> doctors = _doctorRepo.GetAllDoctors();
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
            Doctor doctor = _doctorRepo.GetDoctor(examination.DoctorId);
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
            return _examinationRepo.ExaminationsForDoctor(id);
        }

        public ObservableCollection<Examination> ReadEndedExams()
        {
            ObservableCollection<Examination> endedExams = new ObservableCollection<Examination>();
            foreach (Examination exam in _examinationRepo.examinationList)
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
            foreach(Doctor doctor in _doctorRepo.DoctorList)
            {
                if(type == doctor.Type)
                {
                    list.Add(doctor.Id);
                }
            }
            return list;
        }
    }
}