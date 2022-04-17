using Controller;
using Model;
using Repository;
using Service;
using Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Patient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 


    
    public partial class App : Application
    {
        public ExamController ExamController { get; set; }
        public DoctorController DoctorController { get; set; }
        public PatientController PatientController { get; set; }

        public ExaminationRepo ExaminationRepo { get; set; }

        public App()
        {
            //ovo obrisati pa zamneiti iz fajla kad do toga dodjem
            List<Examination> exams = new List<Examination>();
            List<Doctor> doctors = new List<Doctor>();
            List<Model.Patient> patients = new List<Model.Patient>();

            //ExaminationRepo examinationRepository = new ExaminationRepo(dbPathExams);
            ExaminationRepo = new ExaminationRepo(GlobalPaths.ExamsDBPath);
            PatientRepo patientRepository = new PatientRepo(GlobalPaths.PatientsDBPath);
            PatientService patientService = new PatientService(patientRepository, ExaminationRepo);
            PatientAccountService patientAccountService = new PatientAccountService(patientRepository);
            DoctorRepo doctorRepository = new DoctorRepo("...", doctors);
            DoctorService doctorService = new DoctorService(doctorRepository, ExaminationRepo);

            ExamController = new ExamController(patientService, doctorService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService, patientAccountService);
        }
    }
}
