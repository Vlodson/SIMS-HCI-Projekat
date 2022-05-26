using HospitalMain.Enums;
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
    public class EmergencyService
    {

        private ExaminationRepo _examinationRepo;
        private DoctorRepo _doctorRepo;

        public EmergencyService(ExaminationRepo examinationRepo, DoctorRepo doctorRepo)
        {
            _examinationRepo = examinationRepo;
            _doctorRepo = doctorRepo;
        }

        private ObservableCollection<Doctor> GetDoctorsByType(DoctorType doctorType)
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

        private ObservableCollection<Examination> ExaminationsForDoctor(string id)
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
        private Examination GetBookedExamination(DateTime dateTime, DoctorType doctorType)
        {
            ObservableCollection<Doctor> doctors = GetDoctorsByType(doctorType);

            foreach (Doctor doctor in doctors)
            {
                Examination exam = BookedValidation(doctor, dateTime);
                if (exam != null)
                {
                    return exam;
                }
            }
            return null;
        }

        private Examination BookedValidation(Doctor doctor, DateTime dateTime)
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

            foreach (Doctor doctor in doctors)
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
            foreach (Examination exam in exams)
            {
                if (exam.Date == dateTime)
                {
                    return true;
                }
            }
            return false;
        }

        //validacija kod hitnih slucajeva
        public bool EmergencyValidation(DateTime dateTime, DoctorType doctorType)
        {
            if (CheckForAvailableDateForEmergency(dateTime, doctorType) != "")
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

        private ObservableCollection<Examination> GetFree(ObservableCollection<DateTime> startEndRange, DoctorType doctorType)
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
            foreach (Examination exam in ExaminationsForDoctor(doctor.Id))
            {
                if (DateTime.Compare(dt, exam.Date) == 0)
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
    }
}
