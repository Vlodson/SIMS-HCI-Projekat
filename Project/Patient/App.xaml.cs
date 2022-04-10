using Controller;
using Model;
using Repository;
using Service;
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

        public App()
        {
            //ovo obrisati pa zamneiti iz fajla kad do toga dodjem
            List<Examination> exams = new List<Examination>();
            List<Doctor> doctors = new List<Doctor>();
            List<Model.Patient> patients = new List<Model.Patient>();


            ExaminationRepo examinationRepository = new ExaminationRepo("...");
            PatientRepo patientRepository = new PatientRepo("...");
            PatientService patientService = new PatientService(patientRepository, examinationRepository);
            DoctorRepo doctorRepository = new DoctorRepo("...", doctors);
            DoctorService doctorService = new DoctorService(doctorRepository, examinationRepository);

            ExamController = new ExamController(patientService);
            DoctorController = new DoctorController(doctorService);
            PatientController = new PatientController(patientService);
        }
    }
}
