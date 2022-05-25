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

        public ObservableCollection<Doctor> GetDoctorsByType(DoctorType doctorType)
        {
            ObservableCollection<Doctor> listOfDoctors = new ObservableCollection<Doctor>();

            foreach (Doctor doctor in _doctorRepo.DoctorList)
            {
                if (doctor.Type == doctorType)
                {
                    listOfDoctors.Add(doctor);
                }
            }

            return listOfDoctors;
        }

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

        //ova fja se poziva samo u slucaju kada svi doktori odredjene specijalizacije imaju zakazane termine kada je i hitan slucaj
        //funkcija vraca prvi termin na koji naidje, a koji se poklapa sa hitnim slucajem
        public Examination GetBookedExamination(DateTime dateTime, DoctorType doctorType)
        {
            ObservableCollection<Doctor> doctors = GetDoctorsByType(doctorType);

            foreach(Doctor doctor in doctors)
            {
                Examination exam = sdsadas(doctor, dateTime);
                if(exam != null)
                {
                    return exam;
                }
            }
            return null;
        }

        private Examination sdsadas(Doctor doctor, DateTime dateTime)
        {
            foreach (Examination exam in ExaminationsForDoctor(doctor.Id))
            {
                if (exam.Date == dateTime)
                {
                    return exam;
                }
            }
            return null;
        }

        //ova funkcija vraca id prvog doktora na kojeg naidje odredjene specijalizacije, koji nema zakazan termin u terminu hitnog slucaja (jednostavnija opcija)
        //u suprotnom vraca prazan string (ide se na tezu opciju, jer nema doktora koji ima slobodan termin u terminu hitnog slucaja) 
        public string CheckForAvailableDateForEmergency(DateTime dateTime, DoctorType doctorType)
        {
            ObservableCollection<Doctor> doctors = GetDoctorsByType(doctorType);
            
            foreach(Doctor doctor in doctors)
            {
                ObservableCollection<Examination> exams = ExaminationsForDoctor(doctor.Id);
                if (SearchingAvailableDate(exams, dateTime))
                {
                    continue;
                }
                return doctor.Id;
            }
            return "";
        }

        private bool SearchingAvailableDate(ObservableCollection<Examination> exams, DateTime dateTime)
        {
            foreach(Examination exam in exams)
            {
                if(exam.Date == dateTime)
                {
                    return true;
                }
            }
            return false;
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
        public ObservableCollection<Examination> GetFreeExaminations(ObservableCollection<DateTime> startEndRange, DoctorType doctorType)
        {
            ObservableCollection<Examination> listExams = GetFree(startEndRange, doctorType);
            return listExams;
        }

        public ObservableCollection<Examination> GetFree(ObservableCollection<DateTime> startEndRange, DoctorType doctorType)
        {
            ObservableCollection<Examination> examinations = new ObservableCollection<Examination>();
            ListFreeDoctorsAppointments(doctorType, examinations, startEndRange);

            //situuacija ako prvog dana nema nijedan slobodan termin, trazice u narednim danima sve dok ne nadje neki slobodan
            while (examinations.Count == 0)
            {
                ResetStartEndRange(startEndRange);
                ListFreeDoctorsAppointments(doctorType, examinations, startEndRange);
            }
            return examinations;
        }

        private void ListFreeDoctorsAppointments(DoctorType doctorType, ObservableCollection<Examination> examinations, ObservableCollection<DateTime> startEndRange)
        {
            foreach (Doctor doctor in GetDoctorsByType(doctorType))
            {
                ListFreeAppointmentsForDoctor(doctor, examinations, startEndRange);
            }
        }

        private void ListFreeAppointmentsForDoctor(Doctor doctor, ObservableCollection<Examination> examinations, ObservableCollection<DateTime> startEndRange)
        {
            foreach (DateTime dt in CreateExamsInOneDay(startEndRange))
            {
                if (CheckIfExamIsFree(doctor, dt))
                {
                    examinations.Add(new Examination("", dt, "-1", 30, ExaminationTypeEnum.OrdinaryExamination, "", doctor.Id));
                }
            }
        }

        private void ResetStartEndRange(ObservableCollection<DateTime> startEndRange)
        {
            DateTime start = startEndRange[0].Date;
            DateTime end = startEndRange[1].Date;

            startEndRange.Clear();
            startEndRange.Add(start);
            end = end.AddDays(1);
            startEndRange.Add(end);
        }

        private bool CheckIfExamIsFree(Doctor doctor, DateTime dt)
        {
            //ReadMyExams je isto sto i ExaminationsForDoctor
            foreach (Examination exam in ReadMyExams(doctor.Id))
            {
                if(DateTime.Compare(dt, exam.Date) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private ObservableCollection<DateTime> CreateExamsInOneDay(ObservableCollection<DateTime> startEndRange)
        {
            ObservableCollection<DateTime> examinationsInSomeRange = new ObservableCollection<DateTime>();
            DateTime start = startEndRange[0].Date;
            DateTime end = startEndRange[1].Date;

            //opseg dana
            int days = Convert.ToInt32((end - start).TotalDays);
            for (int i = 0; i < days + 1; i++)
            {
                //za svaki dan se generisu termini
                DateTime firstExamInDay = new DateTime(start.AddDays(i).Year, start.AddDays(i).Month, start.AddDays(i).Day, 7, 0, 0);
                AddExamInOneDay(firstExamInDay, examinationsInSomeRange);
            }
            return examinationsInSomeRange;
        }

        private void AddExamInOneDay(DateTime firstExamInDay, ObservableCollection<DateTime> examinationsInSomeRange)
        {
            for (int j = 0; j < 16; j++)
            {
                examinationsInSomeRange.Add(firstExamInDay.AddMinutes(j * 30));
            }
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